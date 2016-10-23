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
	Book SellPotionIAmHolding()
	{
		float closestDist = 9999;
		Book closePotion = null;
		foreach (Book potion in FindObjectsOfType<Book>()) {
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

	IEnumerator GivenPotion(Book potion)
	{
		myAnimator.SetTrigger ("leave");
		yield return new WaitForSeconds (0.25f);
		myAgent.destination = travelToPosition.position;
		yield return new WaitForSeconds (1);
		while (myAgent.remainingDistance > 0.5f)
			yield return null;
		// Decide which rewards to do?
		Destroy (potion.gameObject);

		myAgent.destination = standingPosition.position;
		yield return new WaitForSeconds (1);
		while (myAgent.remainingDistance > 0.25f)
			yield return null;
		myAnimator.SetTrigger ("place");
		yield return new WaitForSeconds (0.5f);
		// Make the rewards appear
	}
}
