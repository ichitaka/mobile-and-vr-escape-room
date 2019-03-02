using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleCube : MonoBehaviour {

    public GameObject chest;
    public GameObject nextKey;
    private bool chestOpen;

    private void Start()
    {
        chestOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CubeKey" && !chestOpen)
        {
            Debug.Log("Tag erkannt");
       
            chestOpen = true;

            Instantiate(nextKey, transform.parent.transform);
            chest.GetComponent<Chest>().Open();

            
        }
    }
}
