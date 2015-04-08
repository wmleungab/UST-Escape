using UnityEngine;
using System.Collections;

public class PredFallDialogInterface : DialogInterface {

	
	public void startFirstDialog(){

		DialogSystem.character[] nameString ={
			DialogSystem.character.PLAYER,
			DialogSystem.character.PRINCIPAL,
			DialogSystem.character.PRINCIPAL,
			DialogSystem.character.PRINCIPAL,
			DialogSystem.character.PLAYER
			};
		string[] dialogString = new string[]{
			"Are you alright?!",
			"Thank you... \nBut I think I am not holding long...",
			"Listen carefully!\nI was a rich man when I am young.\nI put all of my treasure THERE",
			"You must go to find it...\n 9413, remember, 9413",
			"...\n(It seems there is a locker in the atrium, maybe I should go to check)"
		};

		conversation (nameString, dialogString, 1);
	}

	public void startNormalDialog(){

	DialogSystem.character[] nameString ={
			DialogSystem.character.PRINCIPAL,
			DialogSystem.character.PLAYER,
			DialogSystem.character.PRINCIPAL,
			DialogSystem.character.PLAYER
		};
		string[] dialogString = new string[]{
			"I can see many mouse... \nAm I in a theme park?...",
			"Hey! I have get rid of the mouse already!",
			"9413\n9413...\n941...3......",
			"**sigh** \nThis guy has gone crazy already\nI should just take all of his money as he has told me"
		};

		conversation (nameString, dialogString, 2);
	}
	
	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish");
	}

}
