using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : Photon.MonoBehaviour {

	public int id;

	Animator animator;

	public bool locked;
	//private GameObject room; not necessary currently
	private GameObject otherDoor;

  void Start()
    {
       
        animator = GetComponent<Animator>();
		locked = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" & !locked)
        {
			//vorerst bis Button funktioniert
			DoorControl("Open", true);
        }
        if (col.gameObject.tag == "Door")
        {
			otherDoor = col.gameObject;
            locked = false;
            //IsUnlocked ();
		}
    }


    void OnTriggerExit(Collider col)
    {
		// should be called by lock itself
        if (col.gameObject.tag == "Door")
        {
			otherDoor = null;
			//IsLocked ();
            locked = true;
           // DoorControl("Close", false);
        }

    }


    public void DoorControl(string direction, bool isStarter)
    {

		if (isStarter) {
			otherDoor.GetComponent<Doors> ().DoorControl (direction, false);
		}
        animator.SetTrigger(direction);
    }

	public void IsLocked()
	{
        photonView.RPC("IsLockedRPC", PhotonTargets.All, null);
		//room.transform.Find("Mesh1").gameObject.GetComponent<Renderer> ().material = room.GetComponent<Rooms>().normalMat;
	}

    [PunRPC]
    void IsLockedRPC()
    {
        locked = true;
    }

    public void IsUnlocked()
	{
        photonView.RPC("IsUnlockedRPC", PhotonTargets.All, null);
		//room.transform.Find("Mesh1").gameObject.GetComponent<Renderer> ().material = room.GetComponent<Rooms>().unlockedMat;
	}

    [PunRPC]
    void IsUnlockedRPC()
    {
        locked = false;
    }
}
