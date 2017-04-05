using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	private LocalCamera localCam;
	private float speed = 10;

	public void Start () {
		GetComponent<MeshRenderer> ().material.color = new Color(Random.value, Random.value, Random.value, 1);
	}

	public override void OnStartLocalPlayer() {

		localCam = GameObject.FindWithTag ("MainCamera").GetComponent<LocalCamera> ();
		localCam.attachToPlayer (this);
	}

	void Update() {
		if (isLocalPlayer) {
			move ();
		}

		//TODO move to serevr side;
		if (transform.position.y < -0.1f) {
			transform.Translate (0, -transform.position.y, 0);
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
		GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
	}

	void move () {
		float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
		float turn = Input.GetAxis ("Mouse X") * Time.deltaTime * 150.0f;

		transform.Translate (x, 0, z, Space.Self);
		transform.Rotate (0, turn, 0, Space.Self);

		/*
		Vector3 camFacing = new Vector3(-localCam.transform.localPosition.x, 0, -localCam.transform.localPosition.z);

		if (x !=  0 || z != 0) {
			//transform.eulerAngles = Vector3.RotateTowards (transform.forward, target, 9 * Time.deltaTime, 1.0f);
			Debug.Log (x + " " + z);
				
		}

		transform.Translate(-localCam.transform.localPosition.x * x, 0, -localCam.transform.localPosition.z * z);

		*/

		if (Input.GetButtonDown("Jump")) {
			GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 50, 0), ForceMode.Impulse);
		}
		if (Input.GetButton ("Drop")) {
			GetComponent<Rigidbody> ().AddForce (new Vector3 (0, -75, 0), ForceMode.Acceleration);
		}
	}
}