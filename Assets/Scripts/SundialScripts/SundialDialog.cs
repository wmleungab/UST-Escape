using UnityEngine;
using System.Collections;

public class SundialDialog : MonoBehaviour
{
		public GameObject name;
		public GameObject dialogue;
		public GameObject speakerLeft;
		public GameObject speakerRight;
		public GameObject QTETriangle;
		int conserveration_count = 1;
		string main1 = "... Unlock the sundial? Sundial was lock? ";
		string main2 = "Wait a minute, it was rotated! It is not facing its original direction!\nBut what does it mean?";
		string main3 = "Maybe, I just try the magic that Voldemort taught me.";
		string main4 = "'Mobiliarbus'...! 'Mobiliarbus!'";
		string main5 = "It starts rotating!";
		string main6 = "It works! A stair? What is the stair going to?";

		// Use this for initialization
		void Start ()
		{
		GamePause.pauseGame ();
				name.GetComponent<TextMesh> ().text = "You";
				dialogue.GetComponent<TextMesh> ().text = main1;
			
		}

		void OnMouseUp ()
		{

				switch (conserveration_count) {
				case 1:
						{

								++conserveration_count;
								OnMouseUp ();
								break;}
				case 2:
						{
								dialogue.GetComponent<TextMesh> ().text = main2;
								++conserveration_count;
								break;}
				case 3:
						{
								
								dialogue.GetComponent<TextMesh> ().text = main3;
								++conserveration_count;
								break;}
				case 4:
						{
								QTETriangle.SetActive (true);
								dialogue.GetComponent<TextMesh> ().text = main4;

								break;}
				case 5:
						{
								dialogue.GetComponent<TextMesh> ().text = main5;
								++conserveration_count;
								break;}
				case 6:
						{
								dialogue.GetComponent<TextMesh> ().text = main6;
								++conserveration_count;
								break;}
				case 7:
						{
			GamePause.continueGame ();
			Application.LoadLevel("SecretChamberBefore");
								dialogue.GetComponent<TextMesh> ().text = "Change sense";
								break;}
				}
				
		}


		// Update is called once per frame
		void Update ()
		{
				if (QTETriangle == null && conserveration_count == 4) {
						++conserveration_count;
						OnMouseUp ();
				}
						
		}


}
