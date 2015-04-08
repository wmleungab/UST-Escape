using UnityEngine;
using System.Collections;

public class LakeDialogInterface : DialogInterface {

	public string keyItemName;

	lakeScript lakeObj;
	string itemName;

	private int dialogCount = 0;
	private bool getKey = false;

	public void startLakeDialog(lakeScript _lakeObj, string _itemName){
		lakeObj = _lakeObj;
		itemName = _itemName;
		
		DialogSystem.character[] nameString ={
			DialogSystem.character.SYSTEM
			};
		string[] dialogString = new string[]{
			"**Someone appeared from the lake**"
		};

		conversation (nameString, dialogString, 0);
		
	}
	
	public void startDropDialog(){
		dialogCount++;

		string tempItem;
		if(dialogCount==1) tempItem = "Gold " + itemName;
		else if(dialogCount==2) tempItem = "Silver " + itemName;
		else{
			if(itemName == keyItemName){
				getKey = true;
				tempItem = "broken fake nose";
			}
			else tempItem = "broken " + itemName;
		}

		DialogSystem.character nameString = DialogSystem.character.GIRLGOD;
		string dialogString = "Is this " + tempItem + " yours ?";

		optionSelect (nameString, dialogString, dialogCount);
	}
	
	public void startByeDialog(){
		
		DialogSystem.character[] nameString ={
			DialogSystem.character.GIRLGOD,
			DialogSystem.character.GIRLGOD,
			DialogSystem.character.SYSTEM
			};
		string[] dialogString = new string[]{
			"You dishonest boy! \nPlease don't f_cking throwing rubbish to my home again!!",
			"I will keep this " + itemName + " as punishment!",
			"**He(She) disappeared into the lake again**"
		};

		conversation (nameString, dialogString, 4);
	}
	
	public void startSuccessDialog(){
		DialogSystem.character[] nameString ={
			DialogSystem.character.GIRLGOD,
			DialogSystem.character.SYSTEM
			};
		string[] dialogString = new string[]{
			"You are welcome to visit me again~ \nMuah~~(Doing air kiss)",
			"**He(She) disappeared into the lake again**"
		};

		conversation (nameString, dialogString, 4);
	}
	
	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish");
		
		if(id==0) startDropDialog();
		
		else if(selection==0){
			if(id<3) startDropDialog();
			else if(id==3){
				startByeDialog();
			}
		}
		else if (selection==1){
			if(id<3) startByeDialog();
			else if(id==3){
				lakeObj.dialogCallBack(getKey);
				startSuccessDialog();
			}
		}
		
		if(id>=3) dialogCount = 0;

	}

	}
