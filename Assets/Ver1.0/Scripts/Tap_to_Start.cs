using UnityEngine;
using System.Collections;

public class Tap_to_Start : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			Application.LoadLevel("Stage 1-1");
		}
	}
}
