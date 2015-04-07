using UnityEngine;
using System.Collections;

public class tableScript : MonoBehaviour {

	private SpriteRenderer tableSprite;
	private GameObject playerObj;
	
	public Sprite spriteChanged;
	public float maxTouchingDistance = 0.0f;
	public int tableNo = -1;
	
	public static bool allCanOpen = true;

	void Start() {
		Transform temp = transform.Find("tableSprite");
		if (temp == null) Debug.LogError("table not found");
		tableSprite = temp.GetComponent<SpriteRenderer>();
		if (tableSprite==null) Debug.LogError("on Start: tableSprite not found");
		
		playerObj = GameObject.FindWithTag("Player");
		if (playerObj==null) Debug.LogError("Player Object not found");
		allCanOpen = true;
	}
	
	void colliderOnClick(){
		if (allCanOpen && (Vector3.Distance(playerObj.transform.position, transform.position) < maxTouchingDistance)){
			//Change Sprite
			if (tableSprite==null) Debug.LogError("on update: tableSprite not found");
			Sprite temp = tableSprite.sprite;
			tableSprite.sprite = spriteChanged;
			spriteChanged = temp;
			
			//Send Message to parent Object
			allCanOpen = !(transform.parent.GetComponent<BigTableScript>().openTable(tableNo));
		}
	}
	public void flip(){
		if (tableSprite==null) Debug.LogError("on update: tableSprite not found");
		Sprite temp = tableSprite.sprite;
		tableSprite.sprite = spriteChanged;
		spriteChanged = temp;
		}
}
