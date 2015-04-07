using UnityEngine;
using System.Collections;

public class SaveLoadSystem : MonoBehaviour
{

	//Here is a private reference only this class can access
	private static SaveLoadSystem _instance;
	public string[] playerInventoryList = new string[20];
	public string[] mapItemList = new string[20];
	public SceneType currentSceneType = SceneType.MENU;
	public bool[] labSceneStateArr = new bool[4];
	SceneType lastSceneType = SceneType.MENU;
	public enum LabSceneState
	{
		BEGANDIALOG=0,
		DOORIVD,
		PRINTERIVD,
		PUZZLESOLVED
	}

	public enum SceneType
	{
		MENU=0,
		LAB,
		ATRIUM,
		LG2,
		SUNDIAL,
		UNDERSUN1,
		UNDERSUN2
	}

	public static SaveLoadSystem getInstance ()
	{
		if (_instance == null) {
			_instance = GameObject.FindObjectOfType<SaveLoadSystem> ();
			
			//Tell unity not to destroy this object when loading a new scene!
			DontDestroyOnLoad (_instance.gameObject);
		}
		
		return _instance;
	}

	void Awake ()
	{
		if (_instance == null) {
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad (this);
		} else {
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if (this != _instance)
				Destroy (this.gameObject);
		}
	}
	//Please set the value of the bool array before calling this function
		
	public void save ()
	{
		PlayerPrefs.SetInt ("sceneNum", (int)currentSceneType);
			
		if (currentSceneType == SceneType.LAB) {
			if (labSceneStateArr [(int)LabSceneState.BEGANDIALOG])
				PlayerPrefs.SetInt ("LabSceneStateBEGANDIALOG", 1);
			else
				PlayerPrefs.SetInt ("LabSceneStateBEGANDIALOG", 0);
			if (labSceneStateArr [(int)LabSceneState.DOORIVD])
				PlayerPrefs.SetInt ("LabSceneStateDOORIVD", 1);
			else
				PlayerPrefs.SetInt ("LabSceneStateDOORIVD", 0);
			if (labSceneStateArr [(int)LabSceneState.PRINTERIVD])
				PlayerPrefs.SetInt ("LabSceneStatePRINTERIVD", 1);
			else
				PlayerPrefs.SetInt ("LabSceneStatePRINTERIVD", 0);
			if (labSceneStateArr [(int)LabSceneState.PUZZLESOLVED])
				PlayerPrefs.SetInt ("LabSceneStatePUZZLESOLVED", 1);
			else
				PlayerPrefs.SetInt ("LabSceneStatePUZZLESOLVED", 0);
		}
	}

	//When you call this function, it returns you the last scene num where you saved
	public SceneType load ()
	{
		Debug.Log ("SaveLoadSystem: Loading the previous save");
		currentSceneType = (SceneType)PlayerPrefs.GetInt ("sceneNum");
		if (currentSceneType == SceneType.LAB) {



			if (PlayerPrefs.GetInt ("LabSceneStateBEGANDIALOG") == 1)
				labSceneStateArr [(int)LabSceneState.BEGANDIALOG] = true;
			else if (PlayerPrefs.GetInt ("LabSceneStateBEGANDIALOG") == 0)
				labSceneStateArr [(int)LabSceneState.BEGANDIALOG] = false;
			if (PlayerPrefs.GetInt ("LabSceneStateDOORIVD") == 1)
				labSceneStateArr [(int)LabSceneState.DOORIVD] = true;
			else if (PlayerPrefs.GetInt ("LabSceneStateDOORIVD") == 0)
				labSceneStateArr [(int)LabSceneState.DOORIVD] = false;
			if (PlayerPrefs.GetInt ("LabSceneStatePRINTERIVD") == 1)
				labSceneStateArr [(int)LabSceneState.PRINTERIVD] = true;
			else if (PlayerPrefs.GetInt ("LabSceneStatePRINTERIVD") == 0)
				labSceneStateArr [(int)LabSceneState.PRINTERIVD] = false;
			if (PlayerPrefs.GetInt ("LabSceneStatePUZZLESOLVED") == 1)
				labSceneStateArr [(int)LabSceneState.PUZZLESOLVED] = true;
			else if (PlayerPrefs.GetInt ("LabSceneStatePUZZLESOLVED") == 0)
				labSceneStateArr [(int)LabSceneState.PUZZLESOLVED] = false;

						
		} else if (currentSceneType == SceneType.ATRIUM) {

		} else if (currentSceneType == SceneType.LG2) {

		} else if (currentSceneType == SceneType.SUNDIAL) {

		} else if (currentSceneType == SceneType.UNDERSUN1) {
		} else if (currentSceneType == SceneType.UNDERSUN2) {
		}
		return currentSceneType;
	}

	public SceneType loadSceneTypeOnly ()
	{
		currentSceneType = (SceneType)PlayerPrefs.GetInt ("sceneNum");
		return currentSceneType;
	}

	void Start ()
	{
			
	}

	void Update ()
	{
		/*	if (isSavableScenesTransaction()) {
						if (!GamePause.isPause ()) {
								lastSceneType = currentSceneType;
								save ();
						
						}
				}
			*/
		if (isMenu2OtherSceneTransaction ()) {
			Debug.Log ("SaveLoadSystem: A scene transaction from MainMenu to some scene was detected.");
			lastSceneType = currentSceneType;
			load ();
		}

		if (setnCheckCurrentSceneType () && !GamePause.isPause ()) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Debug.Log ("SaveLoadSystem:Saving the game");
				save ();
				lastSceneType = SceneType.MENU;
				currentSceneType = SceneType.MENU;
				Application.LoadLevel ("mainmenu"); 
			} else if (Input.GetKeyDown (KeyCode.E)) {
				Debug.Log ("SaveLoadSystem:Saving the game");
				save ();
				lastSceneType = SceneType.MENU;
				currentSceneType = SceneType.MENU;
				Application.LoadLevel ("mainmenu"); 
			} 
		}
				
	}

	bool isSavableScenesTransaction ()
	{
		if (lastSceneType != currentSceneType) {
			if (lastSceneType != SceneType.MENU)
				return true;
		}
		return false;
	}

	bool isMenu2OtherSceneTransaction ()
	{
		if (lastSceneType != currentSceneType) {
			if (lastSceneType == SceneType.MENU)
				return true;
		}
		return false;
	}


	//return true if the current scene is a savable scene
	bool setnCheckCurrentSceneType ()
	{
		string name = Application.loadedLevelName;
		if (name == "lab_stage") {
			currentSceneType = SceneType.LAB;
			return true;
		} else if (name == "atrium_stage") {
			currentSceneType = SceneType.ATRIUM;
			return true;
		} else if (name == "LG2_stage") {
			currentSceneType = SceneType.LG2;
			return true;
		} else if (name == "sundial") {
			currentSceneType = SceneType.SUNDIAL;
			return true;
		} 
		return false;

	}

	public void resetSave ()
	{
		Debug.Log ("SaveLoadSystem:Reseting the save");
		playerInventoryList = new string[20];
		mapItemList = new string[20];
		currentSceneType = SceneType.LAB;
		labSceneStateArr = new bool[4];
		save ();
	}
		
}
