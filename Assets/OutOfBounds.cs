using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {
	public RespawnManager respawnManager;
	public Transform cauldronCenter;
	public float cauldronRadius;
	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Paddle")) {
			if (Vector3.Distance (other.transform.position, cauldronCenter.position) < cauldronRadius)
				return;
		}
		Rigidbody otherRigidbody = other.GetComponentInParent<Rigidbody>();
		if (otherRigidbody && otherRigidbody.gameObject)
			respawnManager.Respawn (otherRigidbody.gameObject);
	}
}
