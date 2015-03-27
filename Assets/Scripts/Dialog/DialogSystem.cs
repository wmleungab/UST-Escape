using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogSystem : MonoBehaviour
{
		bool dialogisOn = false;
		public GameObject dialogpf;
		GameObject dialogbg;
		bool waitForClick = false;
		bool isClick = false;
		static public bool toCreateDialog = false;
		static public string[]nameString;
		static public string[]dialogString;
	public Queue<string>cnameString = new Queue<string> ();
	public Queue<string> cdialogString = new Queue<string> ();
	
	private NavMeshAgent playerNavMesh;


	public Sprite DialogPrecident ;

		// Use this for initialization
		void Start ()
		{
			//test
			toCreateDialog = true;
			nameString = new string[]{"Precident","Precident"};
			dialogString = new string[]{"fuck\nfuck","you"};
			
			playerNavMesh = (NavMeshAgent)GameObject.FindWithTag("Player").GetComponent("NavMeshAgent");
		}
		
/*		void startDialog (string[] _nameString, string[] _dialogString) {
			toCreateDialog = true;
			nameString = _nameString;
			dialogString = _dialogString;

		}
*/	
		void startDialog() {
			toCreateDialog = true;
		}
	
		// Update is called once per frame
		void Update ()
		{
		if (!dialogisOn) {
			if (toCreateDialog) {
				CreateDialog ();
				toCreateDialog = false;
				dialogisOn = true;
			}
					
		}
		if (dialogisOn) {
			if (waitForClick)
				if (Input.GetMouseButtonDown (0)){
						isClick = true;
				}
			if (isClick) {
				if (cdialogString.Count > 0) {
					dialogbg.transform.GetChild (1).GetComponent<TextMesh> ().text = cnameString.Dequeue ();
					dialogbg.transform.GetChild (2).GetComponent<TextMesh> ().text = cdialogString.Dequeue ();

					//charaterpic
					if(dialogbg.transform.GetChild (1).GetComponent<TextMesh> ().text.Equals("Precident"))
						dialogbg.transform.GetChild (3).GetComponent<SpriteRenderer> ().sprite = DialogPrecident;

					waitForClick = true;
					isClick = false;
				}
				else{
					Destroy(dialogbg);
					dialogisOn=false;
					playerNavMesh.enabled = true;
				}
			}
			
		}

		}

		void CreateDialog ()
		{
			Debug.Log ("Creating Dialogue");
		
			foreach (string s in nameString)
				cnameString.Enqueue (s);
			foreach (string s in dialogString)
				cdialogString.Enqueue (s);

			Vector3 position = Camera.mainCamera.gameObject.transform.position;
			position.y -= 3;
			position.z -= 3.5f;
			dialogbg = Instantiate (dialogpf, position, Quaternion.Euler(90, 0, 0))as GameObject;
			dialogbg.transform.localScale -= new Vector3(0.2F, 0.2F, 0.8F);
			isClick = true;
				/*
		for (int i=0; i<num; i++) {
						if (isClick) {
								dialogbg = Instantiate (dialogpf, new Vector3(0,0,-30), Quaternion.identity)as GameObject;
								dialogbg.transform.GetChild (1).GetComponent<TextMesh> ().text = name;
								dialogbg.transform.GetChild (2).GetComponent<TextMesh> ().text = alldialog [0];
								waitForClick = true;
								isClick=false;
						}
				}*/
			dialogisOn = false;
			waitForClick = true;
			playerNavMesh.enabled = false;
		}
}
