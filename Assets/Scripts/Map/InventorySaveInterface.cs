﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CreateItem))]
public class InventorySaveInterface : MonoBehaviour {

	static bool loaded = false;

	void Start(){
		startLoading ();
	}

	string[] outputSaveList(){
		string childString = "";
		foreach (Transform child in transform)
		{
			//child is your child transform
			childString += child.name + ",";
		}
		if(childString.Length>0) childString = childString.Remove(childString.Length - 1);
		Debug.Log (childString);

		SaveLoadSystem.getInstance().setnSaveInventoryList (childString);
		string[] result = childString.Split (',');
		Debug.Log (array2String(result));
		return result;
	}

	string array2String(string[] strArray) {
		string result = "InventoryList={";
		
		foreach (string arrayItem in strArray){
			result += (arrayItem + ",");
		}
		
		result = result.Remove(result.Length - 1);
		result += "}";
		
		return result;
	}

	void startLoading(){
		if (!loaded) {
			string itemstr = SaveLoadSystem.getInstance ().getnLoadInventoryList ();
			string[] itemList = itemstr.Split (',');

			if (itemstr.Length > 0) {
					Debug.Log (itemstr);

					loadFromList (itemList);
					loaded = true;
			}
		}
	}

	void loadFromList(string[] itemList) {
		CreateItem ciObj = GetComponent<CreateItem> ();
			foreach(string itemName in itemList){
			StartCoroutine (ciObj.giveItemToPlayer (itemName));
		}
	}
	
}
