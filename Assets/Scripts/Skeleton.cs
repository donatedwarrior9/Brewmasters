using UnityEngine;
using System.Collections;


public class Skeleton : MonoBehaviour {
	NavMeshAgent myagent;
    public enum monsterType { skeleton, zombie, pumpkinhead };
    public monsterType MonsterType;
	public GameObject []myRewardPrefab;
	public GameObject deatheffectsPrefab;
	IEnumerator Start () {
        myagent = GetComponent<NavMeshAgent> ();
		myagent.destination = FindObjectOfType<EncounterArea> ().transform.position;
		while (myagent.remainingDistance > 0.5f)
			yield return null;
		FindObjectOfType<EncounterArea> ().EnteredTrap(this);
		GetComponent<Animator> ().enabled = false;
		yield return new WaitForSeconds (3);
		Instantiate (deatheffectsPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

}
