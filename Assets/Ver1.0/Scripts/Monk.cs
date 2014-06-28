using UnityEngine;
using System.Collections;

public class Monk : MonoBehaviour {

	public int heal = 5;

	void OnCollisionEnter2D(Collision2D coll) {
		
		if (coll.gameObject.tag == gameObject.tag) {

			Health health = coll.gameObject.GetComponent<Health>();
			
			SoundEffectsHelper.Instance.MakeHealingSound();
			
			health.life += heal;
			Debug.Log ("healed!");

			// Get Animator
			Animator animator = coll.gameObject.GetComponent<Animator>();
			
			// Change Animator Params
			animator.SetTrigger ("Healed");

			GameObject.Find("LifeGaugeUI").SendMessage("UpdateLifeGauge");
		}
	}

}
