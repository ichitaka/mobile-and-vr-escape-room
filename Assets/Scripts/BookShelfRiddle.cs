using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfRiddle : MonoBehaviour {

    Animator animator;
    bool alreadyOpen;
    public GameObject row1;
    public GameObject row2;
    public GameObject row3;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        alreadyOpen = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (InShelf() && !alreadyOpen)
        {
            alreadyOpen = true;
            animator.SetTrigger("Slide");
        }
	}

    private bool InShelf()
    {
        if (row1.GetComponent<BookRow>().GetBookBool()&& row2.GetComponent<BookRow>().GetBookBool()&& row3.GetComponent<BookRow>().GetBookBool())
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }
}
