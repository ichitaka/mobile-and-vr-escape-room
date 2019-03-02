using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.VR;


namespace Com.VirtualClass.EscapeRoom
{
	public class GameManager : Photon.PunBehaviour {
		#region Public Variables

		static public GameManager Instance;

		[Tooltip("The prefabs to use for representing the player")]
		public GameObject playerAndroid;
		public GameObject playerVR;

		public GameObject[] rooms;

		#endregion

		#region Private Variables

		private List<Vector3> posList;

		#endregion

		#region MonoBehaviour CallBacks

		void Start() {
			posList = new List<Vector3> ();
			InitPositions ();

			bool isVR = VRDevice.isPresent;
			GameObject player;

			PhotonNetwork.offlineMode = true;

			Instance = this;
			Debug.Log ("We are Instantiating LocalPlayer from " + Application.loadedLevelName);
			// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
			if (isVR) {
				player = PhotonNetwork.Instantiate (this.playerVR.name, new Vector3 (0.5f, 0f, 0f), Quaternion.identity, 0);
            } else {
				player = PhotonNetwork.Instantiate (this.playerAndroid.name, new Vector3 (2.8f, 7f, 0.5f), Quaternion.Euler(90 ,0 ,0), 0);
				PhotonNetwork.Instantiate ("RaumStart", new Vector3 (2f, 0.1f, -1f), Quaternion.identity, 0);
				for (int i = 0; i < rooms.Length; i++) {
                    Vector3 roompos = GivePos(rooms[i].name + "(Clone)");
                    Debug.Log(roompos);
                    PhotonNetwork.Instantiate (rooms [i].name, roompos, rooms[i].transform.rotation, 0);
				}
			}
			if (PhotonNetwork.room.PlayerCount == 1) {
				Component comp = player.GetComponent<InstantiateAndroid> ();
                if (comp == null) {
					player.GetComponent<InstantiateVRPlayer> ().TeleportToWaitingzone ();
				} else {
					player.GetComponent<InstantiateAndroid> ().TeleportToWaitingzone ();
				}
			}
		}

		#endregion

		#region Photon Messages


		public override void OnPhotonPlayerConnected( PhotonPlayer other  )
		{
			Debug.Log( "OnPhotonPlayerConnected() " + other.NickName ); // not seen if you're the player connecting
			if ( PhotonNetwork.isMasterClient ) 
			{
				Debug.Log( "OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient ); // called before OnPhotonPlayerDisconnected


//				LoadArena();
			}
		}


		public override void OnPhotonPlayerDisconnected( PhotonPlayer other  )
		{
			Debug.Log( "OnPhotonPlayerDisconnected() " + other.NickName ); // seen when other disconnects

			if ( PhotonNetwork.isMasterClient ) 
			{
				Debug.Log( "OnPhotonPlayerDisonnected isMasterClient " + PhotonNetwork.isMasterClient ); // called before OnPhotonPlayerDisconnected


				//LoadArena();
			}
		}
			

		/// <summary>
		/// Called when the local player left the room. We need to load the launcher scene.
		/// </summary>
		public void OnLeftRoom()
		{
			SceneManager.LoadScene(0);
		}


		#endregion


		#region Public Methods


		public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom();
		}

		public Vector3 GivePos(string names) {
			Vector3 output = getOffset(posList[0], names);
            Debug.Log("nach offset:" + output);
            posList.RemoveAt (0);
			return output;
		}

		public void SavePos(Vector3 pos, string names) {
			posList.Insert (0, removeOffset(pos, names));
		}

        #endregion

        #region Private Methods
        Vector3 getOffset(Vector3 uiPos, string names)
        {
            Debug.Log("vor offset:" + uiPos);
            Vector3 output = uiPos;
            if (names == "Raum1 1(Clone)")
            {
                output += Vector3.zero;
            }
            else if (names == "Raum5(Clone)")
            {
                Debug.Log("vor addition:" + output);
                output += new Vector3(-1.2f, 0f, 0f);
                Debug.Log("nach addition:" + output);
            }
            else if (names == "Raum5.1(Clone)")
            {
                output += new Vector3(-1.2f, 0, -0.4f);
            }
            else if (names == "Raum6(Clone)")
            {
                output += new Vector3(-1.2f, 0, -0.8f);
            }
            else if (names == "Raum7(Clone)")
            {
                output = output + Vector3.zero;
            }
            return output;
        }

        Vector3 removeOffset(Vector3 uiPos, string names)
        {
            Vector3 output = uiPos;
            if (names == "Raum1 1(Clone)")
            {
                output = output - Vector3.zero;
            }
            else if (names == "Raum5(Clone)")
            {
                output = output - new Vector3(-1.2f, 0f, 0f);
            }
            else if (names == "Raum5.1(Clone)")
            {
                output = output - new Vector3(-1.2f, 0, -0.4f);
            }
            else if (names == "Raum6(Clone)")
            {
                output = output - new Vector3(-1.2f, 0, -0.8f);
            }
            else if (names == "Raum7(Close)")
            {
                output = output - Vector3.zero;
            }
            return output;
        }

        //		void LoadArena()
        //		{
        //			if ( ! PhotonNetwork.isMasterClient ) 
        //			{
        //				Debug.LogError( "PhotonNetwork : Trying to Load a level but we are not the master Client" );
        //			}
        //			Debug.Log( "PhotonNetwork : Loading Level : " + PhotonNetwork.room.PlayerCount );
        //			PhotonNetwork.LoadLevel("AndroidRoom");
        //		}
        //
        void InitPositions() {
			posList.Add(new Vector3 (4.8f, 0f, 1.9f));
			posList.Add(new Vector3 (4.8f, 0f, 0.7f));
			posList.Add(new Vector3 (4.8f, 0f, -0.5f));
			posList.Add(new Vector3 (6.8f, 0f, 1.9f));
			posList.Add(new Vector3 (6.8f, 0f, 0.7f));
			posList.Add(new Vector3 (6.8f, 0f, -0.5f));
		}


		#endregion
	}
}