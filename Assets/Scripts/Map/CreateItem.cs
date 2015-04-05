﻿using UnityEngine;
using System.Collections;

public class CreateItem : MonoBehaviour {
	
	public GameObject[] objList;
	bool loading = false;
	bool toMoveToPlayer = false;
	private Transform item;

	IEnumerator LoadObj(int index) { 
	
			loading = true; yield return null; // render another frame to make sure the label is drawn.

			item = ((GameObject)Instantiate(objList[index])).transform;
			item.name = objList[index].name;
		 
			loading = false;

	}
	
	public bool isLoading(){
		return loading;
	}
	
	public IEnumerator giveItemToPlayer(int index){
		
		while(toMoveToPlayer){
			yield return new WaitForSeconds(0.1f);
		}		
		//yield return null;
		if(toMoveToPlayer || loading) Debug.LogError("Not Enough Time to handle this item " + index);
		StartCoroutine(LoadObj(index));
		toMoveToPlayer = true;
		
	}

	// Update is called once per frame
	 void Update() {
		 if(toMoveToPlayer && !loading){
			 Debug.Log(item);
			 gameObject.SendMessage("itemToPlayer", item);
			 item = null;
			 toMoveToPlayer = false;
		 }
	 }
}
