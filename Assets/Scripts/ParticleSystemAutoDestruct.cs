using UnityEngine;
using System.Collections;

public class ParticleSystemAutoDestruct : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (particleSystem.IsAlive() == false) {
			Destroy(gameObject);
		}
	}
}
