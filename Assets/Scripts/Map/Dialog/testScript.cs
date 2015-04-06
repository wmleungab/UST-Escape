using UnityEngine;
using System.Collections;

public class testScript : DialogInterface {
	public Sprite spriteToShow;
	
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
			"…2dialog?",
			"I rememberat the same time!?", 
			"work ng work?"
		};
		
		conversation(cArr,dialog,1);
	}
	
	
	void  startOp(){
		
		DialogSystem.character c=
			
			DialogSystem.character.PLAYER
				;
		
		string dialog = 
			"ng work?";
		
		
		optionSelect(c,dialog,2);
	}
	
	void  startBp(){
		
		DialogSystem.character[] cArr={
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER
		};
		
		string[] dialog = new string[]{
			"…2dialog?",
			"I rememberat the same time!?", 
			"work ng work?"
		};
		
		showBigIcon(cArr,dialog,3,spriteToShow);
		
	}
	
	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finished with selection result "+selection);
		if(id==1)StartCoroutine ("startOp");
		if(id==2)StartCoroutine ("startBp");
	}
}
