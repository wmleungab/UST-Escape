//Displaying the Inventory.

//Variables for dragging:
static var itemBeingDragged : Item; //This refers to the 'Item' script when dragging.
private var draggedItemPosition : Vector2; //Where on the screen we are dragging our Item.
private var draggedItemSize : Vector2;//The size of the item icon we are dragging.

//Variables for the window:
var windowSize:Vector2 = Vector2(375, 162.5); //The size of the Inventory window.
var useCustomPosition = true; //Do we want to use the customPosition variable to define where on the screen the Inventory window will appear?
var customPosition : Vector2 = Vector2 (0, 0); // The custom position of the Inventory window.
var itemIconSize : Vector2 = Vector2(60.0, 60.0); //The size of the item icons.

//Variables for updating the inventory
var updateListDelay = 9999;//This can be used to update the Inventory with a certain delay rather than updating it every time the OnGUI is called.
//This is only useful if you are expanding on the Inventory System cause by default Inventory has a system for only updating when needed (when an item is added or removed).
private var lastUpdate = 0.0; //Last time we updated the display.
private var UpdatedList : Transform[]; //The updated inventory array.

//More variables for the window:
static var displayInventory = false; //If inv is opened.
private var windowRect =Rect (200,200,108,130); //Keeping track of the Inventory window.
var invSkin:GUISkin; //This is where you can add a custom GUI skin or use the one included (InventorySkin) under the Resources folder.
var Offset : Vector2 = Vector2 (7, 12); //This will leave so many pixels between the edge of the window (x = horizontal and y = vertical).
var WindowOffset : Vector2 = Vector2 (0,18);
var spaceTemp : float;
var canBeDragged = true; //Can the Inventory window be dragged?

var onOffButton : KeyCode = KeyCode.I; //The button that turns the Inventory window on and off.
var isBattle = false;

//Keeping track of components.
private var associatedInventory : Inventory;
private var cSheetFound = false;
private var cSheet : Character;
private var startTime : float = 0;
private var buttonPressed = false;

//battle variables
var windowTitle = "Inventory";

@script AddComponentMenu ("Inventory/Inventory Display")
@script RequireComponent(Inventory)

//Store components and adjust the window position.
function Awake()
{
	windowSize = Vector2(Screen.width/4, Screen.height);
	itemIconSize = Vector2(windowSize.x/3, windowSize.x/3);
	spaceTemp = Screen.width/20;

		if (useCustomPosition == false)
	{
		windowRect=Rect(Screen.width-windowSize.x-70,Screen.height-windowSize.y-70,windowSize.x,windowSize.y);
	}
	else
	{
		windowRect = Rect (customPosition.x, customPosition.y, windowSize.x, windowSize.y);
	}
	Debug.Log ("invDisplay: "+this.transform.parent.name);
	associatedInventory=this.GetComponent(Inventory);//keepin track of the inventory script
	if (GetComponent(Character) != null)
	{
		cSheetFound = true;
		cSheet = GetComponent(Character);
	}
	else
	{
		Debug.Log ("No Character script was found on this object. Attaching one allows for functionality such as equipping items.");
		cSheetFound = false;
	}

}

function toBattleMode(){
	Debug.Log("Inventory change to battle mode");
	isBattle = true;
	windowTitle = "";
	WindowOffset.y = spaceTemp/2;
	windowSize = Vector2(Screen.width - spaceTemp * 2, Screen.height / 4 );
	windowRect=Rect(spaceTemp,Screen.height-windowSize.y-spaceTemp/2,windowSize.x,windowSize.y);
	canBeDragged = false;

	displayInventory = true;
	
	gameObject.SendMessage ("ChangedState", true, SendMessageOptions.DontRequireReceiver);
	gameObject.SendMessage("PauseGame", true, SendMessageOptions.DontRequireReceiver); //PauseGame/DisableMouse/HideMouse
}

function toMapMode(){
	Debug.Log("Inventory change to map mode");
	isBattle = false;
	windowTitle = "Inventory";
	WindowOffset.y = spaceTemp/2;
	windowSize = Vector2(Screen.width/4, Screen.height);
	windowRect=Rect(0,0,windowSize.x,windowSize.y);
	canBeDragged = true;

	displayInventory = false;
	
	gameObject.SendMessage ("ChangedState", false, SendMessageOptions.DontRequireReceiver);
	gameObject.SendMessage("PauseGame", false, SendMessageOptions.DontRequireReceiver); //StopPauseGame/EnableMouse/ShowMouse
}

//Update the inv list
function UpdateInventoryList()
{
	UpdatedList = associatedInventory.Contents;
	//Debug.Log("Inventory Updated");
}

function Update()
{
	if(Input.GetKeyDown(KeyCode.Escape)) //Pressed escape
	{
		ClearDraggedItem(); //Get rid of the dragged item.
	}
	if(Input.GetMouseButtonDown(1)) //Pressed right mouse
	{
		ClearDraggedItem(); //Get rid of the dragged item.
	}
	
	//Turn the Inventory on and off and handle audio + pausing the game.
	if(Input.GetKeyDown(onOffButton) || Input.GetKeyDown(KeyCode.Menu))
	{
		if (displayInventory)
		{
			displayInventory = false;
			
			gameObject.SendMessage ("ChangedState", false, SendMessageOptions.DontRequireReceiver);
			gameObject.SendMessage("PauseGame", false, SendMessageOptions.DontRequireReceiver); //StopPauseGame/EnableMouse/ShowMouse
		}
		else
		{
			displayInventory = true;
			
			gameObject.SendMessage ("ChangedState", true, SendMessageOptions.DontRequireReceiver);
			gameObject.SendMessage("PauseGame", true, SendMessageOptions.DontRequireReceiver); //PauseGame/DisableMouse/HideMouse
		}
	}
	
	
	if(Input.GetMouseButtonDown(0)) //Pressed left mouse
	{
		//Debug.Log("mouse down update");
		startTime = Time.time;
		buttonPressed = true;
	}
	if(buttonPressed){
		if(Time.time - startTime > 0.2f){
			//Making the dragged icon update its position
			if(itemBeingDragged!=null)
			{
				//Give it a 15 pixel space from the mouse pointer to allow the Player to click stuff and not hit the button we are dragging.
				draggedItemPosition.y=Screen.height-Input.mousePosition.y+15;
				draggedItemPosition.x=Input.mousePosition.x+15;
			}
		}
	}
    if ( Input.GetMouseButtonUp(0) )
   {
       buttonPressed = false ;
	   
	   if(itemBeingDragged!=null){
		   if ( (Time.time - startTime) < 0.2 )
		   {
				// short click effect
				//Debug.Log("short click");
				if(itemBeingDragged.isEquipment==false)
					itemBeingDragged.GetComponent(ItemEffect).UseEffect(); //It's not equipment so we just use the effect.
		   }
		   else {
/* 			   if(!windowRect.Contains(Input.mousePosition)){
				   Debug.Log("outside window");
				}
 */		   }
		   ClearDraggedItem(); //Stop dragging
	   }
   }
	
	
	//Updating the list by delay
	if(Time.time>lastUpdate){
		lastUpdate=Time.time+updateListDelay;
		UpdateInventoryList();
	}
}

//Drawing the Inventory window
function OnGUI()
{
	if(isBattle) {
		var newColor = new Color(1,1,1,0.0f);
		GUI.color = newColor;
	}

	GUI.skin = invSkin; //Use the invSkin
	GUI.skin.window.fontSize = 30*Screen.width/1024;
	GUI.skin.GetStyle("Stacks").fontSize = 40*Screen.width/1024;
	if(itemBeingDragged != null) //If we are dragging an Item, draw the button on top:
	{
		GUI.depth = 1;
		GUI.Button(Rect(draggedItemPosition.x,draggedItemPosition.y,draggedItemSize.x,draggedItemSize.y),itemBeingDragged.itemIcon);
		GUI.depth = 10;
	}
	
	//If the inventory is opened up we create the Inventory window:
	if(displayInventory)
	{
		windowRect = GUI.Window (0, windowRect, DisplayInventoryWindow, windowTitle);
	}
}

//Setting up the Inventory window
function DisplayInventoryWindow(windowID:int)
{

	if (canBeDragged == true)
	{
		GUI.DragWindow (Rect (0,0, 10000, 30));  //the window to be able to be dragged
	}
	
	var currentX = WindowOffset.x + Offset.x; //Where to put the first items.
	var currentY = WindowOffset.y + Offset.y; //Im setting the start y position to 18 to give room for the title bar on the window.
	
	for(var i:Transform in UpdatedList) //Start a loop for whats in our list.
	{
		var item=i.GetComponent(Item);
			//if(GUI.RepeatButton(Rect(currentX,currentY,itemIconSize.x,itemIconSize.y),item.itemIcon))
			if(Event.current.isMouse && Rect(currentX,currentY,itemIconSize.x,itemIconSize.y).Contains(Event.current.mousePosition))
			{
				var dragitem=true; //Incase we stop dragging an item we dont want to redrag a new one.
/* 				if(itemBeingDragged == item) //We clicked the item, then clicked it again
				{
					associatedInventory.DropItem(item);
					if (cSheetFound)
					{
						GetComponent(Character).UseItem(item,0,true); //We use the item.
					}
					ClearDraggedItem(); //Stop dragging
					dragitem = false; //Dont redrag
				}
 */				if (Event.current.button == 0) //Check to see if it was a left click
				{
					if(itemBeingDragged==null)
					{
							Debug.Log("drag item set");
							itemBeingDragged = item; //Set the item being dragged.
							draggedItemSize=itemIconSize; //We set the dragged icon size to our item button size.
							//We set the position:
							draggedItemPosition.y=Screen.height-Input.mousePosition.y-15;
							draggedItemPosition.x=Input.mousePosition.x+15;
					}
				}
				else if (Event.current.button == 1) //If it was a right click we want to drop the item.
				{
					associatedInventory.DropItem(item);
				}
			}
			GUI.Button(Rect(currentX,currentY,itemIconSize.x,itemIconSize.y),item.itemIcon);
		

		
		if(item.stackable) //If the item can be stacked:
		{
			GUI.Label(Rect(currentX, currentY, itemIconSize.x, itemIconSize.y), "" + item.stack, "Stacks"); //Showing the number (if stacked).
		}
		
		currentX += itemIconSize.x + spaceTemp/2;
		if(currentX + itemIconSize.x + Offset.x > windowSize.x) //Make new row
		{
			currentX=Offset.x; //Move it back to its startpoint wich is 0 + offsetX.
			currentY+=itemIconSize.y; //Move it down a row.
			if(currentY + itemIconSize.y + Offset.y > windowSize.y) //If there are no more room for rows we exit the loop.
			{
				return;
			}
		}
	}
}

//If we are dragging an item, we will clear it.
function ClearDraggedItem()
{
	Debug.Log("Drag item clear");
	itemBeingDragged=null;
}