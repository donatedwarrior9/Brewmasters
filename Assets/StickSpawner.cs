using UnityEngine;
using System.Collections;

public class StickSpawner : MonoBehaviour {
	public GameObject myStickPrefab;
	GameObject myStick;

	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			myStick = (GameObject)Instantiate (myStickPrefab, transform.position, Quaternion.Euler (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360)));
			while (myStick != null) {
				yield return null;
			}
			yield return new WaitForSeconds (2);
		}
	}

}
