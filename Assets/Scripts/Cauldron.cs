using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cauldron : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (ColorLoop ());
		StartCoroutine (HeatLoop ());
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
	{
		if (GUILayout.Button("GoldenApple"))
			AddIngredient(Ingredient.IngredientType.GoldenApple);
		if (GUILayout.Button("BrownMushroom"))
			AddIngredient(Ingredient.IngredientType.BrownMushroom);
		if (GUILayout.Button("StirClockwise"))
			AddIngredient(Ingredient.IngredientType.CWStir);
	}

	public void StartRecipie(Recipie newRecipie)
	{
		currentRecipie = newRecipie;
		currentStep = 0;
		currentStepStartedTime = Time.time;
		addedIngredientsThisStep.Clear ();
		SetNewColor ();
	}

	public Recipie currentRecipie;
	int currentStep = 0;
	float currentStepStartedTime = 0;
	public List<IngredientAmount> addedIngredientsThisStep = new List<IngredientAmount> ();

	// When something touches the potion surface, add it to the potion
	void OnTriggerEnter(Collider other)
	{
		// Try to get the ingredient component of the thing added
		// Ingredient components should be on the "root" of the thing being added. It can have colliders in its children (eg a mushroom can have two colliders)
		Ingredient thisIngredient = other.GetComponentInParent<Ingredient>();
		// If there is no ingredient component, don't add it. We may want to make it vanish after some amount of time
		if (!thisIngredient) {
			// If the object can "respawn", respawn it. Otherwise, destroy it.
			//>Vanish the thing if possible
			return;
		}
		// You need to be working on a recipie
		if (currentRecipie == null) {
			//>Vanish the thing if possible
			return;
		}
		// Add this thing to the potion
		AddIngredient(thisIngredient.type);
		// Make the special effects for adding this ingredient (splash, sound effects, etc)
		// This may be ingredient-specific, stored in the ingredient or having to do with the ingredient parameters
		InstantiateSplashEffect(thisIngredient);
		// Destroy this ingredient
		Destroy(thisIngredient.gameObject);
	}

	public void StirClockWise()
	{
		AddIngredient (Ingredient.IngredientType.CWStir);
	}

	public void StirCounterClockwise()
	{
		AddIngredient (Ingredient.IngredientType.CCWStir);
	}

	void AddIngredient(Ingredient.IngredientType ingredientType)
	{
		// You need to be working on a recipie
		if (currentRecipie == null) {
			Debug.Log ("No current recipie!");
			return;
		}
		if (GetCurrentStep () == null) {
			Debug.Log ("No current step!");
			return;
		}
		// Check if you added something WRONG!!!
		bool recipieContainsIngredient = false;
		foreach (IngredientAmount requiredIngredient in GetCurrentStep().requiredIngredients) {
			if (requiredIngredient.ingredient == ingredientType)
				recipieContainsIngredient = true;
		}
		if (!recipieContainsIngredient)
		{
			RecipieFailed();
			return;
		}
		// Add ingredient to the dictionary
		bool addedAlready = false;
		foreach (IngredientAmount addedIngredient in addedIngredientsThisStep) {
			if (addedIngredient.ingredient == ingredientType)
				addedIngredient.value = addedIngredient.value + 1;
		}
		if (!addedAlready) {
			IngredientAmount newAmount = new IngredientAmount ();
			newAmount.ingredient = ingredientType;
			newAmount.value = 1;
			addedIngredientsThisStep.Add (newAmount);
		}
		CheckRecipieStatus ();
		SetNewColor ();
	}

	void InstantiateSplashEffect(Ingredient ingredientAdded)
	{
		Debug.Log ("Splash!");
	}

	void CheckRecipieStatus()
	{
		bool allPassConditionsMet = true;
		foreach (IngredientAmount requiredIngredient in GetCurrentStep().requiredIngredients)
		{
			bool thisIngredientWasAdded = false;
			foreach (IngredientAmount addedIngredientAmount in addedIngredientsThisStep) {
				if (addedIngredientAmount.ingredient == requiredIngredient.ingredient)
				{
					if (addedIngredientAmount.value == requiredIngredient.value)
						thisIngredientWasAdded = true;
					else if (addedIngredientAmount.value > requiredIngredient.value)
					{
						RecipieFailed ();
						return;
					}
				}
			}
			if (!thisIngredientWasAdded)
				allPassConditionsMet = false;
		}

		if (allPassConditionsMet) {
			RecipieStepPassed ();
			return;
		}
	}
		
	RecipieStep GetCurrentStep()
	{
		if (currentStep >= currentRecipie.steps.Length)
			return null;
		return currentRecipie.steps [currentStep];	
	}

	void RecipieStepPassed()
	{
		currentStep++;
		currentStepStartedTime = Time.time;
		addedIngredientsThisStep.Clear ();
		CancelInvoke ("RanOuttaTime");
		if (currentStep >= currentRecipie.steps.Length)
			RecipiePassed ();
		else {
			// If the next step has a timer fail condition, start the timer to instantly fail
			if (GetCurrentStep().maxStepTime > 0)
				Invoke("RanOuttaTime", currentRecipie.steps[currentStep].maxStepTime);
		}

	}

	float progress = 0;
	void SetNewColor()
	{
		if (currentRecipie != null && currentRecipie.steps.Length > 0) {
			progress = (float)currentStep / (float)currentRecipie.steps.Length;
			desiredColor = Color.Lerp (Random.ColorHSV (0, 1, 0.5f, 1, 0.7f, 1, 1, 1), currentRecipie.finalColor, progress);
		}
		else {
			progress = 0;
			desiredColor = Color.gray;
		}
		Debug.Log (progress);
	}

	public Renderer cauldronSurfaceRenderer;
	public Light cauldronGlow;
	public static Color desiredColor = Color.grey;
	public float colorChangeSpeed = 3;
	public static Color smoothedColor = Color.grey;
	IEnumerator ColorLoop()
	{
		Material waterMaterial = cauldronSurfaceRenderer.material;
		while (true) {
			smoothedColor = Color.Lerp (smoothedColor, desiredColor, Time.deltaTime * colorChangeSpeed);
			waterMaterial.SetColor ("_BaseColor", MakeBaseColor(smoothedColor));
			waterMaterial.SetColor ("_ReflectionColor", MakeReflectionColor(smoothedColor));
			cauldronGlow.color = smoothedColor;
			yield return null;
		}
	}

	Color MakeBaseColor(Color input)
	{
		return new Color (input.r, input.g, input.b, 0.5f);
	}

	Color MakeReflectionColor(Color input)
	{
		return new Color (input.r * 1.2f, input.g * 1.2f, input.b * 1.2f, 0.5f);
	}
		

	void RanOuttaTime()
	{
		RecipieFailed ();
	}

	void RecipiePassed()
	{
		Debug.Log ("YOU PASS!");
	}

	void RecipieFailed()
	{
		Debug.Log ("YOU FAIL!");
	}

	int currentStir = -1;
	int lastStir = -1;
	int lastLastStir = -1;
	public void Stirred(int colliderIndex)
	{
		lastLastStir = lastStir;
		lastStir = currentStir;
		currentStir = colliderIndex;

		if (IsClockwise (currentStir, lastStir) && IsClockwise (lastStir, lastLastStir) && IsClockwise (lastLastStir, currentStir)) {
			StirClockWise ();
			currentStir = -1;
		}
		if (IsCounterClockwise (currentStir, lastStir) && IsCounterClockwise (lastStir, lastLastStir) && IsCounterClockwise (lastLastStir, currentStir)) {
			StirCounterClockwise ();
			currentStir = -1;
		}
	}

	bool IsClockwise(int current, int last)
	{
		return (current == 1 && last == 0) || (current == 2 && last == 1) || (current == 0 && last == 2);
	}
	bool IsCounterClockwise(int current, int last)
	{
		return (current == 0 && last == 1) || (current == 2 && last == 0) || (current == 1 && last == 2);
	}

	public float heat;
	public void AddHeat()
	{
		heat += heatIncreasePerStick;
	}
	public float heatDecay = 1;
	public float heatIncreasePerStick = 15;
	IEnumerator HeatLoop()
	{
		while (true) {
			heat = heat - (Time.deltaTime * heatDecay);
			if (heat < 0)
				heat = 0;
			yield return null;
		}
	}

}
[System.Serializable]
public class Recipie {
	public string name;
	public RecipieStep[] steps;
	public Color finalColor;
}

[System.Serializable]
public class RecipieStep {
	//public SerializableDictionary<Ingredient.IngredientType, int> requiredIngredients;
	public List<IngredientAmount> requiredIngredients;
	public float maxStepTime = 0;
}
	
[System.Serializable]
public class IngredientAmount {
	public Ingredient.IngredientType ingredient;
	public int value;
}


	