using UnityEngine;
using System.Collections;

public class BridgeEvent : EventScript {

	GameObject gapChild;
	GameObject woodChild;
	
	public override void Start(){
		base.Start ();
		gapChild = transform.Find("Gap").gameObject;
		woodChild = transform.Find("woodplate").gameObject;
		
		woodChild.SetActive(false);
	}

	override public void startEvent (Transform keyObj) {
		
		woodChild.SetActive(true);
		gapChild.SetActive(false);
		
	}

}
