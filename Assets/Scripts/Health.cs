using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int life = 2;
	public int attack = 1;
	public GameObject particlePrefab;
	public bool isPlayer = false;

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D coll) {
		try {
			if (coll == null || coll.gameObject == null) {
				Debug.LogWarning("衝突オブジェクトがnullです");
				return;
			}

			if (coll.gameObject.tag != gameObject.tag && coll.gameObject.tag != "Wall") {
				try {
					Health health = coll.gameObject.GetComponent<Health>();
					if (health == null) {
						Debug.LogWarning("衝突オブジェクトにHealthコンポーネントがありません: " + coll.gameObject.name);
						return;
					}

					try {
						if (SoundEffectsHelper.Instance != null) {
							SoundEffectsHelper.Instance.MakeExplosionSound();
						} else {
							Debug.LogWarning("SoundEffectsHelper.Instanceがnullです");
						}
					} catch (System.Exception e) {
						Debug.LogError("サウンド再生中にエラーが発生しました: " + e.Message);
					}

					life -= health.attack;
					Debug.Log(gameObject.name + "のライフが" + life + "になりました");

					if (life <= 0) {
						life = 0;
						
						try {
							if (particlePrefab != null) {
								Instantiate(particlePrefab, transform.position, transform.rotation);
							}
						} catch (System.Exception e) {
							Debug.LogError("パーティクル生成中にエラーが発生しました: " + e.Message);
						}
						
						try {
							if (SoundEffectsHelper.Instance != null) {
								if (gameObject.GetComponent<Boss>() != null) {
									SoundEffectsHelper.Instance.MakeBossDestroySound();
								} else {
									SoundEffectsHelper.Instance.MakeDestroySound();
								}
							}
						} catch (System.Exception e) {
							Debug.LogError("サウンド再生中にエラーが発生しました: " + e.Message);
						}

						if (isPlayer) {
							Debug.Log("Player Died: " + gameObject.name);
							try {
								if (gameObject.name == "Samurai_01") {
									Debug.Log("Samurai Died");
									GameObject ninja = GameObject.Find("Ninja_01");
									if (ninja != null) {
										Debug.Log("Ninja Activated");
										Player ninjaPlayer = ninja.GetComponent<Player>();
										if (ninjaPlayer != null) {
											ninjaPlayer.enabled = true;
										}
									} else {
										GameObject monk = GameObject.Find("Monk_01");
										if (monk != null) {
											Debug.Log("Monk Activated");
											Player monkPlayer = monk.GetComponent<Player>();
											if (monkPlayer != null) {
												monkPlayer.enabled = true;
											}
										}
									}
								} else if (gameObject.name == "Ninja_01") {
									Debug.Log("Ninja Died");
									GameObject samurai = GameObject.Find("Samurai_01");
									if (samurai == null) {
										GameObject monk = GameObject.Find("Monk_01");
										if (monk != null) {
											Debug.Log("Monk Activated");
											Player monkPlayer = monk.GetComponent<Player>();
											if (monkPlayer != null) {
												monkPlayer.enabled = true;
											}
										}
									}
								}
							} catch (System.Exception e) {
								Debug.LogError("プレイヤー切り替え中にエラーが発生しました: " + e.Message);
							}
						}
						
						try {
							Destroy(gameObject);
						} catch (System.Exception e) {
							Debug.LogError("オブジェクト破壊中にエラーが発生しました: " + e.Message);
						}
					}

					try {
						GameObject lifeGaugeUI = GameObject.Find("LifeGaugeUI");
						if (lifeGaugeUI != null) {
							lifeGaugeUI.SendMessage("UpdateLifeGauge", SendMessageOptions.DontRequireReceiver);
						} else {
							Debug.LogWarning("LifeGaugeUIオブジェクトが見つかりません");
						}
					} catch (System.Exception e) {
						Debug.LogError("ライフゲージ更新中にエラーが発生しました: " + e.Message);
					}
				} catch (System.Exception e) {
					Debug.LogError("衝突処理中にエラーが発生しました: " + e.Message);
				}
			}
		} catch (System.Exception e) {
			Debug.LogError("OnCollisionEnter2D中にエラーが発生しました: " + e.Message + "\n" + e.StackTrace);
		}
	}
}
