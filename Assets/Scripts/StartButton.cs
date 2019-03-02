using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.VirtualClass.EscapeRoom
{
    public class StartButton : MonoBehaviour
    {

        public GameObject launcher;
        public GameObject CubeButton;


        private void OnTriggerEnter(Collider other)
        {
            if (CubeButton.GetComponent<EnterStart>().GetBool())
            {
                launcher.GetComponent<Launcher>().Connect();
            }
        }
    }
}