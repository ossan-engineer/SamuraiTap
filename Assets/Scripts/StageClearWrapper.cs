using UnityEngine;
using System.Collections;

public class StageClearWrapper : MonoBehaviour {

	// public bool isStageClear = false;

	// Use this for initialization
	public void BlowOff () {
		try {
			Hashtable hTable = new Hashtable();
			hTable.Add ("y", 8f);
			hTable.Add ("easeType", "easeOutQuad");
			// hTable.Add ("easeType", "easeOutBounce");
			// hTable.Add ("oncomplete", "CompleteHandler");
			hTable.Add ("time", 1);
			// hTable.Add ("delay", 1);
			iTween.MoveTo(gameObject, hTable);

			// isStageClear = true;
			try {
				AudioSource audioSource = GetComponent<AudioSource>();
				if (audioSource != null) {
					audioSource.Play();
				} else {
					Debug.LogWarning("AudioSourceコンポーネントが見つかりません");
				}
			} catch (System.Exception e) {
				Debug.LogError("オーディオ再生中にエラーが発生しました: " + e.Message);
			}

			Debug.Log ("BlowOff");
		} catch (System.Exception e) {
			Debug.LogError("BlowOff中にエラーが発生しました: " + e.Message + "\n" + e.StackTrace);
		}
	}
	
	void CompleteHandler() {
		Debug.Log("StageClearWrapper: アニメーション完了");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
