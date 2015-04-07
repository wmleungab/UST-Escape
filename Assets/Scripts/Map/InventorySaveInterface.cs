using UnityEngine;
using System.Collections;

public class InventorySaveInterface : MonoBehaviour {

	string[] outputSaveList(){
		string childString = "";
		foreach (Transform child in transform)
		{
			//child is your child transform
			childString += child.name + ",";
		}
		childString = childString.Remove(childString.Length - 1);
	}

}
