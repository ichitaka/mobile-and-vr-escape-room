using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.VR;

public class InstantiatePlayer : Photon.MonoBehaviour {

	#region Public Variables

	public bool isVR;
	public GameObject vrPlayer;
	public GameObject androidPlayer;

	public static GameObject LocalPlayerInstance;

	#endregion

//	#region Photon Callbacks
//
//	void OnPhotonSerializeView(
//		PhotonStream stream,
//		PhotonMessageInfo info) {
//		if (stream.isWriting == true) {
//			stream.SendNext (isVR);
//		} else {
//			isVR = (bool)stream.ReceiveNext ();
//		}
//	}
//
//	#endregion

	void setVRPlayer () {
		this.photonView.RPC("setVRPlayerRPC", PhotonTargets.All, null);
	}

	[PunRPC]
	void setVRPlayerRPC(){
		if (photonView.isMine) {
			vrPlayer.SetActive (true);
			androidPlayer.SetActive (false);
		}
	}

	#region MonoBehaviour Callbacks

	// Use this for initialization
	void Start () {
//		if (photonView.isMine == true) {
//			if (isVR) {
//				setVRPlayer ();
//			}
//		}
		//TODO this code is just editor related, needs update!
//		#if UNITY_EDITOR_WIN
//		setVRPlayer();
//		#endif
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown (KeyCode.Space) == true) {
//			setVRPlayer();
//		}
	}

	void Awake() {
		// #Important
		// used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
		if (photonView.isMine) {
			InstantiatePlayer.LocalPlayerInstance = this.gameObject;
			bool isVR = VRDevice.isPresent;
//			if (isVR) {
//				var cam = Instantiate (vrPlayer);
//			} else {
//				var cam = Instantiate (androidPlayer);
//			}
//			cam.transform.parent = gameObject.transform;
		} else {
			//this.gameObject.SetActive (false);
		}
		// #Critical
		// we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
		DontDestroyOnLoad (this.gameObject);
	}
	#endregion
}
