using UnityEngine;
using System.Collections;

public class TablesScript : BigTableScript {

	public bool[] answerArray;
	private bool[] tablesOpen;
	TableDialogInterface dialogInterface;
	public Sprite pcOn;
	public Sprite pcOff;

	// Use this for initialization
	void Start () {

		if (answerArray.Length != noOfTables){
			Debug.LogError("TablesScript: answerArray not initialized correctly");
		}
		tablesOpen = new bool[noOfTables];
	}

	//return if answer correct
	public override bool openTable(int tableNo) {
		if(tableNo >= 0 && tableNo < tablesOpen.Length){
			tablesOpen[tableNo] = !tablesOpen[tableNo];
			Debug.Log("Big Table Array: " + this.getTablesOpen());
			issueDialog(tableNo);

			if(checkArray()){
				Debug.Log("get it right!");
				GameObject.Find("Door").gameObject.SendMessage("openDoor", true, SendMessageOptions.DontRequireReceiver);
				return true;
			}
		}
		else {
			Debug.LogError("tableNo " + tableNo + " not valid");
		}
		return false;
	}
	
	bool checkArray() {
		for (int i=0; i<tablesOpen.Length; i++){
			if (tablesOpen[i] != answerArray[i]){
				return false;
			}
		}
		return true;
	}
	
	string getTablesOpen() {
		string result = "tablesOpen={";
		
		foreach (bool arrayItem in tablesOpen){
			result += (arrayItem + ",");
		}
		
		result = result.Remove(result.Length - 1);
		result += "}";
		
		return result;
	}

	void issueDialog(int tableNo){
		dialogInterface = gameObject.GetComponents<TableDialogInterface> ()[0];
		DialogSystem.character[] c = {DialogSystem.character.SYSTEM};
		string tableState;
		if (tablesOpen [tableNo])
						tableState = "turned On";
				else
						tableState = "turned Off";
		string statement="Computer "+(tableNo+1)+" is "+tableState;
		string[] s = {statement};
		if (tablesOpen [tableNo])
						dialogInterface.showBigIcon (c, s, 111, pcOn);
		else dialogInterface.showBigIcon (c, s, 111, pcOff);
	}
}
