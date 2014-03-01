using UnityEngine;
using System.Collections;

public class LifeGaugeUI : MonoBehaviour {

	public Texture2D bgImage;
	public Texture2D fgImage;

	// TODO:  Convert Players' Life Sum into 1.0f
	public GameObject playerTemp;
	GameObject[] players;

	float playerEnergy =  1.0f;

	void Start () {
//		if (players == null) {
//			players = GameObject.FindGameObjectsWithTag("Player");
//		}
		UpdateLifeGauge();
	}
	
	void OnGUI () {
		
		GUI.BeginGroup (new Rect(0, 0, 252, 15));
		
		GUI.DrawTexture (new Rect(0, 0, 252, 15), bgImage, ScaleMode.StretchToFill, true, 10.0f);

		GUI.BeginGroup (new Rect(0, 0, playerEnergy * 252, 15));
		
		GUI.DrawTexture (new Rect(0, 0, 252, 15), fgImage, ScaleMode.StretchToFill, true, 10.0f);
		
		GUI.EndGroup ();
		GUI.EndGroup ();
	}
	
	void Update () {
		// playerEnergy = 0.2f;
	}

	void UpdateLifeGauge () {
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			playerEnergy += player.GetComponent<Health>().life;
			Debug.Log(player);
		}
		Debug.Log(playerEnergy);
	}
	
}
