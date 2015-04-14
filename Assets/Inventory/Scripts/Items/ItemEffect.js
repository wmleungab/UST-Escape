#pragma strict

//This script allows you to insert code when the Item is used (clicked on in the inventory).

var deleteOnUse = true;

private var playersInv : Inventory;
private var item : Item;

@script AddComponentMenu ("Inventory/Items/Item Effect")
@script RequireComponent(Item)

//This is where we find the components we need
function Awake ()
{
	playersInv = FindObjectOfType(Inventory); //finding the players inv.
	if (playersInv == null)
	{
		Debug.LogWarning("No 'Inventory' found in game. The Item " + transform.name + " has been disabled for pickup (canGet = false).");
	}
	item = GetComponent(Item);
}

//This is called when the object should be used.
function UseEffect () 
{
	Debug.LogWarning("<INSERT CUSTOM ACTION HERE>"); //INSERT CUSTOM CODE HERE!
	if(this.name=="redapple")
GameObject.Find("Player").GetComponent("HealthBar").SendMessage("Addhp",40);
	else if(this.name=="greenapple");
else if(this.name=="beautifulKey");
else if(this.name=="midterm"){
var i:int;
for(i=0;i<GameObject.Find("Enemies").transform.childCount;i++)
if(GameObject.Find("Enemies").transform.GetChild(i).name.Contains("dembeater")){
GameObject.Find("Enemies").transform.GetChild(i).gameObject.GetComponent("HealthBar").SendMessage("decreaseHP",11);
SendMessage("findKey","midterm");
}
}
else if(this.name=="salad"){
for(i=0;i<GameObject.Find("Enemies").transform.childCount;i++)
if(GameObject.Find("Enemies").transform.GetChild(i).name.Contains("dimJACK")){
GameObject.Find("Enemies").transform.GetChild(i).gameObject.GetComponent("HealthBar").SendMessage("decreaseHP",15);
SendMessage("findKey","salad");
}
}
else if(this.name=="key");
else if(this.name=="nose"){
for(i=0;i<GameObject.Find("Enemies").transform.childCount;i++)
if(GameObject.Find("Enemies").transform.GetChild(i).name.Contains("Void")){
GameObject.Find("Enemies").transform.GetChild(i).gameObject.GetComponent("HealthBar").SendMessage("decreaseHP",12);
SendMessage("findKey","nose");
}
}
else if(this.name=="knife");
else if(this.name=="mace");
	//Play a sound
	playersInv.gameObject.SendMessage("PlayDropItemSound", SendMessageOptions.DontRequireReceiver);
	
	//This will delete the item on use or remove 1 from the stack (if stackable).
	if (deleteOnUse == true)
	{
		DeleteUsedItem();
	}
}

function findKeyCallback(item:Transform){
SendMessage("removeItem",item);
}

//This takes care of deletion
function DeleteUsedItem()
{
	if (item.stack == 1) //Remove item
	{
		playersInv.RemoveItem(this.gameObject.transform);
	}
	else //Remove from stack
	{
		item.stack -= 1;
	}
	Debug.Log(item.name + " has been deleted on use");
}