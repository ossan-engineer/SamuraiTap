using UnityEngine;
using UnityEngine.SceneManagement; // 新しいシーン管理用の名前空間を追加
using System.Collections;

public class SceneController : MonoBehaviour {

//	public GUITexture stageClear;
//	public GUITexture gameOver;

	// Stage Clear Flag
	public bool isStageEnd;

	public float timer;
	public float waitingTime = 2f;

	public string nextStage;

	void Start () {
//		stageClear.enabled = false;
//		gameOver.enabled = false;
		//isStageEnd = false;
	}

	void Update () {

		if (isStageEnd) {

			timer += Time.deltaTime;
			if(timer > waitingTime){
				if (Input.GetButtonDown("Fire1")) {
					// Application.LoadLevel(nextStage); // 非推奨APIを削除
					SceneManager.LoadScene(nextStage); // 新しいシーン読み込みAPIを使用
				}
//				// timer = 0;
			}
		}
	}

//	void Update(){
//		timer += Time.deltaTime;
//		if(timer > waitingTime){
//			//Action
//			timer = 0;
//		}
//	}

//	IEnumerator StageClear() {
//		yield return new WaitForSeconds(2.5f);
//		// Application.LoadLevel(nextStage); // 非推奨APIを削除
//		Debug.Log("Cleared");
//	}
	
//	IEnumerator GameOver () {
//		yield return new WaitForSeconds(2);
//		// Application.LoadLevel("Title"); // 非推奨APIを削除
//		Debug.Log("GameOver");
//	}
}
