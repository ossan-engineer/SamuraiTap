using UnityEngine;
using System.Collections;

public class Tap_to_Start : MonoBehaviour {

	Color color;

	void Start () {
		StartCoroutine("Blink");
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Fire1")) {
			Application.LoadLevel("Stage 1-1");
		}

	}

	IEnumerator Blink() {

		while (true) {
			color = guiText.color;
			color.a = 0;
			guiText.color = color;
			yield return new WaitForSeconds(0.5f);
			color.a = 0.5f;
			guiText.color = color;
			yield return new WaitForSeconds(0.5f);
		}

	}
}
