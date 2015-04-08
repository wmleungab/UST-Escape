using UnityEngine;
using System.Collections;

public class masterTableScript : DialogInterface {
	public float maxTouchingDistance = 0.0f;
	public Sprite spriteChanged;
	public Sprite ascIITable;

	private SpriteRenderer tableSprite;
	private GameObject playerObj;
	// Use this for initialization

	void Start() {
		/*Transform temp = transform.Find("tableSprite");
		if (temp == null) Debug.LogError("table not found");
		tableSprite = temp.GetComponent<SpriteRenderer>();
		if (tableSprite==null) Debug.LogError("on Start: tableSprite not found");
		*/
		playerObj = GameObject.FindWithTag("Player");
		if (playerObj==null) Debug.LogError("Player Object not found");
		//allCanOpen = true;
	}
	void colliderOnClick(){
		if ((Vector3.Distance(playerObj.transform.position, transform.position) < maxTouchingDistance)){
		/*	//Change Sprite
			if (tableSprite==null) Debug.LogError("on update: tableSprite not found");
			Sprite temp = tableSprite.sprite;
			tableSprite.sprite = spriteChanged;
			spriteChanged = temp;
			
			//Send Message to parent Object
			allCanOpen = !(transform.parent.GetComponent<BigTableScript>().openTable(tableNo));
		*/
			DialogSystem.character[] c={DialogSystem.character.PLAYER};
			string[] s={"A table? What does it mean?"};
			showBigIcon(c,s,555,ascIITable);
		}
	}
	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("masterTableScript: Dialog with id " + id + "has finished with selection result "+selection);

	}
}
