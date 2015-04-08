using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	private GameObject playerObj;
	public float maxTouchingDistance = 5.0f;
	int clickCount = 0;

	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindWithTag("Player");
		if (playerObj==null) Debug.LogError("Player Object not found");

	}
	
	public virtual void colliderOnClick(){
		if (Mathf.Abs(Vector3.Distance(playerObj.transform.position, transform.position)) < maxTouchingDistance){
			
			//Debug.Log("collider Clicked (President)");
			if (clickCount == 0) {
				GetComponent<DialogInterface>().SendMessage("startFirstDialog");
			}
			else {
				GetComponent<DialogInterface>().SendMessage("startNormalDialog");
			}
			
			clickCount++;
		}
	}
	
	public virtual void dialogCallBack(){
	}
	
}
