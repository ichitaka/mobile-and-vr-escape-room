using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterStart : MonoBehaviour {

    bool playerEntered;


	void Start () {
        playerEntered = false;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Controller")
        {
            playerEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" )
        {
            playerEntered = false;
        }
    }

    public bool GetBool()
    {
        return playerEntered;
    }
}
