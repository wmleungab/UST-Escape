#pragma strict
private var oldInv : Inventory;
private var hasClone = false;
function Start () {
	var temp = GameObject.Find("VictimClone");
	if(temp) {
		Debug.Log("player have clone");
		hasClone = true;
		oldInv = temp.Find("Inventory").GetComponent(Inventory);//keepin track of the inventory script
		if(oldInv == null)
			Debug.LogError("can't get Inventory Component of clone object");
		else
			copyInv2Victim(oldInv);
		Destroy(temp);
	}
	else{
		Debug.Log("player no clone");
	}
	this.enabled = false;
}

function copyInv2Victim(oldInv:Inventory) {
	var UpdatedList : Transform[]; //The updated inventory array.
	UpdatedList = oldInv.Contents;
	
	var player = GameObject.Find("Victim");
	var playerInv : Inventory;
	playerInv = player.Find("Inventory").GetComponent(Inventory);

	for(var i:Transform in UpdatedList) //Start a loop for whats in our list.
	{
		playerInv.AddItem(i);
		i.parent = playerInv.itemHolderObject;
	}
	for(var i:Transform in UpdatedList) //Start a loop for whats in our list.
	{
		oldInv.RemoveItem(i);
	}
}

function Update () {


}