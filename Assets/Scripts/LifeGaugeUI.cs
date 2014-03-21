using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class LifeGaugeUI : MonoBehaviour {

	public Texture2D bgImage;
	public Texture2D fgImage;
	GameObject[] players;
	GameObject[] enemies;

	// float playerEnergy =  1.0f;
	float playerEnergy;
	float enemyEnergy;

	float totalEnergy;

	int screenWidth = Screen.width;
	int screenHeight = Screen.height;
	
	void Start () {
//		if (players == null) {
//			players = GameObject.FindGameObjectsWithTag("Player");
//		}
		UpdateLifeGauge();
		// Debug.Log(screenHeight.GetType());
	}
	
	void OnGUI () {

		float lifeGaugeUIHeight = screenHeight * 0.02f;
		
		GUI.BeginGroup (new Rect(0, 0, screenWidth, lifeGaugeUIHeight));
		
		GUI.DrawTexture (new Rect(0, 0, screenWidth, lifeGaugeUIHeight), bgImage, ScaleMode.StretchToFill, true, 0);

		GUI.BeginGroup (new Rect(0, 0, playerEnergy * screenWidth, lifeGaugeUIHeight));
		
		GUI.DrawTexture (new Rect(0, 0, screenWidth, lifeGaugeUIHeight), fgImage, ScaleMode.StretchToFill, true, 0);
		
		GUI.EndGroup ();
		GUI.EndGroup ();
	}
	
	void Update () {
		// playerEnergy = 0.2f;
	}

	void UpdateLifeGauge () {
		playerEnergy = 0;
		enemyEnergy = 0;
		totalEnergy = 0;
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			playerEnergy += player.GetComponent<Health>().life;
			// Debug.Log(player);
		}
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies) {
			enemyEnergy += enemy.GetComponent<Health>().life;
			// Debug.Log(enemy);
		}
		totalEnergy = playerEnergy + enemyEnergy;

		Debug.Log("Player" + playerEnergy);
		Debug.Log("Enemy" + enemyEnergy);
		Debug.Log("Total" + totalEnergy);
		playerEnergy = playerEnergy / totalEnergy;

		if (enemyEnergy <= 0) {
			GameObject.Find("Main Camera").GetComponent<SceneController>().isStageEnd = true;

			// GameObject.Find("Main Camera").SendMessage("StageClear");
			GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
			foreach (GameObject player in players) {
				player.GetComponent<Player>().enabled = false;
			}
			GameObject.Find("StageClearWrapper").SendMessage("BlowOff");
			// GameObject.Find("StageClear").SendMessage("BlowOff");

			if (GameObject.Find("Monk_01")) {
				GameObject.Find("Monk_01").GetComponent<Monk>().enabled = false;
			}
		} else if (playerEnergy <= 0) {
			// GameObject.Find("Main Camera").SendMessage("GameOver");
			GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
			GameObject.Find("GameOverWrapper").SendMessage("BlowOff");

			 // GameObject.Find("GameOver").SendMessage("BlowOff");
		}

	}
	
}
