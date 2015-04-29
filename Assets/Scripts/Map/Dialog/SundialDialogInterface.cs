using UnityEngine;
using System.Collections;

public class SundialDialogInterface : DialogInterface {
	bool cleaner=true;
	public GameObject QTETriangle;


	// Use this for initialization
	void Start () {
		GamePause.pauseGame ();
		DialogSystem.character[] cArr = {
			DialogSystem.character.VOLDEMORT,
			DialogSystem.character.PLAYER,
			DialogSystem.character.VOLDEMORT,
			DialogSystem.character.PLAYER,
			DialogSystem.character.VOLDEMORT,
			DialogSystem.character.PLAYER,
			DialogSystem.character.VOLDEMORT,
			DialogSystem.character.VOLDEMORT,
			DialogSystem.character.PLAYER,
			DialogSystem.character.VOLDEMORT,
			DialogSystem.character.VOLDEMORT
		};
		
		string[] dialog = new string[]{
			"Finally, get you back to here...",
			"Oh, I am sorry, does it hurt?",
			"I am fine... Just feel sleepy...",
			"Why did you attack me?!",
			"I was controlled by the boss... That was not what I want",
			"Your boss? Who he is? Take me to him!",
			"I can't hold long... listen!",
			"Use my magic ward to unlock the sundial, he is under there!",
			"Magic? Who are you?\nHow can you know magic?",
			"I was the magic club chairman long time ago...",
			"Remember Mobiliarbus...Mobiliarbus..."
		};

		conversation (cArr, dialog, 1);
	}
	void startSecondDialog(){
		DialogSystem.character[] cArr = {
			DialogSystem.character.SYSTEM,
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER
		};
		
		string[] dialog = new string[]{
			"VOLDEMORT disappears...",
			"... Unlock the sundial? Sundial was lock?",
			"Wait a minute, it was rotated! It is not facing its original direction!\nBut what does it mean?",
			"Maybe, I just try the magic that Voldemort taught me.",
			 "'Mobiliarbus'...! 'Mobiliarbus!'"
		};
		conversation (cArr, dialog, 2);
	}
	void startThirdDialog(){
		DialogSystem.character[] cArr = {

			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER
		};
		
		string[] dialog = new string[]{
	 	"It starts rotating!",
		 "It works! A stair? What is the stair going to?"
		};
		conversation (cArr, dialog, 3);
	}

	// Update is called once per frame
	void Update () {
		if (cleaner) {
			if (GameObject.Find ("AutoFade") != null)
				Destroy (GameObject.Find ("AutoFade"));
			cleaner=false;
		}
		if (QTETriangle == null) {
			startThirdDialog();
		}
	}



	override public void  onDialogFinish (int id, int selection)
	{
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finished with selection result " + selection);
		if (id == 1) {
			startSecondDialog();
		}
		if (id == 2)
			QTETriangle.SetActive (true);
		if (id == 3)
			Application.LoadLevel("SecretChamberBefore");
		
	}
}
