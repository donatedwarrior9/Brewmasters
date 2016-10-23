using UnityEngine;
using System.Collections;

public class BookStand : MonoBehaviour {

	public ParticleSystem sparkleSystem;
	ParticleSystem.EmissionModule sparkleEmission;
	public Cauldron cauldron;
	void Start()
	{
		sparkleEmission = sparkleSystem.emission;
	}
	void OnTriggerEnter(Collider other)
	{
		Book book = other.GetComponentInParent<Book> ();
		if (!book)
			return;
		//book.Place (bookPoint);
	}

	Book ClosestBook()
	{
		float closestDist = 9999;
		Book closeBook = null;
		foreach (Book book in FindObjectsOfType<Book>()) {
			float dist = Vector3.Distance (book.transform.position, transform.position);
			if (dist < closestDist) {
				closestDist = dist;
				closeBook = book;
			}
		}
		return closeBook;
	}
	public void OnObjectSnapped()
	{
		sparkleEmission.enabled = false;
		Book closestbook = ClosestBook ();
		cauldron.SelectBook (closestbook);
		closestbook.Open ();
	}

	public void OnObjectUnSnapped()
	{
		sparkleEmission.enabled = true;
		cauldron.DeselectBook ();
		Book closestbook = ClosestBook ();
		closestbook.Close ();
		closestbook.GetComponent<Rigidbody> ().AddForce (Vector3.up * 1 + transform.up * 1, ForceMode.VelocityChange);
	}

	public VRTK.VRTK_SnapDropZone snapZone;

	public void UnequipBook()
	{
		snapZone.ForceUnsnap ();
	}

}
