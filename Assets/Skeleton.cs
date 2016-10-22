using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour {
	NavMeshAgent myagent;
	public GameObject myRewardPrefab;
	public GameObject deatheffectsPrefab;
	Animator myanimator;
	IEnumerator Start () {
		myanimator = GetComponent<Animator> ();
		myagent = GetComponent<NavMeshAgent> ();
		myagent.destination = FindObjectOfType<EncounterArea> ().transform.position;
		while (myagent.remainingDistance > 0.5f) {
			myanimator.SetFloat ("forward", myagent.desiredVelocity.magnitude);
			yield return null;
		}
		FindObjectOfType<EncounterArea> ().EnteredTrap(this);
		myanimator.enabled = false;
		yield return new WaitForSeconds (3);
		Instantiate (deatheffectsPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

}
