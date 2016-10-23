using UnityEngine;
using System.Collections;

public class Book : MonoBehaviour {
	public Recipie recipie;
	public void Open()
	{
		GetComponent<Animator> ().SetBool ("open", true);
	}
	public void Close()
	{
		GetComponent<Animator> ().SetBool ("open", false);
	}
}
