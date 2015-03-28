using UnityEngine;
using System.Collections;

public class Multi_NetworkManager : MonoBehaviour
{
		public Sprite playerServer;
		public Sprite playerClient;
		public GameObject sharedData;
		private Multi_Fields myFields;
		private const string typeName = "AHKUSTGame";
		private const string gameName = "HKUSTGameRoom";
		private HostData[] hostList;
	private GUIStyle myButtonStyle;

		// Use this for initialization
		void Start ()
		{
				myFields = sharedData.GetComponent<Multi_Fields> ();

			
		}
	
	// Update is called once per frame
		void Update ()
		{
		
		}

		private void RefreshHostList ()
		{
				MasterServer.RequestHostList (typeName);
		}
	
		void OnMasterServerEvent (MasterServerEvent msEvent)
		{
				if (msEvent == MasterServerEvent.HostListReceived)
						hostList = MasterServer.PollHostList ();
		}

		private void StartServer ()
		{
				Network.InitializeServer (2, 25000 + Random.Range (0, 100), !Network.HavePublicAddress ());
				MasterServer.RegisterHost (typeName, gameName);
		}

		void OnServerInitialized ()
		{
				Debug.Log ("Server Initializied");
				myFields.stateInfo [(int)Multi_Fields.States.CONNECTION_ESTABLISHED] = true;
				
				spawnCharCServer ();

		}

		void spawnCharCServer ()
		{
				GameObject enemy = GameObject.Find ("enemyChar");
				enemy.GetComponent<SpriteRenderer> ().sprite = playerServer;
				Vector3 tempP = new Vector3 (-1.22f, 1.53f, 0);
				Vector3 tempS = new Vector3 (1.3f, 1.3f, 0);
				//enemy.transform.position = tempP;
				//enemy.transform.localScale = tempS;
		}

		private void JoinServer (HostData hostData)
		{
				Network.Connect (hostData);
		}
	
		void OnConnectedToServer ()
		{
				Debug.Log ("Server Joined");
		myFields.stateInfo [(int)Multi_Fields.States.CONNECTION_ESTABLISHED] = true;
				spawnCharClient ();
		}

		void spawnCharClient ()
		{
				GameObject enemy = GameObject.Find ("enemyChar");
				enemy.GetComponent<SpriteRenderer> ().sprite = playerClient;
				Vector3 tempP = new Vector3 (-1.22f, 0.98f, 0);
				Vector3 tempS = new Vector3 (1.1f, 1.1f, 0);
				//enemy.transform.position = tempP;
				//enemy.transform.localScale = tempS;
		}

		void OnGUI ()
		{
				// Create style for a button
				GUIStyle myButtonStyle = new GUIStyle (GUI.skin.button);
				myButtonStyle.fontSize = 30;
		
				// Load and set Font
				Font myFont = (Font)Resources.Load ("Fonts/comic", typeof(Font));
				myButtonStyle.font = myFont;

				if (!Network.isClient && !Network.isServer) {
						if (GUI.Button (new Rect (100, 100, 250, 100), "Start Server", myButtonStyle))
								StartServer ();

						if (GUI.Button (new Rect (100, 250, 250, 100), "Refresh Hosts", myButtonStyle))
								RefreshHostList ();
			
						if (hostList != null) {
								for (int i = 0; i < hostList.Length; i++) {
					if (GUI.Button (new Rect (400, 100 + (110 * i), 300, 100), hostList [i].gameName,myButtonStyle)) {
						
												JoinServer (hostList [i]);

										}
								}

						}
				} /*else if (Network.isClient || Network.isServer) {
						if (GUI.Button (new Rect (800, 100, 250, 100), "Close connection"))
								Network.Disconnect ();
				}*/

		if (myFields.stateInfo [(int)Multi_Fields.States.CONNECTION_ESTABLISHED]) {
			
			if (Network.isClient && !myFields.stateInfo [(int)Multi_Fields.States.CLIENT_READY_TO_START]) {
				if (GUI.Button (new Rect (100, 100, 250, 100), "Start game",myButtonStyle)) 
					
										myFields.syncState (Multi_Fields.States.CLIENT_READY_TO_START, true);
						}
			if (Network.isServer && !myFields.stateInfo [(int)Multi_Fields.States.SERVER_READY_TO_START]) {
				if (GUI.Button (new Rect (100, 100, 250, 100), "Start game",myButtonStyle))
					
										myFields.syncState (Multi_Fields.States.SERVER_READY_TO_START, true);
										
				
						}
				}
		}

}
