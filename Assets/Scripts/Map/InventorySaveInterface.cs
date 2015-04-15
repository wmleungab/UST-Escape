using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CreateItem))]
public class InventorySaveInterface : MonoBehaviour {

	void Start(){
			Debug.Log("Start Loading Inventory");
			//gameObject.SendMessage("startLoading");
			startLoadingInventory();
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

	void startLoadingInventory(){
			string itemstr = SaveLoadSystem.getInstance ().getnLoadInventoryList ();
			Debug.Log("Inventory LOading: " + itemstr);
			string[] itemList = itemstr.Split (',');


			if (itemstr.Length > 0) {
					Debug.Log (itemstr);

					loadFromList (itemList);

				StartCoroutine(waitFor10Seconds());
			}
	}

	void loadFromList(string[] itemList) {
		CreateItem ciObj = GetComponent<CreateItem> ();
			foreach(string itemName in itemList){
			StartCoroutine (ciObj.giveItemToPlayer (itemName));
		}
	}

	IEnumerator waitFor10Seconds(){
		ItemDialogInterface.showDialog = false;
		yield return new WaitForSeconds(2);
		var equipstr = SaveLoadSystem.getInstance ().getnLoadEquipmentList ();
		gameObject.SendMessage("equipFromList",  equipstr);
		ItemDialogInterface.showDialog = true;
	}

}
