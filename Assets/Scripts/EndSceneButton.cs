using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.VirtualClass.EscapeRoom
{
	public class EndSceneButton : Photon.MonoBehaviour
    {

        public GameObject endDoor;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Controller" && endDoor.GetComponent<EndDoor>().getBool())
            {
                PhotonNetwork.LoadLevel("EndScene");
            }
        }
    }
}
