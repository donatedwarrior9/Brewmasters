using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cauldron : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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

}
[System.Serializable]
public class Recipie {
	public string name;
	public RecipieStep[] steps;
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


	