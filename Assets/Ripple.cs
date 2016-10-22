using UnityEngine;
using System.Collections;

public class Ripple : MonoBehaviour {
	Vector3 startScale;
	public float growSpeed = 5;
	public float growAmount = 2;
	IEnumerator Start () {
		startScale = transform.localScale;
		Vector3 desiredScale = transform.localScale * growAmount;
		float i = 0;
		while (i < 1) {
			transform.localScale = Vector3.Lerp (startScale, desiredScale, i);
			i += Time.deltaTime * growSpeed;
			yield return null;
		}
		Destroy (gameObject);
	}

}
