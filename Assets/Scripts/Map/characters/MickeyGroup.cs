using UnityEngine;
using System.Collections;

public class MickeyGroup : MonoBehaviour {

	
	void groupBattle(){
		GetComponent<sceneInterface>().gotoBattle();
		Destroy(this.gameObject);
	}
	
}
