using UnityEngine;
using System.Collections;

public class SaveLoadSystem : MonoBehaviour
{

		//Here is a private reference only this class can access
		private static SaveLoadSystem _instance;
		public string[] playerInventoryList = new string[20];
		public string[] mapItemList = new string[20];
		public SceneType currentSceneType = SceneType.LAB;
		public bool[] labSceneState = new bool[3];

		public enum LabSceneState
		{
				BEGANDIALOG=0,
				DOORIVD,
				PRINTERIVD
		}

		public enum SceneType
		{
				LAB=0,
				ATRIUM,
				LG2,
				SUNDIAL,
				UNDERSUN
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
						if (labSceneState [(int)LabSceneState.BEGANDIALOG])
								PlayerPrefs.SetInt ("LabSceneStateBEGANDIALOG", 1);
						else
								PlayerPrefs.SetInt ("LabSceneStateBEGANDIALOG", 0);
						if (labSceneState [(int)LabSceneState.DOORIVD])
								PlayerPrefs.SetInt ("LabSceneStateDOORIVD", 1);
						else
								PlayerPrefs.SetInt ("LabSceneStateDOORIVD", 0);
						if (labSceneState [(int)LabSceneState.PRINTERIVD])
								PlayerPrefs.SetInt ("LabSceneStatePRINTERIVD", 1);
						else
								PlayerPrefs.SetInt ("LabSceneStatePRINTERIVD", 0);
				}
		}

		//When you call this function, it returns you the last scene num where you saved
		public SceneType load ()
		{
				currentSceneType = (SceneType)PlayerPrefs.GetInt ("sceneNum");
				if (currentSceneType == SceneType.LAB) {
						if (PlayerPrefs.GetInt ("LabSceneStateBEGANDIALOG") == 1)
								labSceneState [(int)LabSceneState.BEGANDIALOG] = true;
						else if (PlayerPrefs.GetInt ("LabSceneStateBEGANDIALOG") == 0)
								labSceneState [(int)LabSceneState.BEGANDIALOG] = false;
						if (PlayerPrefs.GetInt ("LabSceneStateDOORIVD") == 1)
								labSceneState [(int)LabSceneState.DOORIVD] = true;
						else if (PlayerPrefs.GetInt ("LabSceneStateDOORIVD") == 0)
								labSceneState [(int)LabSceneState.DOORIVD] = false;
						if (PlayerPrefs.GetInt ("LabSceneStatePRINTERIVD") == 1)
								labSceneState [(int)LabSceneState.PRINTERIVD] = true;
						else if (PlayerPrefs.GetInt ("LabSceneStatePRINTERIVD") == 0)
								labSceneState [(int)LabSceneState.PRINTERIVD] = false;

						
				} else if (currentSceneType == SceneType.ATRIUM) {

				} else if (currentSceneType == SceneType.LG2) {

				} else if (currentSceneType == SceneType.SUNDIAL) {

				} else if (currentSceneType == SceneType.UNDERSUN) {
				}
				return currentSceneType;
		}

		void Start ()
		{
				Debug.Log ("SaveLoadSystem: Loading the previous save");
				
				load ();
				Debug.Log (currentSceneType);
				Debug.Log ("Loading: " + labSceneState [0]);
				Debug.Log ("Loading: " + labSceneState [1]);
				Debug.Log ("Loading: " + labSceneState [2]);
		}

		void Update ()
		{
				if (setCurrentSceneType ()) {
						if (Input.GetKeyDown (KeyCode.Escape)) {
								Debug.Log ("SaveLoadSystem:Saving the game");
								save ();
						} else if (Input.GetKeyDown (KeyCode.E)) {
								Debug.Log ("SaveLoadSystem:Saving the game");
								save ();
						} else if (Input.GetKeyDown (KeyCode.L)) {
								Debug.Log ("SaveLoadSystem: Loading the previous save");

								load ();
								Debug.Log ("Loading: " + currentSceneType);
								Debug.Log ("Loading: " + labSceneState [0]);
								Debug.Log ("Loading: " + labSceneState [1]);
								Debug.Log ("Loading: " + labSceneState [2]);
						} else if (Input.GetKeyDown (KeyCode.R)) {
								Debug.Log ("SaveLoadSystem:Reseting the save");
								resetSave ();
						}
				}
				
		}

		bool setCurrentSceneType ()
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
				} else
						return false;

		}

		void resetSave ()
		{
				playerInventoryList = new string[20];
				mapItemList = new string[20];
				currentSceneType = SceneType.LAB;
				labSceneState = new bool[3];
				save ();
		}
		
}
