using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalCamera : MonoBehaviour {

	public NetworkManager networkManager;
	private Player p = null;

	void Update () {
		if (p != null) {
			//float turn = Input.GetAxis ("Mouse X") * Time.deltaTime * 150.0f;
			//transform.RotateAround(p.transform.position, Vector3.up, turn);
			float tilt = Input.GetAxis ("Mouse Y") * Time.deltaTime * 150.0f;
			transform.RotateAround(p.transform.position, p.transform.TransformVector(Vector3.left), tilt);
		}
	}


	public void attachToPlayer(Player pNew) {
		this.p = pNew;
		this.transform.SetParent (p.transform);
	}
}
