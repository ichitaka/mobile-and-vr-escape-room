using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateVRPlayer : Photon.PunBehaviour{

	public GameObject vrPlayer;
    public GameObject positionMarker;

	private GameObject eyes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Awake() {
		if (photonView.isMine) {
			GameObject player = Instantiate (vrPlayer, transform);
            GameObject positionMark = GameObject.Find("PositionMarker");// PhotonNetwork.Instantiate(positionMarker.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            if(positionMark != null)
            {
                Debug.Log("PositionMark found!");
            }
            GameObject eyes = null;
            foreach (Transform trans in transform.GetComponentsInChildren<Transform>(true))
            {
                if(trans.name == "Camera (eye)")
                {
                    eyes = trans.gameObject;
                    break;
                }
            }
            
            if (eyes != null)
            {
                positionMark.GetComponent<FollowHead>().ConnectToHeadset(eyes);
            } else
            {
                Debug.Log("eyes null!");
            }
			//cam.transform.parent = gameObject.transform;
		}
		DontDestroyOnLoad (this.gameObject);
	}

	public void TeleportToWaitingzone() {
		//do stuff here
		Debug.Log ("waiting for other player");
		transform.position = new Vector3 (15f, 15f, 14.37f);
	}

	public void TeleportToPlayzone() {
		//do stuff here
		Debug.Log ("returning to playzone");
		transform.position = new Vector3 (0.5f, 0f, 0.07f);
	}

	public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer) {
		TeleportToPlayzone ();
	}

	public void connectPositionMarker(FollowHead marker) {
        Debug.Log(eyes.name);
		marker.ConnectToHeadset (eyes);
	}
}
