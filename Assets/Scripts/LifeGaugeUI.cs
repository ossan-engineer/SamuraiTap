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
		Debug.Log("LifeGaugeUI Awake: 初期化開始");
		
		GameObject mainCamera = GameObject.FindWithTag("MainCamera");
		if (mainCamera != null) {
			sceneController = mainCamera.GetComponent<SceneController>();
			mainCameraAudio = mainCamera.GetComponent<AudioSource>();
		}

		stageClearWrapper = GameObject.Find("StageClearWrapper");
		gameOverWrapper = GameObject.Find("GameOverWrapper");
		monkObject = GameObject.Find("Monk_01");
		
		if (bgImageTexture == null) {
			Debug.LogWarning("bgImageTextureがnullです。Resources/Imagesから読み込みます。");
			bgImageTexture = Resources.Load<Texture2D>("Images/Blue");
			
			if (bgImageTexture == null) {
				Debug.LogWarning("Resources/Images/Blueが見つかりません。Assets/Imagesから読み込みます。");
				string[] bgFiles = System.IO.Directory.GetFiles(Application.dataPath + "/Images", "Blue.png", System.IO.SearchOption.AllDirectories);
				if (bgFiles.Length > 0) {
					string path = bgFiles[0].Replace(Application.dataPath, "Assets");
					bgImageTexture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(path);
					Debug.Log("背景テクスチャを読み込みました: " + path);
				}
			}
		}
		
		if (fgImageTexture == null) {
			Debug.LogWarning("fgImageTextureがnullです。Resources/Imagesから読み込みます。");
			fgImageTexture = Resources.Load<Texture2D>("Images/Red");
			
			if (fgImageTexture == null) {
				Debug.LogWarning("Resources/Images/Redが見つかりません。Assets/Imagesから読み込みます。");
				string[] fgFiles = System.IO.Directory.GetFiles(Application.dataPath + "/Images", "Red.png", System.IO.SearchOption.AllDirectories);
				if (fgFiles.Length > 0) {
					string path = fgFiles[0].Replace(Application.dataPath, "Assets");
					fgImageTexture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(path);
					Debug.Log("前景テクスチャを読み込みました: " + path);
				}
			}
		}
		
		Debug.Log("LifeGaugeUI Awake: 初期化完了");
	}
	
	void Start () {
		Debug.Log("LifeGaugeUI Start: 初期化開始");
		
		Canvas canvas = GetComponent<Canvas>();
		if (canvas == null) {
			Debug.LogWarning("Canvasが見つかりません。親オブジェクトに追加します。");
			canvas = gameObject.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			gameObject.AddComponent<CanvasScaler>();
			gameObject.AddComponent<GraphicRaycaster>();
		}
		
		GameObject lifeGaugePanel = transform.Find("LifeGaugePanel")?.gameObject;
		if (lifeGaugePanel == null) {
			Debug.LogWarning("LifeGaugePanelが見つかりません。新しく作成します。");
			lifeGaugePanel = new GameObject("LifeGaugePanel");
			lifeGaugePanel.transform.SetParent(transform);
			
			RectTransform panelRect = lifeGaugePanel.AddComponent<RectTransform>();
			panelRect.anchorMin = new Vector2(0.1f, 0.9f);
			panelRect.anchorMax = new Vector2(0.9f, 0.95f);
			panelRect.offsetMin = Vector2.zero;
			panelRect.offsetMax = Vector2.zero;
		}
		
		if (backgroundImage == null) {
			Debug.LogWarning("backgroundImageがnullです。自動的に作成します。");
			GameObject bgObj = new GameObject("BackgroundImage");
			bgObj.transform.SetParent(lifeGaugePanel.transform);
			backgroundImage = bgObj.AddComponent<Image>();
			RectTransform bgRect = bgObj.GetComponent<RectTransform>();
			bgRect.anchorMin = new Vector2(0, 0);
			bgRect.anchorMax = new Vector2(1, 1);
			bgRect.offsetMin = Vector2.zero;
			bgRect.offsetMax = Vector2.zero;
		}
		
		Debug.Log("backgroundImage: " + backgroundImage.name);
		
		backgroundImage.color = new Color(0.2f, 0.2f, 0.2f, 1f);
		backgroundImage.raycastTarget = false;
		
		if (bgImageTexture != null) {
			Debug.Log("bgImageTexture: " + bgImageTexture.name + " サイズ: " + bgImageTexture.width + "x" + bgImageTexture.height);
			try {
				Sprite bgSprite = Sprite.Create(bgImageTexture, new Rect(0, 0, bgImageTexture.width, bgImageTexture.height), new Vector2(0.5f, 0.5f));
				backgroundImage.sprite = bgSprite;
				backgroundImage.type = Image.Type.Simple;
				backgroundImage.color = Color.white; // テクスチャを表示するために色を白に設定
				Debug.Log("背景画像の設定完了");
			} catch (System.Exception e) {
				Debug.LogError("背景画像の設定中にエラー: " + e.Message);
				backgroundImage.color = new Color(0.2f, 0.2f, 0.2f, 1f);
			}
		} else {
			Debug.LogWarning("bgImageTextureがnullです。単色で表示します。");
			backgroundImage.color = new Color(0.2f, 0.2f, 0.2f, 1f);
		}
		
		if (fillImage == null) {
			Debug.LogWarning("fillImageがnullです。自動的に作成します。");
			GameObject fillObj = new GameObject("FillImage");
			fillObj.transform.SetParent(lifeGaugePanel.transform);
			fillImage = fillObj.AddComponent<Image>();
			fillRectTransform = fillObj.GetComponent<RectTransform>();
			fillRectTransform.anchorMin = new Vector2(0, 0);
			fillRectTransform.anchorMax = new Vector2(1, 1);
			fillRectTransform.offsetMin = new Vector2(2, 2);
			fillRectTransform.offsetMax = new Vector2(-2, -2);
		}
		
		Debug.Log("fillImage: " + fillImage.name);
		
		fillImage.color = new Color(1f, 0.2f, 0.2f, 1f);
		fillImage.raycastTarget = false;
		fillImage.type = Image.Type.Filled;
		fillImage.fillMethod = Image.FillMethod.Horizontal;
		fillImage.fillOrigin = (int)Image.OriginHorizontal.Left;
		
		if (fgImageTexture != null) {
			Debug.Log("fgImageTexture: " + fgImageTexture.name + " サイズ: " + fgImageTexture.width + "x" + fgImageTexture.height);
			try {
				Sprite fgSprite = Sprite.Create(fgImageTexture, new Rect(0, 0, fgImageTexture.width, fgImageTexture.height), new Vector2(0.5f, 0.5f));
				fillImage.sprite = fgSprite;
				fillImage.color = Color.white; // テクスチャを表示するために色を白に設定
				Debug.Log("塗りつぶし画像の設定完了");
			} catch (System.Exception e) {
				Debug.LogError("塗りつぶし画像の設定中にエラー: " + e.Message);
				fillImage.color = new Color(1f, 0.2f, 0.2f, 1f);
			}
		} else {
			Debug.LogWarning("fgImageTextureがnullです。単色で表示します。");
			fillImage.color = new Color(1f, 0.2f, 0.2f, 1f);
		}
		
		// RectTransformの確認
		if (fillRectTransform == null) {
			Debug.LogWarning("fillRectTransformがnullです。fillImageから取得します。");
			fillRectTransform = fillImage.GetComponent<RectTransform>();
		}
		
		Debug.Log("fillRectTransform サイズ: " + fillRectTransform.rect.width + "x" + fillRectTransform.rect.height);
		
		fillImage.fillAmount = 1.0f;
		
		UpdateLifeGauge();
		Debug.Log("LifeGaugeUI Start: 初期化完了");
	}
	
	
	void Update () {
		UpdateLifeGauge();
	}

	void UpdateLifeGauge () {
		try {
			playerEnergy = 0;
			enemyEnergy = 0;
			totalEnergy = 0;
			
			players = GameObject.FindGameObjectsWithTag("Player");
			if (players != null && players.Length > 0) {
				foreach (GameObject player in players) {
					if (player != null) {
						Health health = player.GetComponent<Health>();
						if (health != null) {
							playerEnergy += health.life;
						}
					}
				}
			}
			
			enemies = GameObject.FindGameObjectsWithTag("Enemy");
			if (enemies != null && enemies.Length > 0) {
				foreach (GameObject enemy in enemies) {
					if (enemy != null) {
						Health health = enemy.GetComponent<Health>();
						if (health != null) {
							enemyEnergy += health.life;
						}
					}
				}
			}
			
			totalEnergy = playerEnergy + enemyEnergy;

			Debug.Log("Player: " + playerEnergy);
			Debug.Log("Enemy: " + enemyEnergy);
			Debug.Log("Total: " + totalEnergy);
			
			if (totalEnergy > 0) {
				playerEnergy = playerEnergy / totalEnergy;
			} else {
				playerEnergy = 0;
			}

			if (fillImage != null) {
				fillImage.fillAmount = playerEnergy;
			}

			CheckGameState();
		} catch (System.Exception e) {
			Debug.LogError("UpdateLifeGauge中にエラーが発生しました: " + e.Message + "\n" + e.StackTrace);
		}
	}

	void CheckGameState() {
		try {
			if (enemyEnergy <= 0) {
				Debug.Log("敵のエネルギーがゼロになりました - ステージクリア");
				
				if (sceneController != null) {
					sceneController.isStageEnd = true;
					Debug.Log("SceneController.isStageEndをtrueに設定しました");
				} else {
					Debug.LogWarning("sceneControllerがnullです");
				}

				if (mainCameraAudio != null) {
					mainCameraAudio.Stop();
					Debug.Log("メインカメラのオーディオを停止しました");
				} else {
					Debug.LogWarning("mainCameraAudioがnullです");
				}

				if (players != null && players.Length > 0) {
					foreach (GameObject player in players) {
						if (player != null) {
							Player playerComponent = player.GetComponent<Player>();
							if (playerComponent != null) {
								playerComponent.enabled = false;
								Debug.Log("プレイヤーコンポーネントを無効化しました: " + player.name);
							}
						}
					}
				}

				if (stageClearWrapper != null) {
					try {
						StageClearWrapper wrapper = stageClearWrapper.GetComponent<StageClearWrapper>();
						if (wrapper != null) {
							wrapper.BlowOff();
							Debug.Log("StageClearWrapper.BlowOff()を実行しました");
						} else {
							Debug.LogWarning("StageClearWrapperコンポーネントが見つかりません。SendMessageを使用します。");
							stageClearWrapper.SendMessage("BlowOff", SendMessageOptions.DontRequireReceiver);
						}
					} catch (System.Exception e) {
						Debug.LogError("StageClearWrapperの処理中にエラーが発生しました: " + e.Message);
					}
				} else {
					Debug.LogWarning("stageClearWrapperがnullです");
				}

				if (monkObject != null) {
					try {
						Monk monkComponent = monkObject.GetComponent<Monk>();
						if (monkComponent != null) {
							monkComponent.enabled = false;
							Debug.Log("Monkコンポーネントを無効化しました");
						}
					} catch (System.Exception e) {
						Debug.LogError("Monkコンポーネントの処理中にエラーが発生しました: " + e.Message);
					}
				}
			} 
			else if (playerEnergy <= 0) {
				Debug.Log("プレイヤーのエネルギーがゼロになりました - ゲームオーバー");
				
				if (mainCameraAudio != null) {
					mainCameraAudio.Stop();
					Debug.Log("メインカメラのオーディオを停止しました");
				} else {
					Debug.LogWarning("mainCameraAudioがnullです");
				}

				if (gameOverWrapper != null) {
					try {
						GameOverWrapper wrapper = gameOverWrapper.GetComponent<GameOverWrapper>();
						if (wrapper != null) {
							wrapper.BlowOff();
							Debug.Log("GameOverWrapper.BlowOff()を実行しました");
						} else {
							Debug.LogWarning("GameOverWrapperコンポーネントが見つかりません。SendMessageを使用します。");
							gameOverWrapper.SendMessage("BlowOff", SendMessageOptions.DontRequireReceiver);
						}
					} catch (System.Exception e) {
						Debug.LogError("GameOverWrapperの処理中にエラーが発生しました: " + e.Message);
					}
				} else {
					Debug.LogWarning("gameOverWrapperがnullです");
				}
			}
		} catch (System.Exception e) {
			Debug.LogError("CheckGameState中にエラーが発生しました: " + e.Message + "\n" + e.StackTrace);
		}
	}

}
