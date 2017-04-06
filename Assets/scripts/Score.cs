using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Score : NetworkBehaviour {

	[SyncVar]
	public int score = 0;

	//Adds score to a player, serverside
	[Command]
	public void CmdAdd(int amnt) {
		//Debug.Log (score + " + " + amnt + " = " + (score + amnt));
		score += amnt;
	}

	//Creates the UI
	void Update() {
		if (isLocalPlayer) {
			Text uiText = GameObject.FindWithTag ("UI").GetComponent<Text> ();
				uiText.text = "Your score: " + score + "\n\nAll scores:\n";

			foreach (GameObject p in GameObject.FindGameObjectsWithTag ("Player")) {
				uiText.text += p.GetComponent<Score> ().score + "\n";
			}
		}
	}

}
