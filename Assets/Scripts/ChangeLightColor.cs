using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightColor : MonoBehaviour {

    public Color colorEnd = Color.green;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    public void ChangeColor () {
        rend.material.color = colorEnd;
	}
}
