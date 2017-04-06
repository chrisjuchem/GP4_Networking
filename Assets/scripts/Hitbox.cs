using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

	//When we hit something, give it score if it's a player
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && gameObject.transform.parent.gameObject.GetComponent<Player>().isLocalPlayer) {
			other.gameObject.GetComponent<Score> ().CmdAdd (30);
		} 
	}
}
