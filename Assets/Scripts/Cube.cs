using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Photon.MonoBehaviour {
	public Material normalMat;
	public Material grabbedMat;
	public Material unlockedMat;

	private bool grabbed;
	private bool locked;

	//private GameObject[] collisions;
	private List<GameObject> collisions; //using list even if arrays are cheaper for add function and unknown size


	// Use this for initialization
	void Start () {
		collisions = new List<GameObject>(); 
		grabbed = false;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
		locked = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Door") {
			IsUnlocked ();
		}
		collisions.Add (col.gameObject);
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject.tag == "Door") {
			isLocked ();
		}
		collisions.Remove (col.gameObject);
	}

	public void isLocked() {
		locked = true;
		transform.Find("Mesh1").gameObject.GetComponent<Renderer> ().material = normalMat;
	}

	public void IsUnlocked(){
		locked = false;
		transform.Find("Mesh1").gameObject.GetComponent<Renderer> ().material = unlockedMat;
	}

	// Is called when the Cube gets grabbed, changes grabbed bool and appearance
	public void IsGrabbed () {
		this.photonView.RPC ("IsGrabbedRPC", PhotonTargets.All, null);
	}

	[PunRPC]
	public void IsGrabbedRPC() {
		if (!grabbed) {
			grabbed = true;
			transform.Find("Mesh1").gameObject.GetComponent<Renderer> ().material = grabbedMat;
			gameObject.GetComponent<Collider> ().isTrigger = true;
		}
	}

	public void IsDropped () {
		this.photonView.RPC ("IsDroppedRPC", PhotonTargets.All, null);
	}

	[PunRPC]
	public void IsDroppedRPC() {
		bool isHitting = false;
		for (int i = 0; i < collisions.Count; i++) {
			if (collisions [i].tag == "Pick Up") {
				isHitting = true;
				break;
			}
		}
		if (isHitting) {
			return;
		} else {
			grabbed = false;
			gameObject.GetComponent<Collider> ().isTrigger = false;
			if (locked) {
				transform.Find("Mesh1").gameObject.GetComponent<Renderer> ().material = normalMat;
			}
		}
	}
}
