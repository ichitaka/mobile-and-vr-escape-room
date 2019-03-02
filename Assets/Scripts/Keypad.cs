using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {

    private InputField input;
    public string code;
    public int maxString;
    public GameObject safeDoor;
    public GameObject handle;

    // Use this for initialization
    private void Start()
    {
        input = GetComponentInChildren<InputField>();
    }

    public void ClickKey(string character)
    {
        input.text += character;
        GetComponent<AudioSource>().Play();
    }

    public void Backspace()
    {
        if (input.text.Length > 0)
        {
            input.text = input.text.Substring(0, input.text.Length - 1);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (input.text == code)
        {
            Text text = input.transform.Find("Text").GetComponent<Text>();
            text.color = Color.green;
            open();
            Destroy(input);

        }
        if (input.text.Length > maxString)
        {


            input.text = "";

        }    
    }
    void open()
    {
        handle.gameObject.transform.Rotate(0, 0, 90);
        StartCoroutine(DoorOpen());
    }

    IEnumerator DoorOpen()
    {
        yield return new WaitForSeconds(1);
        safeDoor.gameObject.transform.Rotate(0, -41, 0);
    }
}
