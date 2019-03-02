using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BookRow : MonoBehaviour
{

    bool bookInRow;
    bool bookEnteredRow;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Book")
        {

            bookEnteredRow = true;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (!col.gameObject.GetComponent<VRTK_InteractableObject>().IsGrabbed())
        {
            bookInRow = true;
        }
        else
        {
            bookInRow = false;
        }
       
    }

    void OnTriggerExit(Collider col)
    {
        bookEnteredRow = false;

    }
   public bool GetBookBool()
    {
       if (bookInRow && bookEnteredRow)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
