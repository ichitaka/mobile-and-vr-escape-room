using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndDoor : MonoBehaviour {

    Animator animator;
    bool open;

    private void Start()
    {
        animator = GetComponent<Animator>();
        open = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Key" && !open)
        {
            
            open = true;
            Destroy(other.gameObject);
            Open();

        }
    }

    public void Open()
    {
       
        animator.SetTrigger("Open");

    }

    public bool getBool()
    {
        return open;
    }
}
