using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Debug.Log("Blamo");
	}

	void OnHit(Player p) {
	}
}
