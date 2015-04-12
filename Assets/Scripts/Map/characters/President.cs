using UnityEngine;
using System.Collections;

public class President : MonoBehaviour {

	private GameObject playerObj;
	public float maxTouchingDistance = 5.0f;
	public string giveItemName = "apple";
	int clickCount = 1;
	private GameObject zombies;

	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindWithTag("Player");
		if (playerObj==null) Debug.LogError("Player Object not found");
		if (SaveLoadSystem.getInstance ().atriumSceneStateArr [(int)SaveLoadSystem.AtriumSceneState.PREDDIALOG]){
			clickCount = 1;
		}
	}
	
	public void colliderOnClick(){
		if (Mathf.Abs(Vector3.Distance(playerObj.transform.position, transform.position)) < maxTouchingDistance){
			
			//Debug.Log("collider Clicked (President)");
				GetComponent<PresidDialogInterface>().startNormalDialog(this);

			clickCount++;
		}
	}

	public void startGiveItem(GameObject _zombies){
		zombies = _zombies;
		GetComponent<PresidDialogInterface>().startGiveItemDialog(this);
	}

	public void dialogCallBack(){
		giveItem();
	}
	
	void giveItem() {
		
		StartCoroutine(GetComponent<CreateItem>().giveItemToPlayer(giveItemName));
		SaveLoadSystem slObj = SaveLoadSystem.getInstance ();
		if(slObj != null){
			SaveLoadSystem.getInstance ().atriumSceneStateArr [(int)SaveLoadSystem.AtriumSceneState.PREDDIALOG] = true;
			SaveLoadSystem.getInstance ().save ();		
		}
		if (zombies)
			zombies.SetActive(true);
		
	}
}
