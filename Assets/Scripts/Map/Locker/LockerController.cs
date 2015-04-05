using UnityEngine;
using System.Collections;

public class LockerController : MonoBehaviour {

	private LockerScript numberInputObj;
	private static bool isOpened = false;
	private GameObject playerObj;
	
	public float maxTouchingDistance = 5.0f;

	// Use this for initialization
	void Start () {
		numberInputObj = transform.Find("cal").GetComponent<LockerScript>();
		if(numberInputObj == null) {
			Debug.LogError("Number Input Object not set for Locker");
		}
		numberInputObj.closePanel();
		
		playerObj = GameObject.FindWithTag("Player");
		if (playerObj==null) Debug.LogError("Player Object not found");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void openLocker () {
		if(!isOpened){
			Debug.Log("Locker opened");
			//insert script to get item
			StartCoroutine(GetComponent<CreateItem>().giveItemToPlayer(0));
			StartCoroutine(GetComponent<CreateItem>().giveItemToPlayer(1));
			isOpened = true;
		}
	}
	
	void colliderOnClick(){
		if (Vector3.Distance(playerObj.transform.position, transform.position) < maxTouchingDistance){
			numberInputObj.showPanel();
		}
	}
}
