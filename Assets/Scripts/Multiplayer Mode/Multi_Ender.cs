using UnityEngine;
using System.Collections;

public class Multi_Ender : MonoBehaviour
{
	public AudioClip winAudio;
	public AudioClip loseAudio;
		public GameObject Win;
		public GameObject Lost;
		public GameObject stat;
		public GameObject wonqte;
		public GameObject responsetime;
		private Multi_Fields myFields;
		bool done = false;
		bool statButton = true;
		// Use this for initialization
		void Start ()
		{
				myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
				Win.renderer.material.color = new Vector4 (Win.renderer.material.color.r, Win.renderer.material.color.g, Win.renderer.material.color.b, 0);
				Lost.renderer.material.color = new Vector4 (Win.renderer.material.color.r, Win.renderer.material.color.g, Win.renderer.material.color.b, 0);
				Lost.SetActive (false);
				stat.SetActive (false);
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!done) {

						if (myFields.ServerHP == 0) {
								if (Network.isServer) {
										lost ();

								} else if (Network.isClient) {
										win ();

								}	
						} else if (myFields.ClientHP == 0) {
								if (Network.isServer) {
										win ();
				
								} else if (Network.isClient) {
										lost ();
				
								}	
						}
				}
		}
	
		void 	win ()
		{
				StartCoroutine ("fadein", Win);
		gameObject.audio.clip = winAudio;
		gameObject.audio.Play ();
				done = true;
		}

		void 	lost ()
		{
				StartCoroutine ("fadein2", Lost);
		gameObject.audio.clip = loseAudio;
		gameObject.audio.Play ();
				done = true;
		}
	
		IEnumerator fadein (GameObject o)
		{
				yield return new WaitForSeconds (0.1f);
		
				Vector3 ols = o.transform.localScale;
				for (float i=0; i<=1; i+=0.05f) {
						o.renderer.material.color = new Vector4 (o.renderer.material.color.r, o.renderer.material.color.g, o.renderer.material.color.b, i);
						o.transform.localScale = new Vector3 (ols.x + 20 * (1 - i), ols.y + 20 * (1 - i), 1);
						yield return new WaitForSeconds (0.01f);
				}
		
		}
	
		IEnumerator fadein2 (GameObject o)
		{

				yield return new WaitForSeconds (0.1f);
				Lost.SetActive (true);
				Vector3 op = o.transform.position;
				for (float i=0; i<=1; i+=0.05f) {
						o.renderer.material.color = new Vector4 (o.renderer.material.color.r, o.renderer.material.color.g, o.renderer.material.color.b, i);
						o.transform.position = new Vector3 (op.x, op.y - 3 * (1 - i), op.z);
						yield return new WaitForSeconds (0.01f);
				}
		
		}

		void OnGUI ()
		{
				// Create style for a button
				GUIStyle myButtonStyle = new GUIStyle (GUI.skin.button);
				myButtonStyle.fontSize = 30;
		
				// Load and set Font
				Font myFont = (Font)Resources.Load ("Fonts/comic", typeof(Font));
				myButtonStyle.font = myFont;
		
				if (done) {
						if (statButton && GUI.Button (new Rect (100, 100, 250, 100), "Statistic", myButtonStyle)) {
								Destroy (GameObject.Find ("Enemy"));
								statButton = false;
								Win.SetActive (false);
								Lost.SetActive (false);
								stat.SetActive (true);
								TextMesh tm = wonqte.GetComponent<TextMesh> ();
								TextMesh tm2 = responsetime.GetComponent<TextMesh> ();
								if (Network.isServer) {

										tm.text = tm.text + "  " + myFields.SWonQTE + "/" + (myFields.SWonQTE + myFields.CWonQTE);
										tm2.text = tm2.text + " " + myFields.ServerFinishTimeHelper + "s";
								} else if (Network.isClient) {

										tm.text = tm.text + "  " + myFields.CWonQTE + "/" + (myFields.SWonQTE + myFields.CWonQTE);
										tm2.text = tm2.text + " " + myFields.ClientFinishTimeHelper + "s";
								}
						}

						if (!statButton && GUI.Button (new Rect (Screen.width - 350, 100, 250, 100), "Exit", myButtonStyle)) {
								{
										Network.Disconnect ();
										MasterServer.UnregisterHost ();
										Application.LoadLevel ("mainmenu"); 
								}
						}
				}
		}
}
