using UnityEngine;
using System.Collections;

public class LockerDialogInterface : DialogInterface {

	public Sprite closeLockerSprite;
	public Sprite openLockerSprite;
	LockerController lockerObj;

	public void startCloseIntroDialog(LockerController _lockerObj){
		lockerObj = _lockerObj;

		DialogSystem.character[] nameString ={
			DialogSystem.character.SYSTEM
		};
		string[] dialogString = new string[]{
			"It is a locker."
		};
		//dsObj.startDialog(nameString, dialogString);
		showBigIcon (nameString, dialogString, 1, closeLockerSprite);
	}
	public void startOpenIntroDialog(LockerController _lockerObj){
		lockerObj = _lockerObj;

		DialogSystem.character[] nameString ={
			DialogSystem.character.SYSTEM
		};
		string[] dialogString = new string[]{
			"The locker is opened"
		};
		//dsObj.startDialog(nameString, dialogString);
		showBigIcon (nameString, dialogString, 2, openLockerSprite);
	}
	
	public void startFailDialog(LockerController _lockerObj){
		lockerObj = _lockerObj;

		DialogSystem.character[] nameString ={
			DialogSystem.character.SYSTEM,
			DialogSystem.character.PLAYER
		};
		string[] dialogString = new string[]{
			"There is no reaction",
			"There must be expensive things in the locker, I wish I know the password..."
		};
		//dsObj.startDialog(nameString, dialogString);
		showBigIcon (nameString, dialogString, 2, closeLockerSprite);
	}
	
	public void startSuccessDialog(LockerController _lockerObj){
		lockerObj = _lockerObj;

		DialogSystem.character[] nameString ={
			DialogSystem.character.PLAYER
		};
		string[] dialogString = new string[]{
			"Oh yes! I got it!"
		};
		//dsObj.startDialog(nameString, dialogString);
		showBigIcon (nameString, dialogString, 4, openLockerSprite);
	}
	
	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish");
		if(lockerObj!=null && id==1) lockerObj.closeIntroDialogCallBack();
		if(lockerObj!=null && id==4) lockerObj.successDialogCallBack();
	}
}
