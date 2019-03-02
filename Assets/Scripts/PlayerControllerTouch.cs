using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.VirtualClass.EscapeRoom
	{
	public class PlayerControllerTouch : MonoBehaviour {
		private Camera camera;
		private GameObject room;

		private Vector3 screenPoint;
		private Vector3 offset;

		private Cube cubeComponent;
		private Renderer cubeRen;

		// Use this for initialization
		void Start () {
			camera = Camera.main;
		}

		void Update () {
			if (Input.touchCount >0) {
				//TODO could break logic possibly if multitouch is applied!
				foreach (Touch touch in Input.touches) {
					if (touch.phase == TouchPhase.Began) {
						Ray ray = camera.ScreenPointToRay (touch.position);
						RaycastHit hit;
						if (Physics.Raycast (ray, out hit, 100f)) {
							Debug.DrawRay (transform.position, transform.forward, Color.green, 10f);
							Debug.Log (hit.collider.name);
							if (hit.collider.gameObject.tag == "Pick Up") {
								room = hit.collider.gameObject;
								room.GetComponent<Rooms> ().IsGrabbed();
								screenPoint = camera.WorldToScreenPoint(room.transform.position);
								offset = room.transform.position - camera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, screenPoint.z));
							}
						}
					}
					if (touch.phase == TouchPhase.Moved) {
						if (room != null) {
							Vector3 curScreenPoint = new Vector3 (touch.position.x, touch.position.y, screenPoint.z);
							Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
							curPosition = new Vector3 (curPosition.x, 0.1f, curPosition.z);
							room.transform.position = curPosition;
						}
					}
					if (touch.phase == TouchPhase.Ended) {
						if (room != null) {
							room.GetComponent<Rooms> ().IsDropped ();
							room = null;
						}
					}
				}
			}
		}
	}
}