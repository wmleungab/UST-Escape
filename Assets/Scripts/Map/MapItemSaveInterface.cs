using UnityEngine;
using System.Collections;

public class MapItemSaveInterface : MonoBehaviour {

	// Use this for initialization
	void Start () {
		startLoading ();
	
	}
	
	void startLoading(){
			string itemstr = SaveLoadSystem.getInstance ().getnLoadInventoryList ();
			string[] itemList = itemstr.Split (',');
			
			if (itemstr.Length > 0) {
				Debug.Log (itemstr);
				
				loadFromList (itemList);
			}
	}
	
	void loadFromList(string[] itemList) {
		CreateItem ciObj = GetComponent<CreateItem> ();
		foreach(string itemName in itemList){
			Transform temp = transform.Find (itemName);
			if(temp!=null){
				Destroy(temp.gameObject);
			}
		}
	}



}
