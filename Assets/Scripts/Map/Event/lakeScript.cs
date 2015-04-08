using UnityEngine;
using System.Collections;

public class lakeScript : MonoBehaviour {

	CreateItem ciObj;
	string itemName;
	
	void Start() {
		ciObj = GetComponent<CreateItem>();
	}

	 void OnTriggerStay(Collider other)
	 {
		Debug.Log ("Lake Get Something!!");
		if(other.gameObject.tag=="Item"){
			Debug.Log(other.name);
/*			switch (other.name){
				case "knife":
					StartCoroutine(ciObj.giveItemToPlayer(0));
					break;
				case "potion":
					StartCoroutine(ciObj.giveItemToPlayer(1));
					break;
			}
*/			itemName = other.name;
			Destroy(other.gameObject);    
			GetComponent<LakeDialogInterface>().startLakeDialog(this, itemName);
		}
	 }
	 
	 public void dialogCallBack(bool getKey){
		 if(getKey)
			 StartCoroutine(ciObj.giveItemToPlayer("nose"));
		 else
			StartCoroutine(ciObj.giveItemToPlayer(itemName));
	 }
	
}
