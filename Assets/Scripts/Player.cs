using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Vector2 power = new Vector2(0, 1000f); 
//	bool initShot = false;
//	float timer = 0;
//	int waitingTime = 1;
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
			rigidbody2D.AddForce(power);
			// Debug.Log("Fire");

			// SoundEffectsHelper.Instance.MakePlayerShotSound();
		}

		// playerPosition = transform.position;
		// Debug.Log(playerPosition);
	}

	void Awake() {
		Application.targetFrameRate = 60;
	}

}
