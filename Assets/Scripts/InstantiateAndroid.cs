using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAndroid : Photon.PunBehaviour {

	public GameObject androidPlayer;

	private Vector3 ghostPosition;
	private Quaternion ghostRotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake() {
		if (photonView.isMine) {
			Instantiate (androidPlayer, transform);
			
		}
		DontDestroyOnLoad (this.gameObject);
	}

	public void TeleportToWaitingzone() {
		Debug.Log ("waiting for other player");
		transform.position = new Vector3 (15f, 22f, 15f);
		//do stuff here 15,22,15
	}

	public void TeleportToPlayzone() {
		Debug.Log ("returning to playzone");
		transform.position = new Vector3 (2.8f, 7f, 0.5f);
		//do stuff here
	}

	public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer)
	{
		TeleportToPlayzone ();
	}
}
