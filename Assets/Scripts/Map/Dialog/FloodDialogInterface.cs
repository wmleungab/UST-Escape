using UnityEngine;
using System.Collections;

public class FloodDialogInterface : DialogInterface {

	public void startDialog(){

		DialogSystem.character[] nameString ={
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER
		};
		string[] dialogString = new string[]{
			"Yeah!! I remove some water......still tons of water to go ._.",
			"Let me find a place to pour the water away"
		};
		//dsObj.startDialog(nameString, dialogString);
		conversation (nameString, dialogString, 1);
	}

	public void startDialog2(){
		
		DialogSystem.character[] nameString ={
			DialogSystem.character.PLAYER
		};
		string[] dialogString = new string[]{
			"How many times should I do this..."
		};
		//dsObj.startDialog(nameString, dialogString);
		conversation (nameString, dialogString, 1);
	}

	public void startBattleDialog(){
		
		DialogSystem.character[] nameString ={
			DialogSystem.character.DEMBEATER_FLAG,
			DialogSystem.character.DEMBEATER_LEG,
			DialogSystem.character.PLAYER,
			DialogSystem.character.DEMBEATER_FLAG,
			DialogSystem.character.DEMBEATER_LEG,
			DialogSystem.character.DEMBEATER_FLAG
		};
		string[] dialogString = new string[]{
			"Hey, that handsome guy there! \nLet's join our society. There are many welfare waiting for you",
			"Let's join our society",
			"No",
			"What?! \nNobody ever refused me! I can't believe this!!!",
			"I can't believe this!!!",
			"Go to hell NOW!"
		};
		//dsObj.startDialog(nameString, dialogString);
		conversation (nameString, dialogString, 3);
	}

	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish");
		if (id == 3)
						GetComponent<FloodEvent>().battleDialogCallback ();
	}
}
