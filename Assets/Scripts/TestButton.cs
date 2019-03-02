using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.VirtualClass.EscapeRoom
{
    public class TestButton : MonoBehaviour
    {

        public GameObject launcher;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Controller")
            {
                launcher.GetComponent<Launcher>().Connect();
            }
        }
    }
}