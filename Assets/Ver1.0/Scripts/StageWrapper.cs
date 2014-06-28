using UnityEngine;
using System.Collections;

public class StageWrapper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Hashtable hTable = new Hashtable();
		hTable.Add ("y", 8f);
		hTable.Add ("easeType", "easeOutQuad");
		// hTable.Add ("easeType", "easeOutBounce");
		hTable.Add ("oncomplete", "CompleteHandler");
		hTable.Add ("time", 1);
		//hTable.Add ("delay", 1);
		iTween.MoveTo(gameObject, hTable);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
