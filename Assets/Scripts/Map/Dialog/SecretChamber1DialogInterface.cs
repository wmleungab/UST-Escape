using UnityEngine;
using System.Collections;

public class SecretChamber1DialogInterface : DialogInterface {

	// Use this for initialization
	void Start () {
		StartCoroutine ("startBeginningDialog");
	}
	IEnumerator  startBeginningDialog ()
	{
		yield return new WaitForSeconds (1f);
		
		DialogSystem.character[] cArr = {
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER,
			DialogSystem.character.DIMJACK,
			DialogSystem.character.PLAYER,
			DialogSystem.character.DIMJACK,
			DialogSystem.character.PLAYER,
			DialogSystem.character.DIMJACK,
			DialogSystem.character.PLAYER,
			DialogSystem.character.DIMJACK,
			DialogSystem.character.PLAYER,
			DialogSystem.character.DIMJACK
		};
		
		string[] dialog = new string[]{
			"Where is here? A chamber under sundial?",
			"Jack! Is you! Oh my god, you are fine.",
			"...",
			"Why didn’t you wake me up this afternoon,I slept too long.\n" +
			"By the way, let’s get back home together.",
			"No way! I am here to control all people, \n" +
			"and become the king of the world by using my polluted food!", 
			"King? By your food?\nHow can you be the king within these few hours?",
			"It is not few hours, it is already 30 years! Now is 2046!\n"+
			"That’s why you see the things getting so old.\n",
			"What! 2046? I cannot believe with you,\nwhy I am still looked so young?",
			"You came to the future, the rotated sundial is the proof.\nTime pass, it rotates.",
			"... No matter what you say! I have to stop you!\nGo back home with me!",
			"I say NO WAY!!!"
			
		};
		
		conversation (cArr, dialog, 632);
		Debug.Log ("Started conversation");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public void  onDialogFinish (int id, int selection)
	{
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finished with selection result " + selection);
		//if(id==632)
		
	}
}
