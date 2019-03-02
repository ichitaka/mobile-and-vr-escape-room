using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorlock : MonoBehaviour {

	public int id;

	private GameObject door;

	// Use this for initialization
	void Start () {
		door = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	} 

	void onTriggerEnter (Collider other) {
//		Debug.Log ("Collision!");
//		if (other.gameObject.tag == "Door") {
//			Debug.Log ("Unlocked!");
//			room.GetComponent<Rooms> ().IsUnlocked ();
//		}
	}

	void onTriggerExit(Collider other) {
//		Debug.Log ("Exit!");
//		if (other.gameObject.tag == "Door") {
//			Debug.Log ("Locked!");
//			room.GetComponent<Rooms> ().isLocked ();
//		}
	}

	public void isUnlocked () {
		door.GetComponent<Doors> ().IsUnlocked ();
	}

	public void IsLocked() {
		door.GetComponent<Doors> ().IsLocked ();
	}
}
