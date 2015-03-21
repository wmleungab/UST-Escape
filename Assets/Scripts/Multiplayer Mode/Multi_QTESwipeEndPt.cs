using UnityEngine;
using System.Collections;

public class Multi_QTESwipeEndPt : MonoBehaviour {

	public Multi_QTESwipe parent;
	
	// Use this for initialization
	void Start () {
		do {
			parent = GameObject.Find ("QTESwipe(Clone)").GetComponent<Multi_QTESwipe> ();
				} while(parent==null);
	}
	
	
	void  OnTriggerEnter2D (Collider2D col)
	{
		if (parent != null)parent.collisionEndPt (col);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
