using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : Photon.MonoBehaviour {

	private bool connected;
	private GameObject vrUser;

	// Use this for initialization
	void Start () {
		connected = false;
		//DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (connected) {
			transform.position = new Vector3 (
				vrUser.transform.position.x,
				transform.position.y,
				vrUser.transform.position.z);
		}
	}

	public void ConnectToHeadset(GameObject headset) {
		if (headset != null) {
            Debug.Log("we got a headset!");
            this.photonView.RequestOwnership();
			vrUser = headset;
			connected = true;
		} else {
			Debug.Log ("Headset was not properly connected to Position Marker");
		}
	}
}
