using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.VirtualClass.EscapeRoom
{
	public class Rooms : Photon.MonoBehaviour
	{
	    public Material normalMat;
	    public Material grabbedMat;
	    public Material unlockedMat;
		public bool outOfBounds;


	    private bool grabbed;
		private bool realGrab;

		private GameObject mesh;
		private Renderer renderer;
		private GameObject lamp;

		private Vector3 UIpos;

	    private List<GameObject> collisions; //using list even if arrays are cheaper for add function and unknown size

		public void SetLayer(int layer, bool includeChildren = true)
		{
			gameObject.layer = layer;
			if (includeChildren)
			{
				foreach (Transform trans in transform.GetComponentsInChildren<Transform>(true))
				{
					trans.gameObject.layer = layer;
				}
			}
		}

		void Awake() {
			collisions = new List<GameObject>();
			if (!this.name.Contains("Start")) {
				transform.localScale = new Vector3 (0.4f, 1f, 0.4f);
			}
		}

	    // Use this for initialization
	    void Start()
	    {
			UIpos = transform.position;
			lamp = transform.Find ("Lamp 6").gameObject;

			if (!this.name.Contains ("Start")) {
				outOfBounds = true;
				lamp.SetActive (false);
			} else {
				outOfBounds = false;
			}
	        grabbed = false;
			realGrab = false;
	        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
			mesh = transform.Find ("Mesh1").gameObject;
			renderer = mesh.GetComponent<Renderer> ();
/*			foreach (Transform trans in transform.GetComponentsInChildren<Transform>(true))
			{
				if(trans.name == "Lamp 6")
				{
					lamp = trans.gameObject;
					break;
				}
			}*/
	    }

	    // Update is called once per frame
	    void Update()	{
	    }

	    void OnTriggerEnter(Collider col)
	    {	
	        collisions.Add(col.gameObject);

	        if (col.gameObject.tag == "Player")
	        {
				PlayerInRoom();
			}
	    }

	    void OnTriggerExit(Collider col)
	    {
	        collisions.Remove(col.gameObject);

	        if (col.gameObject.tag == "Player")
	        {
				PlayerLeavesRoom();
			}
			if(col.gameObject.tag == "Ground") {
				//outOfBounds = true;
				//if (realGrab == false) {
				//	Reset ();
				//}
			}
	    }
		void PlayerInRoom() {
			if (PhotonNetwork.offlineMode == true) {
				PlayerInRoomRPC ();
			} else {
				photonView.RPC ("PlayerInRoomRPC", PhotonTargets.All, null);
			}
		}
	
		void PlayerLeavesRoom() {
			if (PhotonNetwork.offlineMode == true) {
				PlayerLeavesRoomRPC ();
			} else {
				photonView.RPC ("PlayerLeavesRoomRPC", PhotonTargets.All, null);
			}
		}
	
		[PunRPC]
		void PlayerInRoomRPC() {
			transform.gameObject.tag = "Untagged";
		}
	
		[PunRPC]
		void PlayerLeavesRoomRPC() {
			transform.gameObject.tag = "Pick Up";
		}

		public void Reset() {
			//move back and put scale back
			UIpos = GameManager.Instance.GivePos(name);
			transform.position = UIpos;
			if (!name.Contains ("Start")) {
				transform.localScale = new Vector3 (0.4f, 1f, 0.4f);
			}
			grabbed = false;
			realGrab = false;
			gameObject.GetComponent<Collider>().isTrigger = true;
			ChangeMaterial (normalMat);
			SetLayer (0, true);
		}

	    // Is called when the Cube gets grabbed, changes grabbed bool and appearance
	    public void IsGrabbed()
	    {
			if (!photonView.isMine) {
				photonView.RequestOwnership ();
			}
			if (PhotonNetwork.offlineMode == true) {
				IsGrabbedRPC ();
			} else {
				this.photonView.RPC ("IsGrabbedRPC", PhotonTargets.All, null);
			}
	    }

	    [PunRPC]
	    public void IsGrabbedRPC()
	    {
			realGrab = true;
			SetLayer(8);
			lamp.SetActive (false);
			if (outOfBounds) {
				GameManager.Instance.SavePos (UIpos, name);
				UIpos = Vector3.zero;
				transform.localScale = new Vector3 (1f, 1f, 1f);
			}
	        if (!grabbed)
	        {
	            grabbed = true;
				ChangeMaterial (grabbedMat);
	            gameObject.GetComponent<Collider>().isTrigger = true;
	        }
	    }

	    public void IsDropped()
	    {
			if (PhotonNetwork.offlineMode == true) {
				IsDroppedRPC ();
			} else {
			    this.photonView.RPC("IsDroppedRPC", PhotonTargets.All, null);
			}
		}

	    [PunRPC]
	    public void IsDroppedRPC() {
			realGrab = false;
			Vector3 curPosition = transform.position;
			transform.position = new Vector3 (Mathf.Round (curPosition.x), curPosition.y, Mathf.Round (curPosition.z));
			bool isHitting = false;
			if (outOfBounds) {
				Reset ();
				return;
			}
	        for (int i = 0; i < collisions.Count; i++)
	        {
				if (collisions[i].tag == "Pick Up" || collisions [i].tag == "Bounds")
	            {
	                isHitting = true;
	                break;
	            }
	        }

	        if (isHitting)
	        {
				transform.position = new Vector3 (transform.position.x, 0.3f, transform.position.z);
	            return;
	        }
	        else
	        {	
				lamp.SetActive (true);
	            grabbed = false;
				SetLayer (0);
				gameObject.GetComponent<Collider>().isTrigger = false;
				ChangeMaterial (normalMat);
	        }
	    }

		void ChangeMaterial(Material mat) {

			var mats = new Material[renderer.materials.Length];
			for (var j = 0; j < renderer.materials.Length; j++) {
				mats[j] = mat; 
			}
			renderer.materials = mats;
		}

		Vector3 getOffset(Vector3 uiPos) {
			Vector3 output = uiPos;
			if (name == "Raum1 1(Clone)") {
				output = output + Vector3.zero;
			} else if (name == "Raum5(Clone)") {
				output = output + new Vector3 (-1.2f, 0f, 0f);
			} else if (name == "Raum5.1(Clone)") {
				output = output + new Vector3 (-1.2f, 0, -0.4f);
			} else if (name == "Raum6(Clone)") {
				output = output + new Vector3 (-1.2f, 0, -0.8f);
			} else if (name == "Raum7(Close)") {
				output = output + Vector3.zero;
			}
			return output;
		}
	}
}
 