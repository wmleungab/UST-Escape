using UnityEngine;
using System.Collections;

public class sceneInterface : MonoBehaviour {

	void changeScene(){
			
			Application.LoadLevel("battle");
			GlobalValues.BattleData.returnScene = Application.loadedLevelName;

	}
	
}
