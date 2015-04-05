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
