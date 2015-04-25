using UnityEngine;
using System.Collections;

public class SecretChamber2DialogInterface : DialogInterface {
	bool cleaner=true;
	// Use this for initialization
	void Start () {
		SaveLoadSystem.getInstance().currentSceneType= SaveLoadSystem.SceneType.UNDERSUN2;
		SaveLoadSystem.getInstance ().save ();
		StartCoroutine ("startBeginningDialog");
	}
	IEnumerator  startBeginningDialog ()
	{
		yield return new WaitForSeconds (0f);
		DialogSystem.character[] cArr = {
			DialogSystem.character.PLAYER,
			DialogSystem.character.DIMJACK,
			DialogSystem.character.PLAYER,
			DialogSystem.character.DIMJACK,
			DialogSystem.character.PLAYER,
			DialogSystem.character.DIMJACK,
			DialogSystem.character.PLAYER,
			DialogSystem.character.DIMJACK,
			DialogSystem.character.DIMJACK,
			DialogSystem.character.PLAYER
		};
		
		string[] dialog = new string[]{
			"Jack! Stop doing that! You are doing wrong thing!\n" +
			"Why do you want to control people?",

			"Because everyone will care about me if I was the king.\n" +
			"I have no friend in 2016!",

			"I am your friend, I care about you!",

			"No, you are not my friend!\n" +
			"In lab,I asked you to eat chicken nuggets with me,\n" +
			"but you didn't answer me!",

			"Because, I sleep! I cannot hear you!",

			"...You are liar, I will not believe you.", 
			
			"Is true, Jack, I really slept.\n" +
			"Come on, let get back, and eat chicken nuggets together!",

			"... Maybe, I am wrong.",

			"Would you forgive me?\n",

			"Of course, we are friend!"
			
		};

		
		conversation (cArr, dialog, 732);
		Debug.Log ("Started conversation");
	}
	
	// Update is called once per frame
	void Update () {
		if (cleaner) {
			if (GameObject.Find ("AutoFade") != null)
				Destroy (GameObject.Find ("AutoFade"));
			cleaner=false;
		}
	}
	
	override public void  onDialogFinish (int id, int selection)
	{
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finished with selection result " + selection);
		if (id == 732) {
			GamePause.pauseGame();
			AutoFade.LoadLevel("end_scene",1f,1f,Color.black);
		}
		
	}
}
