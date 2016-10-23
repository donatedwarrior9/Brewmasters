using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {
	public RespawnManager respawnManager;
	void OnTriggerEnter(Collider other)
	{
		Rigidbody otherRigidbody = other.GetComponentInParent<Rigidbody>();
		if (otherRigidbody && otherRigidbody.gameObject)
			respawnManager.Respawn (otherRigidbody.gameObject);
	}
}
