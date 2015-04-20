using UnityEngine;
using System.Collections;

public class Multi_QTESlidePt : MonoBehaviour {

	public Multi_QTESlide parent;
	
	// Use this for initialization
	void Start () {
		do {
			parent = GameObject.Find ("QTESlide").GetComponent<Multi_QTESlide> ();
		} while(parent==null);
	}
	
	
	void  OnTriggerEnter2D (Collider2D col)
	{
		if (parent != null)parent.PtTriggerEnter (col);
	}
	
	void  OnTriggerStay2D (Collider2D col)
	{
		if (parent != null)parent.PtTriggerStay (col);
	}
	void  OnTriggerExit2D (Collider2D col)
	{
		if (parent != null)parent.PtTriggerExit (col);
	}

}
