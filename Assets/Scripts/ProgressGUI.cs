using UnityEngine;
using System.Collections;

public class ProgressGUI : MonoBehaviour {
		
	int totalLife = 0;
	string lifeStr;
	
	void OnGUI () {
		
		lifeStr = totalLife + " pt";
		
		// Display Score on the screen
		GUI.Label( new Rect(10, 10, 100, 20), lifeStr);
		
	}
	
	//
	void AddScore (int score) {
		totalLife = totalLife + score;
	}

}
