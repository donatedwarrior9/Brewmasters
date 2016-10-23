using UnityEngine;
using System.Collections;

public class Book : MonoBehaviour {
	public Recipie recipie;
	public void Place(Transform bookPoint)
	{
		Joint joint = GetComponentInChildren<Joint> ();
		if (joint)
			Destroy (joint);
		transform.position = bookPoint.position;
		transform.rotation = bookPoint.rotation;
		GetComponent<Animator> ().SetBool ("open", true);
	}

	public void Remove()
	{
		
	}
}
