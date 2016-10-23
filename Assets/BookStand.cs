using UnityEngine;
using System.Collections;

public class BookStand : MonoBehaviour {

	public Transform bookPoint;
	void OnTriggerEnter(Collider other)
	{
		Book book = other.GetComponentInParent<Book> ();
		if (!book)
			return;
		book.Place (bookPoint);
	}
}
