using UnityEngine;
using System.Collections;

public class treasureBoxScript : MonoBehaviour {

	bool isOpened = false;
	int dialogCount = 0;
	private GameObject playerObj;
	
	bool startFK = false;

	public float maxTouchingDistance = 5.0f;

	public enum TBKey
	{
			SIMPLEKEY=1,
			BEAUTIFULKEY
	}
	;

	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindWithTag("Player");
		if (playerObj==null) Debug.LogError("Player Object not found");
	}
	
	void colliderOnClick(){
		if (!GamePause.isPause() && (Mathf.Abs(Vector3.Distance(playerObj.transform.position, transform.position)) < maxTouchingDistance)){
			if(dialogCount<5){
				startFindKey("key");
			}
			else{
				startFindKey("beautifulKey");
			}

		}
	}
	
	void startFindKey(string keyName){
		startFK = true;
			gameObject.SendMessage("findKey", keyName);
	}
	void findKeyCallback(Transform keyObj){
		if(startFK){
			startFK = false;
			// if player has the key
			if(keyObj) {
				Debug.Log ("Key found");
				GetComponent<TBDialogInterface>().startDialog(keyObj, this);
				dialogCount++;
			}
		}
	}
	void findKeyFailCallback(){
		if(startFK){
			startFK = false;
			GetComponent<TBDialogInterface>().startIntroDialog();
		}
	}	
	public void dialogCallback(int dialogID, Transform keyObj){
		TBKey keyUsed = 0;
		if(dialogID==1) keyUsed = TBKey.SIMPLEKEY;
		else if(dialogID==2) keyUsed = TBKey.BEAUTIFULKEY;
		openLocker(keyUsed);
		gameObject.SendMessage("removeItem", keyObj);
	}
	
	void openLocker (TBKey keyUsed) {
		if(keyUsed == TBKey.BEAUTIFULKEY && !isOpened){
			Debug.Log("Locker opened");
			//insert script to get item
			StartCoroutine(GetComponent<CreateItem>().giveItemToPlayer(0));
			StartCoroutine(GetComponent<CreateItem>().giveItemToPlayer(1));
			isOpened = true;
		}
		if(keyUsed == TBKey.SIMPLEKEY){
			Debug.Log("Treasure box Monster");
			GetComponent("sceneInterface").SendMessage("gotoBattle");
		}
	}
	
	 void colliderTriggerStay(Collider other)
	 {
		Debug.Log ("TB Get Something!!");
		if(other.gameObject.tag=="Item"){
			TBKey keyUsed = 0;
			Debug.Log(other.name);
			switch (other.name){
				case "key":
					keyUsed = TBKey.SIMPLEKEY;
					openLocker(keyUsed);
					Destroy(other.gameObject);    
					break;
				case "beautifulKey":
					keyUsed = TBKey.BEAUTIFULKEY;
					openLocker(keyUsed);
					Destroy(other.gameObject);    
					break;
			}
		}
	 }
}
