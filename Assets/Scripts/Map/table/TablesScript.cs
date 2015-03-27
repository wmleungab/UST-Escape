using UnityEngine;
using System.Collections;

public class TablesScript : BigTableScript {

	public bool[] answerArray;
	private bool[] tablesOpen;

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
			if(checkArray()){
				Debug.Log("get it right!");
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
}
