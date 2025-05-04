using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Vector2 power = new Vector2(0, 800f); 
	// public bool isPlayer = true;
	static Vector2 playerPosition;

	void Update () {
		try {
			bool touchDetected = false;
			try {
				touchDetected = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
			} catch (System.Exception e) {
				Debug.LogWarning("タッチ入力の検出中にエラーが発生しました: " + e.Message);
				touchDetected = false;
			}
			
			bool mouseClicked = false;
			try {
				mouseClicked = Input.GetMouseButtonDown(0);
			} catch (System.Exception e) {
				Debug.LogWarning("マウス入力の検出中にエラーが発生しました: " + e.Message);
				mouseClicked = false;
			}
			
			bool buttonPressed = false;
			try {
				buttonPressed = Input.GetButtonDown("Fire1");
			} catch (System.Exception e) {
				Debug.LogWarning("ボタン入力の検出中にエラーが発生しました: " + e.Message);
				buttonPressed = false;
			}
			
			if (mouseClicked || touchDetected || buttonPressed) {
				try {
					Rigidbody2D rb = GetComponent<Rigidbody2D>();
					if (rb != null) {
						rb.AddForce(power);
						Debug.Log("プレイヤーに力を加えました: " + power);
					} else {
						Debug.LogWarning("Rigidbody2Dコンポーネントが見つかりません");
					}
					
					try {
						if (SoundEffectsHelper.Instance != null) {
							SoundEffectsHelper.Instance.MakePlayerShotSound();
						} else {
							Debug.LogWarning("SoundEffectsHelper.Instanceがnullです");
						}
					} catch (System.Exception e) {
						Debug.LogError("サウンド再生中にエラーが発生しました: " + e.Message);
					}
				} catch (System.Exception e) {
					Debug.LogError("力を加える処理中にエラーが発生しました: " + e.Message);
				}
			}
		} catch (System.Exception e) {
			Debug.LogError("Player.Update中にエラーが発生しました: " + e.Message + "\n" + e.StackTrace);
		}
	}

}
