using UnityEngine;
using UnityEngine.VR;



namespace Com.VirtualClass.EscapeRoom
{
	public class Launcher : Photon.PunBehaviour
	{
		#region Public Variables

		public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
		[Tooltip("The Ui Panel to let the user connect and play")]
		public GameObject controlPanel;
		[Tooltip("The UI Label to inform the user that the connection is in progress")]
		public GameObject progressLabel;

		[Tooltip("The prefabs to use for representing the player")]
		public GameObject playerAndroid;
		public GameObject playerVR;
		public GameObject positionMarker;
		#endregion


		#region Private Variables
	
		/// <summary>
		/// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
		/// </summary>
		string _gameVersion = "1";

		/// <summary>
		/// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
		/// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
		/// Typically this is used for the OnConnectedToMaster() callback.
		/// </summary>
		bool isConnecting;

		#endregion


		#region MonoBehaviour CallBacks


		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during early initialization phase.
		/// </summary>
		void Awake()
		{
			// #NotImportant
			// Force LogLevel
			PhotonNetwork.logLevel = Loglevel;

			// #Critical
			// we don't join the lobby. There is no need to join a lobby to get the list of rooms.
			PhotonNetwork.autoJoinLobby = false;


			// #Critical
			// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
			PhotonNetwork.automaticallySyncScene = true;
		}


		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during initialization phase.
		/// </summary>
		void Start()
		{
			progressLabel.SetActive(false);
			controlPanel.SetActive(true);
		}

        private void Update()
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Connect();
            }
        }


        #endregion


        #region Public Methods


        /// <summary>
        /// Start the connection process. 
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect()
		{
			// keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
			isConnecting = true;

			progressLabel.SetActive(true);
			controlPanel.SetActive(false);

			// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
			if (PhotonNetwork.connected)
			{
				// #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
				PhotonNetwork.JoinRandomRoom();
			}else{
				// #Critical, we must first and foremost connect to Photon Online Server.
				PhotonNetwork.ConnectUsingSettings(_gameVersion);
			}
		}


		#endregion
		#region Photon.PunBehaviour CallBacks


		public override void OnConnectedToMaster()
		{


			Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
			// we don't want to do anything if we are not attempting to join a room. 
			// this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
			// we don't want to do anything.
			if (isConnecting)
			{
				// #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()
				PhotonNetwork.JoinRandomRoom();
			}

		}


		public override void OnDisconnectedFromPhoton()
		{
			progressLabel.SetActive(false);
			controlPanel.SetActive(true);

			Debug.LogWarning("Launcher: OnDisconnectedFromPhoton() was called by PUN");        
		}


		#endregion

		public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
		{
			Debug.Log("Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

			// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
			PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);
		}

		public override void OnJoinedRoom()
		{
			Debug.Log("Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
			bool isVR = VRDevice.isPresent;
			GameObject player;
//			if (isVR) {
//    			player = PhotonNetwork.Instantiate (this.playerVR.name, new Vector3 (0.5f, 0f, 0.5f), Quaternion.identity, 0);
//				GameObject marker = PhotonNetwork.Instantiate (positionMarker.name, new Vector3 (0f, 0f, 0f), Quaternion.identity, 0);
//				player.GetComponent<InstantiateVRPlayer> ().connectPositionMarker (marker.GetComponent<FollowHead>());
//			} else {
//				player = PhotonNetwork.Instantiate (this.playerAndroid.name, new Vector3 (2.8f, 7f, 0.5f), Quaternion.Euler(90 ,0 ,0), 0);
//			}
			// #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.automaticallySyncScene to sync our instance scene.
			if (PhotonNetwork.room.PlayerCount == 1)
			{
				Debug.Log("Starting AndroidRoom");
//				Component comp = player.GetComponent<InstantiateAndroid> ();
//				if (comp == null) {
//					player.GetComponent<InstantiateVRPlayer> ().TeleportToWaitingzone ();
//				} else {
//					player.GetComponent<InstantiateAndroid> ().TeleportToWaitingzone ();
//				}

				// #Critical
				// Load the Room Level. 
				PhotonNetwork.LoadLevel("AndroidRoom");
			}

		}

	}
}