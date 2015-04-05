﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DialogSystem : MonoBehaviour
{
		GameObject dialogInstance;
		bool dialogisOn = false;
		bool waitForClick = false;
		bool isClick = false;
		static public bool toCreateDialog = false;
		static public character[]	nameString;
		static public string[]dialogString;
		character[] pic = {0,0}; //[0] is the left pic, [1] is the right
		int dialog_counter = 0;
		int myId = -1;
		DialogInterface curDi = null;
		//If there is 3 people in a conservation. Main Character will not appear in pic.

		public GameObject dialogPrefab;
		public Queue<string>cnameString = new Queue<string> ();
		public Queue<string> cdialogString = new Queue<string> ();
		public Sprite dialogPrincipal ;
		public Sprite dialogPlayer;
		public Sprite dialogDembeater_Flag;
		public Sprite dialogDembeater_Leg;
		public Sprite dialogVoldemort;
		public Sprite dialogCleaningLady;
		public Sprite dialogDimjack;
		public Sprite dialogGirlGod;

		public enum character
		{
				PLAYER=0,
				PRINCIPAL,
				CLEANINGLADY,
				DEMBEATER_FLAG,
				DEMBEATER_LEG,
				GIRLGOD,
				VOLDEMORT,
				DIMJACK,
				SYSTEM,
				NOPIC
		}
		;



		// Use this for initialization
		void Start ()
		{
				//test
				//toCreateDialog = true;
				nameString = new character[]{character.PRINCIPAL,character.PRINCIPAL};
				dialogString = new string[]{"Hello","Hello"};

		}
		
		public void startDialog ()
		{
				toCreateDialog = true;
		}

/*		public void startDialog (string _nameString, string _dialogString)
		{ 
				toCreateDialog = true;
				nameString = new string[]{_nameString};
				dialogString = new string[]{_dialogString};

		}*/




		public void startDialog (character[] _nameString, string[] _dialogString, DialogInterface di, int id, bool selection)
		{
				toCreateDialog = true;
				nameString = _nameString;
				dialogString = _dialogString;
				myId = id;
				curDi = di;
		}

		public void onFinish ()
		{
				pic [0] = character.NOPIC;
				pic [1] = character.NOPIC;
				dialog_counter = 0;

				curDi.onDialogFinish (myId, -1);
				myId = -1;
				curDi = null;
		}

		// Update is called once per frame
		void Update ()
		{
				if (!dialogisOn) {
						if (toCreateDialog) {
								CreateDialog ();
								toCreateDialog = false;
								dialogisOn = true;
						}
						
				}
				if (dialogisOn) {
						if (waitForClick)
						if (Input.GetMouseButtonDown (0)) {
								isClick = true;
						}
						if (isClick) {
								if (cdialogString.Count > 0) {
										dialogInstance.transform.GetChild (1).GetComponent<TextMesh> ().text = cnameString.Dequeue ();
										dialogInstance.transform.GetChild (2).GetComponent<TextMesh> ().text = cdialogString.Dequeue ();
										//charaterpic
										if (pic [0] == nameString [dialog_counter - 1]) {
												if (dialogInstance.transform.GetChild (3).GetComponent<SpriteRenderer> ().sprite != null)
														dialogInstance.transform.GetChild (3).renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
												if (dialogInstance.transform.GetChild (4).GetComponent<SpriteRenderer> ().sprite != null)
														dialogInstance.transform.GetChild (4).renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.3f);
										} else if (pic [1] == nameString [dialog_counter - 1]) {
												if (dialogInstance.transform.GetChild (3).GetComponent<SpriteRenderer> ().sprite != null)
														dialogInstance.transform.GetChild (3).renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.3f);
												if (dialogInstance.transform.GetChild (4).GetComponent<SpriteRenderer> ().sprite != null)
														dialogInstance.transform.GetChild (4).renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
										}
										
										

										waitForClick = true;
										isClick = false;
								} else {
										Destroy (dialogInstance);

										onFinish ();
										dialogisOn = false;
										GamePause.continueGame ();
								}
						}
				
				}

		}

		private void setPic ()
		{
				
				character[] myCStr = nameString.Distinct ().ToArray ();
				for (int i=0; i<myCStr.Length; i++) {
						if (i >= 2)
								return;
						pic [i] = myCStr [i];
						switch (myCStr [i]) {

						case character.PLAYER:	
								
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogPlayer;
								break;
						case character.PRINCIPAL:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogPrincipal;
								break;
						case character.CLEANINGLADY:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogCleaningLady;
								break;
						case character.DEMBEATER_FLAG:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogDembeater_Flag;
								break;
						case character.DEMBEATER_LEG:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogDembeater_Leg;
								break;

						case character.GIRLGOD:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogGirlGod;
								break;
						case character.VOLDEMORT:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogVoldemort;
								break;
						case character.DIMJACK:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogDimjack;
								break;
						}

				}	
		}
		
		public void onChildrenTouched (string name)
		{
				switch (name) {
				case "btn_ok":
						Debug.Log ("ok clicked");
						break;
				}
		}

		void CreateDialog ()
		{
				Debug.Log ("Creating Dialogue");
		
				
				for (int i=0; i<nameString.Length; i++) {

						switch ((int)nameString [i]) {
						case 0:
								cnameString.Enqueue ("You");
								break;
						case 1:
								cnameString.Enqueue ("Principal");
								break;
						case 2:
								cnameString.Enqueue ("Cleaning Lady");
								break;
						case 3:
								cnameString.Enqueue ("Dembeater1");
								break;
						case 4:
								cnameString.Enqueue ("Dembeater2");
								break;
						case 5:
								cnameString.Enqueue ("The (girl)?");
								break;
						case 6:
								cnameString.Enqueue ("Voldemort");
								break;
						case 7:
								cnameString.Enqueue ("DumJack");
								break;
						case 8:
								cnameString.Enqueue ("System");
								break;
						}
			
				}	
				dialog_counter = nameString.Length;

				foreach (string s in dialogString)
						cdialogString.Enqueue (s);

				Vector3 position = Camera.mainCamera.gameObject.transform.position;
				position.y -= 3;
				position.z -= 3.5f;
				dialogInstance = Instantiate (dialogPrefab, position, Quaternion.Euler (90, 0, 0))as GameObject;

				setPic ();
				dialogInstance.transform.localScale -= new Vector3 (0.2F, 0.2F, 0.8F);
				isClick = true;
				/*
		for (int i=0; i<num; i++) {
						if (isClick) {
								dialogbg = Instantiate (dialogpf, new Vector3(0,0,-30), Quaternion.identity)as GameObject;
								dialogbg.transform.GetChild (1).GetComponent<TextMesh> ().text = name;
								dialogbg.transform.GetChild (2).GetComponent<TextMesh> ().text = alldialog [0];
								waitForClick = true;
								isClick=false;
						}
				}*/
				dialogisOn = false;
				waitForClick = true;
				GamePause.pauseGame ();
			
		}
}
