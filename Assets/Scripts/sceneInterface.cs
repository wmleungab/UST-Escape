using UnityEngine;
using System.Collections;

public class sceneInterface : MonoBehaviour {

	public bool withBattle = true;
	public int battleBackgroundID=1;
	public int monsterNumber;
	public int[] monsterId;
	static Hashtable dynamicHT;

	void Start() {
		if(dynamicHT==null) dynamicHT = new Hashtable ();
		}

	public void gotoBattle(){
			
		GameObject playerpObj = GameObject.FindWithTag("Player");
		DontDestroyOnLoad(playerpObj);
		GameObject dynamicObj = GameObject.Find("Dynamic");
		DontDestroyOnLoad(dynamicObj);

		GlobalValues.BattleData.returnScene = Application.loadedLevelName;
		loadBattleScene ();
	}
	
	public void changeScene(string levelName){
		GameObject playerpObj = GameObject.FindWithTag("Player");
		DontDestroyOnLoad(playerpObj);
		if (!dynamicHT.ContainsKey(Application.loadedLevelName)) {
						GameObject dynamicObj = GameObject.Find ("Dynamic");
						if (dynamicObj) {
								DontDestroyOnLoad (dynamicObj);
								//dynamicObj.name += Application.loadedLevelName;
								dynamicHT.Add (Application.loadedLevelName, dynamicObj);
								dynamicObj.SetActive (false);
						}
				} else {
					((GameObject)dynamicHT [Application.loadedLevelName]).SetActive(false);
				}

		if (dynamicHT.ContainsKey(levelName)) {
			GameObject nextLevelDy = (GameObject)dynamicHT[levelName];//GameObject.Find (("Dynamic" + levelName));
			//nextLevelDy.name = "Dynamic";
			nextLevelDy.SetActive(true);
		}

		if (withBattle) {
			loadBattleScene();
			GlobalValues.BattleData.returnScene = levelName;
		} else {
			Application.LoadLevel (levelName);
		}

	}

	void loadBattleScene(){
		GlobalValues.BattleData.numOfMonsters = monsterNumber;
		GlobalValues.BattleData.monsterID=monsterId;
		GlobalValues.BattleData.battleBackgroundID=battleBackgroundID;
		Application.LoadLevel ("battle");

	}
	
}
