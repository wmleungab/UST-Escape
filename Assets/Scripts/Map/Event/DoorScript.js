public var keyName : String;

static var playersinv : Inventory;
private var isOpened : boolean = false;
private var doorObj : GameObject;

public var next_stage = "atrium_stage";
var firstTouch=true;

function Start () {
	Debug.Log("DoorScript's start function is called");
	playersinv = FindObjectOfType(Inventory); //finding the players inv.
	isOpened = false;
	doorObj = GameObject.Find("opendoor");
	doorObj.SetActive(false);

}

//find an item from the inventory (IT DOESN'T DROP IT).
function findKey() : Transform
{
	for(var i:Transform in playersinv.Contents) //Loop through the Items in the Inventory:
	{
		if(i.name == keyName) //When a match is found, return the Item.
		{
			Debug.Log("Key found");
			return i;
			//No need to continue running through the loop since we found our item.
		}
	}
	
	return null;
}

function openDoor(){
	Debug.Log("Lab Door Open");
	isOpened = true;
	doorObj.SetActive(true);
}

function OnTriggerEnter() {
	
	
	
	if(!isOpened) {
		Debug.Log("In touch with door, but not it is not open");
		GetComponent("DoorSaveInterface").SendMessage("trackleSaveAndDialog");

	}
	else {
		Debug.Log("In touch with door, and it is open");
		goToNextScene();
	}
	
}

function goToNextScene() {
		Debug.Log("go to next scene");	
		gameObject.SendMessage("changeScene",next_stage);
}
