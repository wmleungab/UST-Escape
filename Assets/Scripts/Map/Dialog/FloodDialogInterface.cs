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
			"How many times should I do this...",
		};
		//dsObj.startDialog(nameString, dialogString);
		conversation (nameString, dialogString, 1);
	}

	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish");
	}
}
