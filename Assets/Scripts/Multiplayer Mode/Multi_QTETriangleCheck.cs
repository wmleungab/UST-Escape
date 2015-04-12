using UnityEngine;
using System.Collections;

public class Multi_QTETriangleCheck : MonoBehaviour {
	
	public Multi_QTETriangle parent;
	
	void Start () {
		if (Application.loadedLevelName != "sundial")
						
		do {
			parent = GameObject.Find ("QTETri(Clone)").GetComponent<Multi_QTETriangle> ();
		} while(parent==null);
	}
	
	
	void  OnTriggerEnter2D (Collider2D col)
	{
		if (parent != null)parent.checkptTriggerEnter (col,gameObject.name);

	}
	
	void  OnTriggerStay2D (Collider2D col)
	{
		//if (parent != null)parent.baseTriggerStay (col);
	}
	void  OnTriggerExit2D (Collider2D col)
	{
		//if (parent != null)parent.baseTriggerExit (col);
	}
	// Update is called once per frame
	void Update () {
		
	}
}