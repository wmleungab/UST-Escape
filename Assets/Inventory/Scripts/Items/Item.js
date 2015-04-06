#pragma strict
#pragma implicit
#pragma downcast

var itemIcon : Texture2D; //The Icon.
var canGet = true; //If we can pick up the Item.
var maxGettingDistance : float = 0;
var itemType : String; //This will let us equip the item to specific slots. Ex: Head, Shoulder, or whatever we set up. If the item is equipment (or weapon) this needs to match a slot to work properly.
var stackable = false; //Is it stackable? If yes then items with the same itemType will be stacked.
var maxStack = 20; //How many Items each stack can have before creating a new one. Remember that the Items that should be stacked should have the same itemType.
var stack = 1; //This is how many stack counts this Item will take up.
var isEquipment = true; //Can the Item be equipped? This includes weapons.
var isAlsoWeapon = false; //Is the Item also a Weapon? This only works with isEquipment set to true.

//This is the object we will instantiate in the Players hand.
//We use this so we can have two versions of the weapon. One for picking up and one for using.
var equippedWeaponVersion : Transform;

//These will store information about usefull components.
var playersinv : Inventory;

private var FPPickUpFound = false;

@script AddComponentMenu ("Inventory/Items/Item")

//Here we find the components we need.
function Start ()
{
	var player = GameObject.FindGameObjectWithTag ("Player");
	Debug.Log("Item: "+this.name + " belongs "+player.name);
	playersinv = player.Find("Inventory").GetComponent(Inventory); //finding the players inv.
	if (playersinv == null)
	{
		canGet = false;
		Debug.LogWarning("No 'Inventory' found in game. The Item " + transform.name + " has been disabled for pickup (canGet = false).");
	}
	else
	{
		gameObject.SendMessage("RetrievePlayer", playersinv, SendMessageOptions.DontRequireReceiver);
	}
	
	if (isEquipment == false && GetComponent(ItemEffect) == null)
	{
		Debug.LogError(gameObject.name + " is not equipment so please assign an ItemEffect script to it");
	}
	
	if (GetComponent(FirstPersonPickUp) != null)
	{
		FPPickUpFound = true;
	}
	else if (transform.GetComponentInChildren(FirstPersonPickUp) != null)
	{
		FPPickUpFound = true;
	}
}

//When you click an item
function OnMouseDown()
{
	var playerObj = GameObject.FindWithTag ("Player");

	Debug.Log("Item Clicked");
	//If the 'FirstPersonPickUp' script is not attached we want to pick up the item.
	if (FPPickUpFound == false && (Vector3.Distance(playerObj.transform.position, transform.position) < maxGettingDistance) && !GamePause.isPause() )
	{
		PickUpItem();
	}
}

//Picking up the Item.
function PickUpItem ()
{
	var getit=true;
	if(canGet){//if its getable or hasnt been gotten.
	
	playersinv.gameObject.SendMessage ("PlayPickUpSound", SendMessageOptions.DontRequireReceiver); //Play sound
	//GameObject.Find("DialogSystem").SendMessage("startDialog");
	gameObject.GetComponent("DialogInterface").SendMessage("getItemDialog", SendMessageOptions.DontRequireReceiver);
	
		if(stackable){
			var locatedit:Item;
			for(var t:Transform in playersinv.Contents){
				if(t.name==this.transform.name){//if the item we wanna stack this on has the same name
					var i:Item=t.GetComponent(Item);
					if(i.stack<i.maxStack){
						locatedit=i;
					}
				}
			}
			if(locatedit!=null){//if we have a stack to stack it to!
				getit=false;
				locatedit.stack+=1;
				Destroy(this.gameObject);
			}
			else{
				getit=true;
			}
		}
		//If we can get it and the inventory isn't full.
		if (getit && playersinv.Contents.length < playersinv.MaxContent)
		{
			playersinv.AddItem(this.transform);
			MoveMeToThePlayer(playersinv.itemHolderObject);//moves the object, to the player
			Debug.Log("move " + this.name + " to " + playersinv.itemHolderObject + " of " + playersinv.transform.parent);
		}
		else if (playersinv.Contents.length >= playersinv.MaxContent)
		{
			Debug.Log("Inventory is full");
		}
	}
}

//Moves the item to the Players 'itemHolderObject' and disables it. In most cases this will just be the Inventory object.
function MoveMeToThePlayer(itemHolderObject : Transform)
{
	canGet = false;
	
	//gameObject.SetActive(false);	It's normally best to disable the individual components so we can keep item effects and update functions alive.
	
/* 	if (GetComponent(MeshRenderer) != null)
	{
		GetComponent(MeshRenderer).enabled = false;
	}
	
	if (GetComponent(SpriteRenderer) != null)
	{
		GetComponent(SpriteRenderer).enabled = false;
	}
	
	if (GetComponent(Collider) != null)
	{
		GetComponent(Collider).enabled = false;
	} */
	
	ControlRenderers(false);
	
	(GetComponent("Item") as Item).enabled = false;
	
	transform.parent = itemHolderObject;
	transform.localPosition = Vector3.zero;
}

//Drops the Item from the Inventory.
function DropMeFromThePlayer(makeDuplicate : boolean)
{
	if (makeDuplicate == false) //We use this if the object is not stacked and so we can just drop it.
	{
		canGet = true;
		gameObject.SetActive(true);
		
/* 		if (GetComponent(MeshRenderer) != null)
		{
			GetComponent(MeshRenderer).enabled = true;
		}
		
		if (GetComponent(Collider) != null)
		{
			GetComponent(Collider).enabled = true;
		}
 */	
		ControlRenderers(true);
		(GetComponent("Item") as Item).enabled = true;
		
		transform.parent = null;
		DelayPhysics();
	}
	else //If the object is stacked we need to make a clone of it and drop the clone instead.
	{
		canGet = true;
		clone = Instantiate(gameObject, transform.position, transform.rotation);
		canGet = false;
		clone.SetActive(true);
		
 		if (clone.GetComponent(MeshRenderer) != null)
		{
			clone.GetComponent(MeshRenderer).enabled = true;
		}
		
		if (clone.GetComponent(Collider) != null)
		{
			clone.GetComponent(Collider).enabled = true;
		}
 	
		(clone.GetComponent("Item") as Item).ControlRenderers(true);
		(clone.GetComponent("Item") as Item).enabled = true;
		
		clone.transform.parent = null;
		clone.name = gameObject.name;
	}
}

//Drops the Item from the Inventory.
function DropMeToLocation(makeDuplicate : boolean, pos : Vector3)
{
	gameObject.GetComponent("DialogInterface").SendMessage("dropItemDialog", SendMessageOptions.DontRequireReceiver);
	if (makeDuplicate == false) //We use this if the object is not stacked and so we can just drop it.
	{
		canGet = true;
		gameObject.SetActive(true);
		
/* 		if (GetComponent(MeshRenderer) != null)
		{
			GetComponent(MeshRenderer).enabled = true;
		}
		
		if (GetComponent(Collider) != null)
		{
			GetComponent(Collider).enabled = true;
		}
 */	
		ControlRenderers(true);
		(GetComponent("Item") as Item).enabled = true;
		
		transform.parent = null;
		transform.position = pos;
		DelayPhysics();
	}
	else //If the object is stacked we need to make a clone of it and drop the clone instead.
	{
		canGet = true;
		clone = Instantiate(gameObject, transform.position, transform.rotation);
		canGet = false;
		clone.SetActive(true);
		
 		if (clone.GetComponent(MeshRenderer) != null)
		{
			clone.GetComponent(MeshRenderer).enabled = true;
		}
		
		if (clone.GetComponent(Collider) != null)
		{
			clone.GetComponent(Collider).enabled = true;
		}
 	
		(clone.GetComponent("Item") as Item).ControlRenderers(true);
		(clone.GetComponent("Item") as Item).enabled = true;
		
		clone.transform.parent = null;
		clone.name = gameObject.name;
		clone.transform.position = pos;
	}
}

function DelayPhysics ()
{
	if (playersinv.transform.parent.collider != null && collider != null)
	{
		Physics.IgnoreCollision(playersinv.transform.parent.collider, collider, true);
		yield WaitForSeconds (1);
		Physics.IgnoreCollision(playersinv.transform.parent.collider, collider, false);
	}
}

function ControlRenderers(enabled : boolean){
	
	if (GetComponent(MeshRenderer) != null)
	{
		GetComponent(MeshRenderer).enabled = enabled;
	}
	
	
	if (GetComponent(Collider) != null)
	{
		GetComponent(Collider).enabled = enabled;
	}

	for (var child : Transform in this.transform)
    {
        child.gameObject.SetActive (enabled);
    }

}

//Drawing an 'I' icon on top of the Item in the scene to keep organised.
function OnDrawGizmos ()
{
	Gizmos.DrawIcon (Vector3(transform.position.x, transform.position.y + 1, transform.position.z), "ItemGizmo.png", true);
}