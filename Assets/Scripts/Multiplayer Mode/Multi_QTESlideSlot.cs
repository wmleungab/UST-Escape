using UnityEngine;
using System.Collections;

public class Multi_QTESlideSlot : MonoBehaviour {

	public Multi_QTESlide parent;
	
	// Use this for initialization
	void Start () {
		do {
			parent = GameObject.Find ("QTESlide").GetComponent<Multi_QTESlide> ();
		} while(parent==null);
	}
	
	
	void  OnTriggerEnter2D (Collider2D col)
	{
		if (parent != null)parent.slotTriggerEnter (col);
	}
	
	void  OnTriggerStay2D (Collider2D col)
	{
		if (parent != null)parent.slotTriggerStay (col);
	}
	void  OnTriggerExit2D (Collider2D col)
	{
		if (parent != null)parent.slotTriggerExit (col);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
