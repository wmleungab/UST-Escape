using UnityEngine;
using System.Collections;

public class itmToPlayer : MonoBehaviour {

	public Hashtable itemList;
	public GameObject[] objList;

	// Use this for initialization
	void Start () {
		itemList = new Hashtable();
		itemList.Add("knife", objList[0]);
		itemList.Add("Block", objList[1]);
	}
	
	public GameObject createItem(string itemName) {
		Debug.Log(itemList[itemName]);
		GameObject go = (GameObject)Instantiate((GameObject)itemList[itemName]); ;
		return go;
	}
}
