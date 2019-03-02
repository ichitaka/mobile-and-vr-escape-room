using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.VirtualClass.EscapeRoom
{
	public class Ground : MonoBehaviour {

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnTriggerExit(Collider col) {
			if(col.tag == "Pick Up") {
				col.GetComponent<Rooms> ().outOfBounds = true;
			}
		}

		void OnTriggerEnter(Collider col) {
			if(col.tag == "Pick Up") {
				col.GetComponent<Rooms> ().outOfBounds = false;
			}
		}
	}
}
