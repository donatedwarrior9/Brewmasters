using UnityEngine;
using System.Collections;

public class StirCollider : MonoBehaviour {
	public int colliderIndex;
	public Cauldron cauldron;
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Paddle")
			cauldron.Stirred (colliderIndex);
	}
}
