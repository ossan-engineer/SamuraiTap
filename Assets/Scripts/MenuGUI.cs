﻿using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager用の名前空間を追加
using System.Collections;

[ExecuteInEditMode]
public class MenuGUI : MonoBehaviour {

	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 0, 100, 50), "Tap to Start")) {
			SceneManager.LoadScene("Stage 1-1");
		}
	}
}
