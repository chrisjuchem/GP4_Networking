using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	private LocalCamera localCam;
	private Vector3 defaultCameraLoc;
	private Quaternion defaultCameraRot;
	private float speed = 10;

	//Get a random color
	public void Start () {
		GetComponent<MeshRenderer> ().material.color = new Color(Random.value, Random.value, Random.value, 1);
	}

	//Attach the camera to the local player.
	public override void OnStartLocalPlayer() {
		localCam = GameObject.FindWithTag ("MainCamera").GetComponent<LocalCamera> ();
		localCam.attachToPlayer (this);
		defaultCameraLoc = localCam.transform.position;
		defaultCameraRot = localCam.transform.rotation;
	}

	//Don't destroy the child Camera
	void OnDestroy() {
		if (localCam != null) {
			localCam.transform.parent = null;
			localCam.transform.position = defaultCameraLoc;
			localCam.transform.rotation = defaultCameraRot;
			//Object.Instantiate (localCam.gameObject);
			//localCam.gameObject.SetActive (false);
			//localCam.gameObject.SetActive (true);
		}
	}


	void Update() {

		if (isLocalPlayer) {
			move ();
		}

		//TODO move to serevr side to prevent cheating.
		//If we've fallen under the map, put ourselves back on the floor.
		if (transform.position.y < -0.1f) {
			transform.Translate (0, -transform.position.y, 0);
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}


		GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, GetComponent<Rigidbody> ().velocity.y, 0);

	}

	void move () {
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
		float turn = Input.GetAxis ("Mouse X") * Time.deltaTime * 150.0f;

		transform.Translate(x, 0, z);
		transform.Rotate (0, turn, 0, Space.Self);

		//Attempt to allow independent camera movement with the mouse, and have the player move in the camera's space
		/*
		Vector3 camFacing = new Vector3(-localCam.transform.localPosition.x, 0, -localCam.transform.localPosition.z);

		if (x !=  0 || z != 0) {
			//transform.eulerAngles = Vector3.RotateTowards (transform.forward, target, 9 * Time.deltaTime, 1.0f);
			Debug.Log (x + " " + z);
		}

		transform.Translate(-localCam.transform.localPosition.x * x, 0, -localCam.transform.localPosition.z * z);
		*/

		//Jump uses energy
		if (Input.GetButtonDown("Jump")) {
			GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 50, 0), ForceMode.Impulse);
			GetComponent<Score> ().CmdAdd (-1);
		}
		if (Input.GetButton ("Drop")) {
			GetComponent<Rigidbody> ().AddForce (new Vector3 (0, -75, 0), ForceMode.Acceleration);
		}
	}
}