using UnityEngine;
using System.Collections;

public static class SceneManger {

	static string currentScene = "";
	static string nextScene = "";

	static void changeScene(string sceneName){
		currentScene = sceneName;
		Application.LoadLevel (sceneName);
	}

	static void battleCallback(){

	}

}
