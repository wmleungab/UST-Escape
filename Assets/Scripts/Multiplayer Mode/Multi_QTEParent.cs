using UnityEngine;
using System.Collections;

public class Multi_QTEParent : MonoBehaviour
{

		// Use this for initialization
		public GameObject QTETouch;
		public GameObject QTESwipe;
		public GameObject QTESlide;
		public GameObject QTETriangle;
		public GameObject QTEUpDown;
		public GameObject attackRound;
		public GameObject defenseRound;
		public GameObject resultBanner;
		public int originalDamage;
		public int reducedDamage;
		private Multi_Fields myFields;
		private GameObject roundStarterInstance;
		private GameObject QTEChildren;
		private string result = "NONE";
		long startingSecond = 0;
		long cSumSecond = 0;
		long sSumSecond = 0;

		void Start ()
		{
				myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
				QTEChildren = null;
				roundStarterInstance = null;

				
		}
	
		// Update is called once per frame
		void Update ()
		{
				//&& !myFields.stateInfo [(int)Multi_Fields.States.ATTACK_ANI_READY]
				if (myFields.stateInfo [(int)Multi_Fields.States.ROUND_STARTS] && !myFields.stateInfo [(int)Multi_Fields.States.ATTACK_ANI_READY]) {
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
						
						
						if (Network.isServer) {
							float myRan = Random.Range (0, 5);
							myFields.syncQTEMode (Mathf.FloorToInt (myRan));
						}
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
								case 4:
										QTEChildren = Instantiate (QTEUpDown, new Vector3 (0, 0, 0), Quaternion.identity)as GameObject;
										break;
								}

								int currentSeconds = int.Parse (System.DateTime.Now.ToString ("ss"));
								int currentMinutes = int.Parse (System.DateTime.Now.ToString ("mm"));
								int currentHours = int.Parse (System.DateTime.Now.ToString ("hh"));
								startingSecond = currentHours * 3600 + currentMinutes * 60 + currentSeconds;
						}

				}

				//Round end
				if (myFields.stateInfo [(int)Multi_Fields.States.ROUND_IN_PROGRESS] 
						&& myFields.stateInfo [(int)Multi_Fields.States.SERVER_FINISH] 
						&& myFields.stateInfo [(int)Multi_Fields.States.CLIENT_FINISH]) {

						if (long.Parse (myFields.ServerFinishTime) < long.Parse (myFields.ClientFinishTime)) {
								result = "Server";
								myFields.changeState (Multi_Fields.States.SERVER_SUCCESS, true);
								if (Network.isServer)
										myFields.incrementWonQTE (true);
						} else if (long.Parse (myFields.ServerFinishTime) > long.Parse (myFields.ClientFinishTime)) {
								result = "Client";
								myFields.changeState (Multi_Fields.States.SERVER_SUCCESS, false);
								if (Network.isServer)
										myFields.incrementWonQTE (false);
						} else
								result = "Fair";
						
						sSumSecond += myFields.ServerFinishTimeHelper - startingSecond;
						cSumSecond += myFields.ClientFinishTimeHelper - startingSecond;

						myFields.changeState (Multi_Fields.States.ROUND_IN_PROGRESS, false);
						myFields.changeState (Multi_Fields.States.SERVER_FINISH, false);
						myFields.changeState (Multi_Fields.States.CLIENT_FINISH, false);

						Instantiate (resultBanner, new Vector3 (0, 0, 0), Quaternion.identity);

						myFields.ServerFinishTime = null;
						myFields.ClientFinishTime = null;


						//Attack round
						if (myFields.stateInfo [(int)Multi_Fields.States.ATTACK_ROUND]
								&& !myFields.stateInfo [(int)Multi_Fields.States.DEFENSE_ROUND]) {

								if (myFields.stateInfo [(int)Multi_Fields.States.SERVER_SUCCESS])
										myFields.changeState (Multi_Fields.States.SERVER_ATTACKING, true);
								else
										myFields.changeState (Multi_Fields.States.SERVER_ATTACKING, false);
								
								/*myFields.changeState (Multi_Fields.States.ATTACK_ANI_READY, false);*/
								//change back to defense round
								myFields.changeState (Multi_Fields.States.ATTACK_ROUND, false);
								myFields.changeState (Multi_Fields.States.DEFENSE_ROUND, true);
								
								//Defense round and its action
						} else if (!myFields.stateInfo [(int)Multi_Fields.States.ATTACK_ROUND]
								&& myFields.stateInfo [(int)Multi_Fields.States.DEFENSE_ROUND]) {
								
								//check defense operation success or not
								if (myFields.stateInfo [(int)Multi_Fields.States.SERVER_SUCCESS]) {
										if (myFields.stateInfo [(int)Multi_Fields.States.SERVER_ATTACKING])
												myFields.changeState (Multi_Fields.States.DEFENSE_SUCCESS, false);
										else
												myFields.changeState (Multi_Fields.States.DEFENSE_SUCCESS, true);
								} else {
										if (myFields.stateInfo [(int)Multi_Fields.States.SERVER_ATTACKING])
												myFields.changeState (Multi_Fields.States.DEFENSE_SUCCESS, true);
										else
												myFields.changeState (Multi_Fields.States.DEFENSE_SUCCESS, false);
								}

								/*myFields.changeState (Multi_Fields.States.ATTACK_ANI_READY, false);*/
								//change back to attack round
								myFields.changeState (Multi_Fields.States.ATTACK_ROUND, true);
								myFields.changeState (Multi_Fields.States.DEFENSE_ROUND, false);

						}
				}
				if (myFields.stateInfo [(int)Multi_Fields.States.ATTACK_ANI_READY]) {
						if (myFields.stateInfo [(int)Multi_Fields.States.ATTACK_ROUND]
								&& !myFields.stateInfo [(int)Multi_Fields.States.DEFENSE_ROUND]) {
								if (Network.isServer) {
										if (!myFields.stateInfo [(int)Multi_Fields.States.SERVER_ATTACKING]) {
												GameObject.Find ("Enemy").GetComponent<Multi_dembeater1> ().startFlag = true;
												//while(GameObject.Find ("Enemy").GetComponent<Multi_dembeater1> ().startFlag);
										}
								} else {
										if (myFields.stateInfo [(int)Multi_Fields.States.SERVER_ATTACKING]) {
												GameObject.Find ("Enemy").GetComponent<Multi_dembeater2> ().startFlag = true;
												//while(GameObject.Find ("Enemy").GetComponent<Multi_dembeater2> ().startFlag);
										}
								}
			
								//issue damage
								
								if (myFields.stateInfo [(int)Multi_Fields.States.SERVER_ATTACKING]) {
										if (myFields.stateInfo [(int)Multi_Fields.States.DEFENSE_SUCCESS]) {
												myFields.ClientHP -= reducedDamage;
												//myFields.syncHP (reducedDamage, false);
										} else {
												myFields.ClientHP -= originalDamage;
												//myFields.syncHP (originalDamage, false);
										}
										if (myFields.ClientHP < 0)
												myFields.ClientHP = 0;
								} else {
										if (myFields.stateInfo [(int)Multi_Fields.States.DEFENSE_SUCCESS]) {
												myFields.ServerHP -= reducedDamage;
												//myFields.syncHP (reducedDamage, true);
										} else {
												myFields.ServerHP -= originalDamage;
												//myFields.syncHP (originalDamage, true);
										}
										if (myFields.ServerHP < 0)
												myFields.ServerHP = 0;
								}


								if (myFields.ServerHP == 0 || myFields.ClientHP == 0) {
										myFields.ServerFinishTimeHelper = sSumSecond / (myFields.SWonQTE + myFields.CWonQTE);
										myFields.ClientFinishTimeHelper = cSumSecond / (myFields.SWonQTE + myFields.CWonQTE);
										Destroy (gameObject);
								}
						}
						myFields.changeState (Multi_Fields.States.ATTACK_ANI_READY, false);
	
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


				
				
		//		GUI.Box (new Rect (Screen.width / 2 - 300, 0, 600, 100), //result
				/*		+ '\n' + "qte: " + QTEChildren
						+ '\n' + "sReady: " + myFields.stateInfo [(int)Multi_Fields.States.SERVER_READY_TO_START_ROUND]
						+ '\n' + "cReady: " + myFields.stateInfo [(int)Multi_Fields.States.CLIENT_READY_TO_START_ROUND]
						+ '\n' + "SERVER_SUCCESS: " + myFields.stateInfo [(int)Multi_Fields.States.SERVER_SUCCESS]
						+ '\n' + "s: " + long.Parse (myFields.internServerFinishTime)
						+ '\n' + "c: " + long.Parse (myFields.internClientFinishTime)*/
		         /*       "\n" + sSumSecond
						+ "\n" + cSumSecond
						+ "\n" + myFields.ServerFinishTimeHelper
						+ "\n" + myFields.ClientFinishTimeHelper
				);
		*/
		}
}
