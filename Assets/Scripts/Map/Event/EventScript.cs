using UnityEngine;
using System.Collections;

public abstract class EventScript : MonoBehaviour {

	bool isFinished = false;
	int dialogCount = 0;
	private GameObject playerObj;
	
	bool startFK = false;

	public float maxTouchingDistance = 5.0f;
	public string keyItemName = "";

	// Use this for initialization
	public virtual void Start() {
		Debug.Log("Hello");
		playerObj = GameObject.FindWithTag("Player");
		if (playerObj==null) Debug.LogError("Player Object not found");
	}
	public void Update () {
	}
	
	virtual public void colliderOnClick(){
		if (Mathf.Abs(Vector3.Distance(playerObj.transform.position, transform.position)) < maxTouchingDistance){
			
			Debug.Log("collider Clicked (Event)");
			startFindKey(keyItemName);

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
				GetComponent<EventDialogInterface>().startDialog(keyObj, this);
				dialogCount++;
			}
		}
	}
	
	public void dialogCallback(int dialogID, Transform keyObj){
		startEvent(keyObj);
		gameObject.SendMessage("removeItem", keyObj);
	}
	
	abstract public void startEvent (Transform keyObj) ;
	
	 virtual public void colliderTriggerStay(Collider other)
	 {
		Debug.Log ("Event Object Get Something!!");
		if(other.gameObject.tag=="Item"){
			Debug.Log(other.name);
			if (other.name == keyItemName){
					startEvent(other.transform);
					Destroy(other.gameObject);    
			}
		}
	 }
}
