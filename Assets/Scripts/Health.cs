using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int life = 2;
	public int attack = 1;
	public GameObject particlePrefab;
	public bool isPlayer = false;

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D coll) {

		 // SoundEffectsHelper.Instance.MakeExplosionSound();

		if (coll.gameObject.tag != gameObject.tag && coll.gameObject.tag != "Wall") {

			Health health = coll.gameObject.GetComponent<Health>();

			SoundEffectsHelper.Instance.MakeExplosionSound();

			life -= health.attack;

//			GameObject.Find("LifeGaugeUI").SendMessage("UpdateLifeGauge");

			if (life <= 0) {
				life = 0;
				Destroy(gameObject);

				if (particlePrefab != null) {
					Instantiate(particlePrefab, transform.position, transform.rotation);
					Destroy(gameObject);
				}

				if (gameObject.GetComponent<Boss>()) {
					SoundEffectsHelper.Instance.MakeBossDestroySound();
				} else {
					SoundEffectsHelper.Instance.MakeDestroySound();
				}

				if (isPlayer) {
					Debug.Log("Player Died");
					if (gameObject.name == "Samurai_01") {
						Debug.Log("Samurai Died");
						if (GameObject.Find("Ninja_01")) {
							Debug.Log("Ninja Activated");	
							GameObject.Find("Ninja_01").GetComponent<Player>().enabled = true;
						} else if (GameObject.Find("Monk_01")) {
							Debug.Log("Monk Activated");	
							GameObject.Find("Monk_01").GetComponent<Player>().enabled = true;
						}
					} else if (gameObject.name == "Ninja_01") {
						Debug.Log("Ninja Died");
						if (!GameObject.Find("Samurai_01") && GameObject.Find("Monk_01")) {
							Debug.Log("Monk Activated");	
							GameObject.Find("Monk_01").GetComponent<Player>().enabled = true;
						}
					}
				}
			}
			GameObject.Find("LifeGaugeUI").SendMessage("UpdateLifeGauge");
		}
//		} else if (coll.gameObject.tag == "Wall") {
//			SoundEffectsHelper.Instance.MakeNoDamageSound();
//		}
	}
}