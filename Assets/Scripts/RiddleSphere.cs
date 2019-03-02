using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleSphere : MonoBehaviour {

    public GameObject chest;
    private bool chestOpen;
    public GameObject nextKey;

    private void Start()
    {
        chestOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SphereKey" && !chestOpen)
        {
            Debug.Log("Tag erkannt");
       
            chestOpen = true;
            Instantiate(nextKey, transform.parent.transform);
            chest.GetComponent<Chest>().Open();
  
      
            
        }
    }
}
