using UnityEngine;
using System.Collections;

public class tableCollider : MonoBehaviour {

	void OnMouseDown(){
		
		//Debug.Log("collider clicked");
		transform.parent.gameObject.SendMessage("colliderOnClick");
		
	}

}
