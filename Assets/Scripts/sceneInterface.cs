using UnityEngine;
using System.Collections;

public class sceneInterface : MonoBehaviour {

	public int monsterNumber;
	public int[] monsterId;

	public void gotoBattle(){
			
		GameObject playerpObj = GameObject.FindWithTag("Player");
		DontDestroyOnLoad(playerpObj);
		GameObject dynamicObj = GameObject.Find("Dynamic");
		DontDestroyOnLoad(dynamicObj);
		GlobalValues.BattleData.numOfMonsters = monsterNumber;
		GlobalValues.BattleData.monsterID=monsterId;
		GlobalValues.BattleData.returnScene = Application.loadedLevelName;
		Application.LoadLevel("battle");
	}
	
	public void changeScene(string levelName){
		GameObject playerpObj = GameObject.FindWithTag("Player");
		DontDestroyOnLoad(playerpObj);
		GlobalValues.BattleData.numOfMonsters = monsterNumber;
		GlobalValues.BattleData.monsterID=monsterId;
		Application.LoadLevel("battle");
		GlobalValues.BattleData.returnScene = levelName;
	}
	
}
