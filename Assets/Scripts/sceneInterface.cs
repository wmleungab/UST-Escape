using UnityEngine;
using System.Collections;

public class sceneInterface : MonoBehaviour {

	public void gotoBattle(){
			
			GameObject dynamicObj = GameObject.Find("Dynamic");
			DontDestroyOnLoad(dynamicObj);
			Application.LoadLevel("battle");
			GlobalValues.BattleData.returnScene = Application.loadedLevelName;
	}
	
	public void changeScene(string levelName){
			Application.LoadLevel("battle");
			GlobalValues.BattleData.returnScene = levelName;
	}
	
}
