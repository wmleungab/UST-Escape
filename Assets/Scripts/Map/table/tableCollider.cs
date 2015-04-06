using UnityEngine;
using System.Collections;

public class tableCollider : MonoBehaviour {

	void OnMouseDown(){
		if(	!GamePause.isPause() )
		Debug.Log("collider clicked");
			transform.parent.gameObject.SendMessage("colliderOnClick");
		
	}

	void OnTriggerStay(Collider Other){
		Debug.Log("collider receive");
			transform.parent.gameObject.SendMessage("colliderTriggerStay", Other);
	}
}
