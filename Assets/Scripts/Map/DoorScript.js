public var keyName : String;

static var playersinv : Inventory;
private var isOpened : boolean = false;
private var doorObj : GameObject;

function Start () {

	playersinv = FindObjectOfType(Inventory); //finding the players inv.
	isOpened = false;
	doorObj = GameObject.Find("opendoor");
	doorObj.active = false;

}

//Removed an item from the inventory (IT DOESN'T DROP IT).
function findKey() : Transform
{
	for(var i:Transform in playersinv.Contents) //Loop through the Items in the Inventory:
	{
		if(i.name == keyName) //When a match is found, remove the Item.
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
	doorObj.active = true;
}

function OnTriggerEnter() {
	
	Debug.Log("trigger enters");
	
	if(!isOpened) {
/*		// find if the player has the key
		var keyChild : Transform;
		
		keyChild = findKey();
		
		// if player has the key
		if(keyChild) {
			Debug.Log ("Key used");
			isOpened = true;
			playersinv.RemoveItem(keyChild);
			goToNextScene();
		}
*/
	}
	else {
		goToNextScene();
	}
	
}

function goToNextScene() {
		Debug.Log("go to next scene");	
}
