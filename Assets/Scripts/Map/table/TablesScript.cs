﻿using UnityEngine;
using System.Collections;

public class TablesScript : BigTableScript
{

		public bool[] answerArray;
		private bool[] tablesOpen;
		TableDialogInterface dialogInterface;
		public Sprite pcOn;
		public Sprite pcOff;
		public Sprite doorOpenPic;
		bool sl_OpenDoorChecker = true;
		// Use this for initialization
		void Start ()
		{

				if (answerArray.Length != noOfTables) {
						Debug.LogError ("TablesScript: answerArray not initialized correctly");
				}
				tablesOpen = new bool[noOfTables];

		}

		void Update ()
		{
				if (sl_OpenDoorChecker)
				if (SaveLoadSystem.getInstance ().labSceneStateArr [(int)SaveLoadSystem.LabSceneState.PUZZLESOLVED]) {
						tablesOpen = answerArray;
						GameObject.Find ("Door").gameObject.SendMessage ("openDoor", true, SendMessageOptions.DontRequireReceiver);
						sl_OpenDoorChecker = false;
						tableScript.allCanOpen=false;
			for(int i=0;i<answerArray.Length;i++){
				if(!answerArray[i])continue;
				GameObject Tables;
				GameObject table;
				for(int j=0;j<6;j++){
					Tables=gameObject.transform.GetChild(j).gameObject;

					for(int k=0;k<4;k++){
						table=Tables.transform.GetChild(k).gameObject;
						if(table.GetComponent<tableScript>().tableNo+j*4==i)table.GetComponent<tableScript>().flip();
					}
				}
			}
						issueSuccessPuzzlingDialog ();
				}
		}
		//return if answer correct
		public override bool openTable (int tableNo)
		{
				if (tableNo >= 0 && tableNo < tablesOpen.Length) {
						tablesOpen [tableNo] = !tablesOpen [tableNo];
						Debug.Log ("Big Table Array: " + this.getTablesOpen ());

			sl_OpenDoorChecker=false;
						issueTurnOnOffDialog (tableNo);

						if (checkArray ()) {
								Debug.Log ("get it right!");
								GameObject.Find ("Door").gameObject.SendMessage ("openDoor", true, SendMessageOptions.DontRequireReceiver);


								SaveLoadSystem.getInstance ().labSceneStateArr [(int)SaveLoadSystem.LabSceneState.PUZZLESOLVED] = true;
								SaveLoadSystem.getInstance ().save ();
								issueSuccessPuzzlingDialog ();
								return true;
						}
				} else {
						Debug.LogError ("tableNo " + tableNo + " not valid");
				}
				return false;
		}
	
		bool checkArray ()
		{
				for (int i=0; i<tablesOpen.Length; i++) {
						if (tablesOpen [i] != answerArray [i]) {
								return false;
						}
				}
				return true;
		}
	
		string getTablesOpen ()
		{
				string result = "tablesOpen={";
		
				foreach (bool arrayItem in tablesOpen) {
						result += (arrayItem + ",");
				}
		
				result = result.Remove (result.Length - 1);
				result += "}";
		
				return result;
		}

		void issueTurnOnOffDialog (int tableNo)
		{
				dialogInterface = gameObject.GetComponents<TableDialogInterface> () [0];
				DialogSystem.character[] c = {DialogSystem.character.SYSTEM};
				string tableState;
				if (tablesOpen [tableNo])
						tableState = "turned On";
				else
						tableState = "turned Off";
				string statement = "Computer " + (tableNo + 1) + " is " + tableState;
				string[] s = {statement};
				if (tablesOpen [tableNo])
						dialogInterface.showBigIcon (c, s, 111, pcOn);
				else
						dialogInterface.showBigIcon (c, s, 111, pcOff);
		}

		void issueSuccessPuzzlingDialog ()
		{
				dialogInterface = gameObject.GetComponents<TableDialogInterface> () [0];
				DialogSystem.character[] c = {DialogSystem.character.SYSTEM};
				string[] s = {"The door is opened."};
				dialogInterface.showBigIcon (c, s, 222, doorOpenPic);

				DialogSystem.character[] c2 = {DialogSystem.character.PLAYER};
				string[] s2 = {"Yes! “DUN EAT”! That is the clue! I can leave now!"};
				dialogInterface.conversation (c2, s2, 333);
		}
}
