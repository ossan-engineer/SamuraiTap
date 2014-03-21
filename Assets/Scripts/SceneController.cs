using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

//	public GUITexture stageClear;
//	public GUITexture gameOver;

	// Stage Clear Flag
	public bool isStageEnd;

	public float timer;
	public float waitingTime;

	public string nextStage;

	void Start () {
//		stageClear.enabled = false;
//		gameOver.enabled = false;
		isStageEnd = false;
	}

	void Update () {
		if (isStageEnd) {
			if (Input.GetButtonDown("Fire1")) {
				Application.LoadLevel(nextStage);
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
//		Application.LoadLevel(nextStage);
//		Debug.Log("Cleared");
//	}
	
//	IEnumerator GameOver () {
//		yield return new WaitForSeconds(2);
//		// Application.LoadLevel("Title");
//		Debug.Log("GameOver");
//	}
}
