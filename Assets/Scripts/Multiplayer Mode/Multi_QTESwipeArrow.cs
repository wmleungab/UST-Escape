using UnityEngine;
using System.Collections;

public class Multi_QTESwipeArrow : MonoBehaviour {

	public Multi_QTESwipe parent;

	// Use this for initialization
	void Start () {
		do {
			parent = GameObject.Find ("QTESwipe(Clone)").GetComponent<Multi_QTESwipe> ();
		} while(parent==null);
	}


	void  OnTriggerEnter2D (Collider2D col)
	{
		if (parent != null)parent.triggerEnterArrow (col);
	}

	void  OnTriggerStay2D (Collider2D col)
	{
		if (parent != null)parent.triggerStayArrow (col);
	}
	void  OnTriggerExit2D (Collider2D col)
	{
		if (parent != null)parent.triggerExitArrow (col);
	}

}
