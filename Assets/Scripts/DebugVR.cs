using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVR : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        Debug.Log("Hello VR!");
        //DontDestroyOnLoad(this.gameObject);
    }
    void OnDestroy()
    {
        print("Script was destroyed");
    }
}
