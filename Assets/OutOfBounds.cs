using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		// Something fell out of bounds

		Debug.Log (other.gameObject.name);
	}
}
