using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 angle = new Vector3(1, 0);
		Vector2 position = angle * Mathf.Sin (Time.time) / 60;
		// Debug.Log(Time.time);
		transform.Translate(position, Space.World);
	}
}
