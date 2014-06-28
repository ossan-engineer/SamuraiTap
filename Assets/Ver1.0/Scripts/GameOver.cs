using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void BlowOff () {
		Hashtable hTable = new Hashtable();
		hTable.Add ("y", 20);
		hTable.Add ("easeType", "easeOutQuad");
		// hTable.Add ("easeType", "easeOutBounce");
		hTable.Add ("oncomplete", "CompleteHandler");
		hTable.Add ("time", 1);
		hTable.Add ("delay", 1.5f);
		iTween.MoveTo(gameObject, hTable);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
