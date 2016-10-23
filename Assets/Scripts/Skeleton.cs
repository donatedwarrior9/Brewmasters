using UnityEngine;
using System.Collections;


public class Skeleton : MonoBehaviour {
	NavMeshAgent myagent;
    public enum monsterType { skeleton, zombie, pumpkinhead };
    public monsterType MonsterType;
	public GameObject []myRewardPrefab;
	public GameObject deatheffectsPrefab;
	Animator myAnimator;
	IEnumerator Start () {
		SetAllKinematic (true);
		myAnimator = GetComponent<Animator> ();
        myagent = GetComponent<NavMeshAgent> ();
		myagent.destination = FindObjectOfType<EncounterArea> ().transform.position;
        yield return new WaitForSeconds(1);
		while (myagent.remainingDistance > 0.5f) {
			myAnimator.SetFloat ("forward", myagent.desiredVelocity.magnitude);
			yield return null;
		}
		FindObjectOfType<EncounterArea> ().EnteredTrap(this);
		myAnimator.enabled = false;
		SetAllKinematic (false);
		yield return new WaitForSeconds (3);
		Instantiate (deatheffectsPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

	void SetAllKinematic(bool toSet)
	{
		foreach (Rigidbody body in GetComponentsInChildren<Rigidbody>())
			body.isKinematic = toSet;
	}
}
