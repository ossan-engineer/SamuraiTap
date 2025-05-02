using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Vector2 power = new Vector2(0, 800f); 
	// public bool isPlayer = true;
	static Vector2 playerPosition;

	void Update () {

//		if (!initShot) {
//			timer += Time.deltaTime;
//			if(timer > waitingTime){
//				//Action
//				rigidbody2D.AddForce( new Vector2(Random.Range(-500f, 500f), 1000f) );
//				initShot = true;
//			}
//		}

		if(Input.GetButtonDown("Fire1")) {
			GetComponent<Rigidbody2D>().AddForce(power);
			// rigidbody2D.AddTorque(500f);
			// Debug.Log("Fire");

			SoundEffectsHelper.Instance.MakePlayerShotSound();
		}
	
	}

}
