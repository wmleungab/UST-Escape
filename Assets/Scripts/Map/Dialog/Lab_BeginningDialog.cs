using UnityEngine;
using System.Collections;

public class Lab_BeginningDialog : DialogInterface {

	// Use this for initialization
	void Start () {

		StartCoroutine ("startMyDia");
	}
	IEnumerator  startMyDia(){
		GamePause.pauseGame ();
		yield return new WaitForSeconds (2f);
		GamePause.continueGame ();
		DialogSystem.character[] cArr={
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER
		};
		
		string[] dialog = new string[]{
			"……??? Where is here? Lab 4221? Why it looks so dim and old?",
			"I remember I fall at sleep in the lab during class, have I slept too long?", 
			"By the way, I think it is evening already. \nI better leave UST and go back home."
		};
		
		conversation(cArr,dialog,1);
	}


	void  startOp(){

		DialogSystem.character c=

			DialogSystem.character.PLAYER
		;
		
		string dialog = 
			"Am I handsome?";

		
		optionSelect(c,dialog,2);
	}

	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish");
		if(id==1)StartCoroutine ("startOp");
	}
}
