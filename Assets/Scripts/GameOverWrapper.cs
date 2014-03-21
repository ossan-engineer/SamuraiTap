using UnityEngine;
using System.Collections;

public class GameOverWrapper : MonoBehaviour {

	public bool isGameOver = false;
	// public AudioClip SEClip;

	public float timer;
	public float waitingTime = 2f;

	// Use this for initialization
	void BlowOff () {
		Hashtable hTable = new Hashtable();
		hTable.Add ("y", 8f);
		hTable.Add ("easeType", "easeOutQuad");
		// hTable.Add ("easeType", "easeOutBounce");
		hTable.Add ("oncomplete", "CompleteHandler");
		hTable.Add ("time", 1);
		// hTable.Add ("delay", 1);
		iTween.MoveTo(gameObject, hTable);
		Debug.Log ("BlowOff");

		// audio.PlayOneShot (SEClip);

		isGameOver = true;
		audio.Play();
		Debug.Log(isGameOver);
	}

	void Start () {
		// audio.clip = SEClip;
	}

	// Update is called once per frame
	void Update () {
		if (isGameOver) {

			timer += Time.deltaTime;
			if(timer > waitingTime){
				//Action
				if (Input.GetButtonDown("Fire1")) {
					Application.LoadLevel("Title");
				}
				// timer = 0;
			}
		}	
	}
}
