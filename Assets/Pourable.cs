using UnityEngine;
using System.Collections;

public class Pourable : MonoBehaviour {

	public GameObject dropletPrefab;
	public Transform bottleOpening;
	public float dropDelay = 1;
	IEnumerator Start () {
		while (true) {
			if (bottleOpening.position.y < transform.position.y - 0.01f)
				Instantiate (dropletPrefab, bottleOpening.position, Quaternion.identity);
			yield return new WaitForSeconds(dropDelay);
		}
	}

}
