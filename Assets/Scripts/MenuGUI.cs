using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class MenuGUI : MonoBehaviour {

	void OnGUI() {
		// Stage 01ボタン表示
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 0, 100, 50), "Tap to Start")) {
			// クリックされたらStage01シーンをロードする
			Application.LoadLevel("Stage 1-1");
		}
		// Stage 02ボタン表示
//		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 110, 100, 50), "Stage02")) {
//			// クリックされたらStage02シーンをロードする
//			Application.LoadLevel("Stage 1-2");
//		}
	}
}
