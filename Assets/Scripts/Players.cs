using UnityEngine;
using System.Collections;

public class Players : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Hashtable hTable = new Hashtable();
		hTable.Add ("y", 2);
		hTable.Add ("easeType", "easeOutQuad");
		// hTable.Add ("easeType", "easeOutBounce");
		hTable.Add ("oncomplete", "CompleteHandler");
		hTable.Add ("time", 1);
		// hTable.Add ("delay", .4f);
		iTween.MoveTo(gameObject, hTable);
		// iTween.ScaleTo(gameObject, hTable);

		// SoundEffectsHelper.Instance.MakeDashSound();

		GetComponent<AudioSource>().Play();
		
	}
	
	void CompleteHandler () {
		GameObject buttom = GameObject.Find("Buttom");
		buttom.GetComponent<BoxCollider2D>().enabled = true;;
		//SoundEffectsHelper.Instance.Destroy();

		GetComponent<AudioSource>().Stop();
	}

}
