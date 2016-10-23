using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}

	Vector3 startPosition;

	public Text myText;
	public void DisplayMessage(string message, string message2)
	{
		StopAllCoroutines ();
		StartCoroutine (DoMessage (message, message2));
	}

	public float movementSpeed = 5;
	IEnumerator DoMessage(string message, string message2)
	{
		Debug.Log (message);
		while (transform.position.y > -1) {
			transform.Translate(Vector3.down * Time.deltaTime * movementSpeed, Space.World);
			yield return null;
		}
		myText.text = message;
		while (Vector3.Distance(transform.position, startPosition) > 0.01f) {
			transform.position = Vector3.Lerp (transform.position, startPosition, Time.deltaTime * movementSpeed);
			yield return null;
		}
		if (message2 != "") {
			yield return new WaitForSeconds (2);
			while (transform.position.y > -1) {
				transform.Translate(Vector3.down * Time.deltaTime * movementSpeed, Space.World);
				yield return null;
			}
			myText.text = message2;
			while (Vector3.Distance(transform.position, startPosition) > 0.01f) {
				transform.position = Vector3.Lerp (transform.position, startPosition, Time.deltaTime * movementSpeed);
				yield return null;
			}
		}
	}
}
