using UnityEngine;
using System.Collections;

public class Monk : MonoBehaviour {

	public int heal = 5;

	void OnCollisionEnter2D(Collision2D coll) {
		try {
			if (coll == null || coll.gameObject == null) {
				Debug.LogWarning("衝突オブジェクトがnullです");
				return;
			}
			
			if (coll.gameObject.tag == gameObject.tag) {
				try {
					Health health = coll.gameObject.GetComponent<Health>();
					if (health == null) {
						Debug.LogWarning("衝突オブジェクトにHealthコンポーネントがありません: " + coll.gameObject.name);
						return;
					}
					
					try {
						if (SoundEffectsHelper.Instance != null) {
							SoundEffectsHelper.Instance.MakeHealingSound();
						} else {
							Debug.LogWarning("SoundEffectsHelper.Instanceがnullです");
						}
					} catch (System.Exception e) {
						Debug.LogError("サウンド再生中にエラーが発生しました: " + e.Message);
					}
					
					health.life += heal;
					Debug.Log(coll.gameObject.name + "のライフが" + health.life + "に回復しました");
					
					try {
						Animator animator = coll.gameObject.GetComponent<Animator>();
						if (animator != null) {
							animator.SetTrigger("Healed");
							Debug.Log("Healedトリガーを設定しました");
						} else {
							Debug.LogWarning("衝突オブジェクトにAnimatorコンポーネントがありません: " + coll.gameObject.name);
						}
					} catch (System.Exception e) {
						Debug.LogError("アニメーター処理中にエラーが発生しました: " + e.Message);
					}
					
					try {
						GameObject lifeGaugeUI = GameObject.Find("LifeGaugeUI");
						if (lifeGaugeUI != null) {
							lifeGaugeUI.SendMessage("UpdateLifeGauge", SendMessageOptions.DontRequireReceiver);
							Debug.Log("LifeGaugeUIを更新しました");
						} else {
							Debug.LogWarning("LifeGaugeUIオブジェクトが見つかりません");
						}
					} catch (System.Exception e) {
						Debug.LogError("ライフゲージ更新中にエラーが発生しました: " + e.Message);
					}
				} catch (System.Exception e) {
					Debug.LogError("回復処理中にエラーが発生しました: " + e.Message);
				}
			}
		} catch (System.Exception e) {
			Debug.LogError("OnCollisionEnter2D中にエラーが発生しました: " + e.Message + "\n" + e.StackTrace);
		}
	}

}
