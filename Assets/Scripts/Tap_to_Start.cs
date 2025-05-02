using UnityEngine;
using UnityEngine.SceneManagement; // 新しいシーン管理用の名前空間を追加
using UnityEngine.UI; // 新しいUIシステム用の名前空間を追加
using System.Collections;

public class Tap_to_Start : MonoBehaviour {

	public Text tapText; // Inspectorで設定する必要あり
	Color color;

	void Start () {
		if (tapText == null) {
			tapText = GetComponentInChildren<Text>();
		}
		
		StartCoroutine("Blink");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			// Application.LoadLevel("Stage 1-1"); // 非推奨APIを削除
			SceneManager.LoadScene("Stage 1-1"); // 新しいシーン読み込みAPIを使用
		}
	}

	IEnumerator Blink() {
		while (true) {
			if (tapText != null) {
				color = tapText.color;
				color.a = 0;
				tapText.color = color;
				yield return new WaitForSeconds(0.5f);
				color.a = 0.5f;
				tapText.color = color;
				yield return new WaitForSeconds(0.5f);
			} else {
				yield break;
			}
		}
	}
}
