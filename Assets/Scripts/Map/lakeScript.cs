using UnityEngine;
using System.Collections;

public class lakeScript : MonoBehaviour {

	 void OnTriggerEnter(Collider other)
	 {
		 Debug.Log ("Lake Get Something!!");
		if(other.gameObject.tag=="Item"){
			Debug.Log(other.name);
			Destroy(gameObject);    
		}
	 }
	
}
