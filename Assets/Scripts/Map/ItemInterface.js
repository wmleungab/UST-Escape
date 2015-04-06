#pragma strict
private var associatedInventory : Inventory;

function Start(){
	
	var player = GameObject.FindGameObjectWithTag ("Player");
	//Debug.Log("Item: "+this.name + " belongs "+player.name);
	associatedInventory = player.Find("Inventory").GetComponent(Inventory); //finding the players inv.
	if(associatedInventory==null) Debug.LogError("cannot find inventory");

}

 function itemToPlayer(item:Transform){
	 Debug.Log("itemInterface.itemToPlayer(" + item + ")");
	 item.GetComponent(Item).PickUpItem();
 }

 function removeItem(item:Transform){
 	 Debug.Log("itemInterface.removeItem(" + item + ")");
		associatedInventory.RemoveItem(item);
}
 
 //find an item from the inventory (IT DOESN'T DROP IT).
function findKey(keyName : String) : Transform
{
	for(var i:Transform in associatedInventory.Contents) //Loop through the Items in the Inventory:
	{
		if(i.name == keyName) //When a match is found, return the Item.
		{
			Debug.Log("Key found");
			gameObject.SendMessage("findKeyCallback", i);
			return i;
			//No need to continue running through the loop since we found our item.
		}
	}
	
	return null;
}