using UnityEngine;
using System.Collections;

public static class GamePause{

	private static int pauseCount = 0;

	public static bool isPause(){
		return (pauseCount>0);
	}
	
	public static void pauseGame(){
		Debug.Log("pauseGame");
		pauseCount++;
	}
	
	public static void continueGame(){
		if(pauseCount>0)
			pauseCount--;
		Debug.Log("continueGame " + pauseCount);
	}
	
}
