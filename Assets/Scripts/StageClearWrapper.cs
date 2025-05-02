using UnityEngine;
using System.Collections;

public class StageClearWrapper : MonoBehaviour {

	// public bool isStageClear = false;

	// Use this for initialization
	public void BlowOff () {
		Hashtable hTable = new Hashtable();
		hTable.Add ("y", 8f);
		hTable.Add ("easeType", "easeOutQuad");
		// hTable.Add ("easeType", "easeOutBounce");
		hTable.Add ("oncomplete", "CompleteHandler");
		hTable.Add ("time", 1);
		// hTable.Add ("delay", 1);
		iTween.MoveTo(gameObject, hTable);

		// isStageClear = true;
		audio.Play();

		Debug.Log ("BlowOff");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
