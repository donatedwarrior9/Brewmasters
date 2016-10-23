using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {
	public float delay = 5;
	IEnumerator Start () {
		yield return new WaitForSeconds (delay);
		Destruct ();
	}
	
	void Destruct()
	{
		Destroy (gameObject);
	}
}
