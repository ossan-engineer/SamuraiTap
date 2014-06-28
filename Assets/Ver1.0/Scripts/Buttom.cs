using UnityEngine;
using System.Collections;

public class Buttom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (Screen.height == 960) {
			transform.position = new Vector3 (0, -4.15f, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
