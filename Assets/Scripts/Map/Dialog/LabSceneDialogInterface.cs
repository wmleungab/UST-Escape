using UnityEngine;
using System.Collections;

public class LabSceneDialogInterface : DialogInterface
{



		// Use this for initialization
		void Start ()
		{

				StartCoroutine ("wait1Sec");
				
		}

		IEnumerator  wait1Sec ()
		{
				GamePause.pauseGame ();
				yield return new WaitForSeconds (0.7f);
				GamePause.continueGame ();
				if (!SaveLoadSystem.getInstance ().labSceneState [(int)SaveLoadSystem.LabSceneState.BEGANDIALOG])
						StartCoroutine ("startBeginningDialog");
				else {
						StartCoroutine ("startLoopHint1");
				}
		}

		IEnumerator  startBeginningDialog ()
		{
			yield return new WaitForSeconds (0.3f);

			DialogSystem.character[] cArr = {
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER
			};
		
				string[] dialog = new string[]{
			"……??? Where is here? Lab 4221? Why it looks so dim and old?",
			"I remember I fall at sleep in the lab during class, have I slept too long?", 
			"By the way, I think it is evening already. \nI better leave UST and go back home."
			};
		
			conversation (cArr, dialog, 1);
		}

		IEnumerator  startLoopHint1 ()
		{
				Debug.Log ("Lab scene: Hint1 will be show after 180s");
				yield return new WaitForSeconds (180f);
				DialogSystem.character[] cArr = {
			DialogSystem.character.PLAYER
		};
		
				string[] dialog = new string[]{
			"There are some words on the white boards, it is useful?"
		};
		
				conversation (cArr, dialog, 2);
		}

		IEnumerator  startLoopHint2 ()
		{
				Debug.Log ("Lab scene: Hint2 will be show after 180s");
				yield return new WaitForSeconds (180f);
				DialogSystem.character[] cArr = {
			DialogSystem.character.PLAYER
		};
		
				string[] dialog = new string[]{
			"So many computers here, let me check them out."
		};
		
				conversation (cArr, dialog, 3);
		}
		/*
	void  startBp(){
		
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

		showBigIcon(cArr,dialog,3,spriteToShow);

	}
*/
		override public void  onDialogFinish (int id, int selection)
		{
				//selection -1: No selection carried out 0; false or no 1: true or yes
				Debug.Log ("Dialog with id " + id + "has finished with selection result " + selection);
				if (id == 1) {
						StartCoroutine ("startLoopHint1");
						SaveLoadSystem.getInstance ().labSceneState [(int)SaveLoadSystem.LabSceneState.BEGANDIALOG] = true;
						SaveLoadSystem.getInstance ().save ();
				}
				if (id == 2)
						StartCoroutine ("startLoopHint2");
				if (id == 3)
						StartCoroutine ("startLoopHint1");

		}
}
