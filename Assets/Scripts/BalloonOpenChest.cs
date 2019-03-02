using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BalloonOpenChest : MonoBehaviour {

    public GameObject chest;
    public GameObject balloon1;
    public GameObject balloon2;
    public GameObject balloon3;
    public GameObject riddleLight;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

           if(!balloon1.GetComponent<MyBalloon>().enabled && !balloon2.GetComponent<MyBalloon>().enabled && !balloon3.GetComponent<MyBalloon>().enabled)
        {
            riddleLight.GetComponent<ChangeLightColor>().ChangeColor();
              chest.GetComponent<Chest>().Open();
        }
	}
}
