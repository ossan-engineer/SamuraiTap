using UnityEngine;
using UnityEngine.UI; // 新しいUIシステム用の名前空間を追加
using System.Collections;

public class LifeGaugeUI : MonoBehaviour {

	public Image backgroundImage;
	public Image fillImage;
	public RectTransform fillRectTransform;

	public Texture2D bgImageTexture;
	public Texture2D fgImageTexture;

	GameObject[] players;
	GameObject[] enemies;

	float playerEnergy;
	float enemyEnergy;
	float totalEnergy;

	private SceneController sceneController;
	private AudioSource mainCameraAudio;
	private GameObject stageClearWrapper;
	private GameObject gameOverWrapper;
	private GameObject monkObject;

	void Awake() {
		GameObject mainCamera = GameObject.FindWithTag("MainCamera");
		if (mainCamera != null) {
			sceneController = mainCamera.GetComponent<SceneController>();
			mainCameraAudio = mainCamera.GetComponent<AudioSource>();
		}

		stageClearWrapper = GameObject.Find("StageClearWrapper");
		gameOverWrapper = GameObject.Find("GameOverWrapper");
		monkObject = GameObject.Find("Monk_01");
	}
	
	void Start () {
		if (backgroundImage != null && bgImageTexture != null) {
			Sprite bgSprite = Sprite.Create(bgImageTexture, new Rect(0, 0, bgImageTexture.width, bgImageTexture.height), new Vector2(0.5f, 0.5f));
			backgroundImage.sprite = bgSprite;
			backgroundImage.color = Color.white;
		}
		
		if (fillImage != null && fgImageTexture != null) {
			Sprite fgSprite = Sprite.Create(fgImageTexture, new Rect(0, 0, fgImageTexture.width, fgImageTexture.height), new Vector2(0.5f, 0.5f));
			fillImage.sprite = fgSprite;
			fillImage.color = Color.white;
		}
		
		UpdateLifeGauge();
	}
	
	
	void Update () {
		UpdateLifeGauge();
	}

	void UpdateLifeGauge () {
		playerEnergy = 0;
		enemyEnergy = 0;
		totalEnergy = 0;
		
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			playerEnergy += player.GetComponent<Health>().life;
		}
		
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies) {
			enemyEnergy += enemy.GetComponent<Health>().life;
		}
		
		totalEnergy = playerEnergy + enemyEnergy;

		Debug.Log("Player" + playerEnergy);
		Debug.Log("Enemy" + enemyEnergy);
		Debug.Log("Total" + totalEnergy);
		
		if (totalEnergy > 0) {
			playerEnergy = playerEnergy / totalEnergy;
		} else {
			playerEnergy = 0;
		}

		if (fillImage != null) {
			fillImage.fillAmount = playerEnergy;
		}

		CheckGameState();
	}

	void CheckGameState() {
		if (enemyEnergy <= 0) {
			if (sceneController != null) {
				sceneController.isStageEnd = true;
			}

			if (mainCameraAudio != null) {
				mainCameraAudio.Stop();
			}

			foreach (GameObject player in players) {
				Player playerComponent = player.GetComponent<Player>();
				if (playerComponent != null) {
					playerComponent.enabled = false;
				}
			}

			if (stageClearWrapper != null) {
				StageClearWrapper wrapper = stageClearWrapper.GetComponent<StageClearWrapper>();
				if (wrapper != null) {
					wrapper.BlowOff();
				} else {
					stageClearWrapper.SendMessage("BlowOff", SendMessageOptions.DontRequireReceiver);
				}
			}

			if (monkObject != null) {
				Monk monkComponent = monkObject.GetComponent<Monk>();
				if (monkComponent != null) {
					monkComponent.enabled = false;
				}
			}
		} 
		else if (playerEnergy <= 0) {
			if (mainCameraAudio != null) {
				mainCameraAudio.Stop();
			}

			if (gameOverWrapper != null) {
				GameOverWrapper wrapper = gameOverWrapper.GetComponent<GameOverWrapper>();
				if (wrapper != null) {
					wrapper.BlowOff();
				} else {
					gameOverWrapper.SendMessage("BlowOff", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

}
