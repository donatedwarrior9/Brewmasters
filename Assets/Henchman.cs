using UnityEngine;
using System.Collections;

public class Henchman : MonoBehaviour {

	public void GivenPotion()
	{
		// He walks away
		// He comes back
		// He puts new things down (defined in the sell potion)
		StartCoroutine(GivenPotion(SellPotionIAmHolding()));
	}

	// CHANGE THIS TO TYPE SELLPOTION (not book)
	SellPotion SellPotionIAmHolding()
	{
		float closestDist = 9999;
		SellPotion closePotion = null;
		foreach (SellPotion potion in FindObjectsOfType<SellPotion>()) {
			float dist = Vector3.Distance (potion.transform.position, transform.position);
			if (dist < closestDist) {
				closestDist = dist;
				closePotion = potion;
			}
		}
		return closePotion;
	}


	public Animator myAnimator;
	public NavMeshAgent myAgent;
	public Transform standingPosition;
	public Transform travelToPosition;
	IEnumerator GivenPotion(SellPotion potion)
	{
		potion.transform.parent = transform;
		myAnimator.SetTrigger ("leave");
		yield return new WaitForSeconds (0.25f);
		myAgent.destination = travelToPosition.position;
		yield return new WaitForSeconds (1);
		while (myAgent.remainingDistance > 0.5f)
			yield return null;
		potion.gameObject.SetActive (false);
		myAgent.destination = standingPosition.position;
		yield return new WaitForSeconds (1);
		while (myAgent.remainingDistance > 0.25f)
			yield return null;
		myAnimator.SetTrigger ("place");
		yield return new WaitForSeconds (0.5f);
		foreach (GameObject rewardPrefab in potion.lootToReturn) {
			Instantiate (rewardPrefab, travelToPosition.position + Vector3.up, Quaternion.identity); 
		}
		Destroy (potion.gameObject);
	}
}
