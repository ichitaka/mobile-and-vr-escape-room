using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedGravitiy : MonoBehaviour {

    private void Awake()
    {
        GetComponent<Collider>().attachedRigidbody.Sleep();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Controller")
        {
            GetComponent<Collider>().attachedRigidbody.WakeUp();
            gameObject.transform.parent = null;
        }
    }


}
