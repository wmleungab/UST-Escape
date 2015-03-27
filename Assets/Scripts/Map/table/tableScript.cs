using UnityEngine;
using System.Collections;

public class tableScript : MonoBehaviour {

	private SpriteRenderer tableSprite;
	public Sprite spriteChanged;

	void Start() {
		Transform temp = transform.Find("tableSprite");
		if (temp == null) Debug.LogError("table not found");
		tableSprite = temp.GetComponent<SpriteRenderer>();
		if (tableSprite==null) Debug.LogError("on Start: tableSprite not found");
	}
	
	void colliderOnClick(){
		if (tableSprite==null) Debug.LogError("on update: tableSprite not found");
		Sprite temp = tableSprite.sprite;
		tableSprite.sprite = spriteChanged;
		spriteChanged = temp;
	}

}
