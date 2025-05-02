using UnityEngine;
using System.Collections;

public class ParticleSystemAutoDestruct : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (GetComponent<ParticleSystem>().IsAlive() == false) {
			Destroy(gameObject);
		}
	}
}
