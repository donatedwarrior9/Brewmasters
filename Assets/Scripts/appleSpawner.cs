using UnityEngine;
using System.Collections;

public class appleSpawner : MonoBehaviour {
	public GameObject [] MyApplePrefab;
	GameObject myApple;

	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			int rand = Mathf.RoundToInt(Random.Range(0, MyApplePrefab.Length));
            myApple = (GameObject)Instantiate (MyApplePrefab[rand], transform.position, Quaternion.identity);
			while (myApple != null) {
				yield return null;
			}
			yield return new WaitForSeconds (7);
		}
	}

}
