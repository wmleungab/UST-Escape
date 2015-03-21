using UnityEngine;
using System.Collections;

public class Multi_QTEParent : MonoBehaviour
{

		// Use this for initialization
		public GameObject QTETouch;
		public GameObject QTESwipe;
		public GameObject QTESlide;
		public GameObject QTETriangle;
		public GameObject attackRound;
		public GameObject defenseRound;
		public GameObject resultBanner;
		private Multi_Fields myFields;
		private GameObject roundStarterInstance;
		private GameObject QTEChildren;
		private string result = "NONE";

		void Start ()
		{
				myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
				QTEChildren = null;
				roundStarterInstance = null;

				
		}
	
		// Update is called once per frame
		void Update ()
		{

				if (myFields.stateInfo [(int)Multi_Fields.States.ROUND_STARTS]) {
						Debug.Log ("Attrd: " + myFields.stateInfo [(int)Multi_Fields.States.ATTACK_ROUND]);
						Debug.Log ("Defenrd: " + myFields.stateInfo [(int)Multi_Fields.States.DEFENSE_ROUND]);
						myFields.changeState (Multi_Fields.States.ROUND_STARTS, false);
						if (roundStarterInstance == null 
								&& myFields.stateInfo [(int)Multi_Fields.States.ATTACK_ROUND]
								&& !myFields.stateInfo [(int)Multi_Fields.States.DEFENSE_ROUND])
								roundStarterInstance = Instantiate (attackRound, new Vector3 (0, 0, 0), Quaternion.identity)as GameObject;
						else if (roundStarterInstance == null 
								&& !myFields.stateInfo [(int)Multi_Fields.States.ATTACK_ROUND]
								&& myFields.stateInfo [(int)Multi_Fields.States.DEFENSE_ROUND])
								roundStarterInstance = Instantiate (defenseRound, new Vector3 (0, 0, 0), Quaternion.identity)as GameObject;
						
				}

				
				//start a qte round
				if (!myFields.stateInfo [(int)Multi_Fields.States.ROUND_IN_PROGRESS] 
						&& myFields.stateInfo [(int)Multi_Fields.States.SERVER_READY_TO_START_ROUND]
						&& myFields.stateInfo [(int)Multi_Fields.States.CLIENT_READY_TO_START_ROUND]) {

						myFields.changeState (Multi_Fields.States.CLIENT_READY_TO_START_ROUND, false);
						myFields.changeState (Multi_Fields.States.SERVER_READY_TO_START_ROUND, false);
						myFields.changeState (Multi_Fields.States.ROUND_IN_PROGRESS, true);

						if (QTEChildren == null) {

								switch (myFields.QTEmode) {
								case 0:
										QTEChildren = Instantiate (QTETouch, new Vector3 (0, 0, 0), Quaternion.identity)as GameObject;
										break;
								case 1:
										QTEChildren = Instantiate (QTESwipe, new Vector3 (0, 0, 0), Quaternion.identity)as GameObject;
										break;
								case 2:
										QTEChildren = Instantiate (QTESlide, new Vector3 (0, 0, 0), Quaternion.identity)as GameObject;
										break;
								case 3:
										QTEChildren = Instantiate (QTETriangle, new Vector3 (0, 0, 0), Quaternion.identity)as GameObject;
										break;
								}
						}



						
						if (Network.isServer)
								myFields.syncQTEMode (Random.Range (0, 4));
				}

				//Round end
				if (myFields.stateInfo [(int)Multi_Fields.States.ROUND_IN_PROGRESS] 
						&& myFields.stateInfo [(int)Multi_Fields.States.SERVER_FINISH] 
						&& myFields.stateInfo [(int)Multi_Fields.States.CLIENT_FINISH]) {

						if (long.Parse (myFields.ServerFinishTime) < long.Parse (myFields.ClientFinishTime)) {
								result = "Server";
								myFields.changeState (Multi_Fields.States.SERVER_SUCCESS, true);
						} else if (long.Parse (myFields.ServerFinishTime) > long.Parse (myFields.ClientFinishTime)) {
								result = "Client";
								myFields.changeState (Multi_Fields.States.SERVER_SUCCESS, false);
						} else
								result = "Fair";
			
						myFields.changeState (Multi_Fields.States.ROUND_IN_PROGRESS, false);
						myFields.changeState (Multi_Fields.States.SERVER_FINISH, false);
						myFields.changeState (Multi_Fields.States.CLIENT_FINISH, false);
						Instantiate (resultBanner, new Vector3 (0, 0, 0), Quaternion.identity);
						myFields.ServerFinishTime = null;
						myFields.ClientFinishTime = null;

						if (myFields.stateInfo [(int)Multi_Fields.States.ATTACK_ROUND]
								&& !myFields.stateInfo [(int)Multi_Fields.States.DEFENSE_ROUND]) {

								myFields.changeState (Multi_Fields.States.ATTACK_ROUND, false);
								myFields.changeState (Multi_Fields.States.DEFENSE_ROUND, true);


						} else if (!myFields.stateInfo [(int)Multi_Fields.States.ATTACK_ROUND]
								&& myFields.stateInfo [(int)Multi_Fields.States.DEFENSE_ROUND]) {

								myFields.changeState (Multi_Fields.States.ATTACK_ROUND, true);
								myFields.changeState (Multi_Fields.States.DEFENSE_ROUND, false);

						}
	
				}
		}

		void OnGUI ()
		{
				// Create style for a button
				GUIStyle myButtonStyle = new GUIStyle (GUI.skin.box);
				myButtonStyle.fontSize = 50;
				// Load and set Font
				Font myFont = (Font)Resources.Load ("Fonts/comic", typeof(Font));
				myButtonStyle.font = myFont;


				

				GUI.Box (new Rect (Screen.width / 2 - 300, 0, 600, 200), result
						+ '\n' + "qte: " + QTEChildren
						+ '\n' + "sReady: " + myFields.stateInfo [(int)Multi_Fields.States.SERVER_READY_TO_START_ROUND]
						+ '\n' + "cReady: " + myFields.stateInfo [(int)Multi_Fields.States.CLIENT_READY_TO_START_ROUND]
						+ '\n' + "SERVER_SUCCESS: " + myFields.stateInfo [(int)Multi_Fields.States.SERVER_SUCCESS]
						+ '\n' + "s: " + long.Parse (myFields.internServerFinishTime)
						+ '\n' + "c: " + long.Parse (myFields.internClientFinishTime)
				);
		}
}
