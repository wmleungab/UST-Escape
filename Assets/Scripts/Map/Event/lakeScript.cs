using UnityEngine;
using System.Collections;

public class lakeScript : MonoBehaviour {

	CreateItem ciObj;
	
	void Start() {
		ciObj = GetComponent<CreateItem>();
	}

	 void OnTriggerStay(Collider other)
	 {
		Debug.Log ("Lake Get Something!!");
		if(other.gameObject.tag=="Item"){
			Debug.Log(other.name);
			switch (other.name){
				case "knife":
					StartCoroutine(ciObj.giveItemToPlayer(0));
					break;
				case "potion":
					StartCoroutine(ciObj.giveItemToPlayer(1));
					break;
			}
			Destroy(other.gameObject);    
		}
	 }
	
}
