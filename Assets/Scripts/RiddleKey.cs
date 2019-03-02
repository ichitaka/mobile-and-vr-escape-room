using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleKey : MonoBehaviour {

    public GameObject chest;
    private bool chestOpen;
    public GameObject nextKey;

    private void Start()
    {
        chestOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RiddleKey" && !chestOpen)
        {
            Debug.Log("Tag erkannt");
       
            chestOpen = true;
            Instantiate(nextKey, transform.parent.transform);
            chest.GetComponent<Chest>().Open();

            
      
            
        }
    }
}
