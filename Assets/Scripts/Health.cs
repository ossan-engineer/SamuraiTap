using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int life = 2;
	public int attack = 1;
	public GameObject particlePrefab;
	 // public bool isEnemy = true;

	// スクリプトの上のほうがよいかな
	// public AudioClip shotSE;
	
	// Use this for initialization
	void OnCollisionEnter2D(Collision2D coll) {

		 // SoundEffectsHelper.Instance.MakeExplosionSound();

		if (coll.gameObject.tag != gameObject.tag && coll.gameObject.tag != "Wall") {
			// coll.gameObject.SendMessage("ApplyDamage", 10);

			Health health = coll.gameObject.GetComponent<Health>();
			// Debug.Log(health);

			SoundEffectsHelper.Instance.MakeExplosionSound();

			// life -= 1;
			life -= health.attack;

			// TODO: LifeGaugeUI.SendMessage("updateLifeGauge");
			// GameObject.Find("LifeGaugeUI").SendMessage("updateLifeGauge");

			// Debug.Log (life);

			if (life <= 0) {
				Destroy(gameObject);

				if (particlePrefab != null) {
					Instantiate(particlePrefab, transform.position, transform.rotation);
					Destroy(gameObject);
				}

				SoundEffectsHelper.Instance.MakeDestroySound();
			}
		}

		
	}

}
