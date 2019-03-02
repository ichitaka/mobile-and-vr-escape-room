using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.VirtualClass.EscapeRoom
{
    public class doorButton : Photon.MonoBehaviour
    {

        public Material normalMat;
        public Material unlockedMat;
        public Material lockedMat;

        private Doors door;
        private Renderer renderer;

        // Use this for initialization
        void Start()
        {
            door = transform.parent.gameObject.GetComponent<Doors>();
            renderer = gameObject.GetComponent<Renderer>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Controller")
            {
                if (door.locked == false)
                {
                    door.GetComponent<Doors> ().DoorControl ("Open", true);
                  //  StartCoroutine(MoveOverSeconds(1f));
                    renderer.material = unlockedMat;
                }
                else
                {
                    renderer.material = lockedMat;
                }
            }
        }


        void OnTriggerExit(Collider col)
        {
            if (col.tag == "Controller")
            {
                renderer.material = normalMat;
            }
        }

        public IEnumerator MoveOverSeconds(float seconds)
        {
            Rooms room = transform.parent.parent.GetComponent<Rooms>();
            if (room != null)
            {
                if (!room.photonView.isMine)
                {
                    transform.parent.parent.GetComponent<Rooms>().photonView.RequestOwnership();
                }
            }
            float elapsedTime = 0;
            GameObject right = door.transform.Find("Right").gameObject;
            GameObject left = door.transform.Find("Left").gameObject;
            Vector3 startingPosRight = right.transform.position;
            Vector3 startingPosLeft = left.transform.position;

            Vector3 endPosRight = new Vector3(-0.7825f, 0.75f, -1.15f);
            Vector3 endPosLeft = new Vector3(-0.7825f, 0.75f, 1.13f);

            //Opening
            while (elapsedTime < seconds)
            {

                right.transform.position = Vector3.Lerp(startingPosRight, endPosRight, (elapsedTime / seconds));
                left.transform.position = Vector3.Lerp(startingPosLeft, endPosLeft, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            right.transform.position = endPosRight;
            left.transform.position = endPosLeft;
            elapsedTime = 0;
            while (elapsedTime < 10)
            {
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            elapsedTime = 0;

            //Closing
            while (elapsedTime < seconds)
            {
                right.transform.position = Vector3.Lerp(endPosRight, startingPosRight, (elapsedTime / seconds));
                left.transform.position = Vector3.Lerp(endPosLeft, startingPosLeft, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            right.transform.position = startingPosRight;
            left.transform.position = startingPosLeft;
        }

    }
}
