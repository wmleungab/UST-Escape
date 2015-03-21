using UnityEngine;
using System.Collections;

public class Multi_QTETouch : MonoBehaviour
{

		public GameObject TouchingPt;
		public Multi_Fields myFields;
		private int success = 0;
		private int num_points = 7;
		Vector3 point;
		Vector3 mouseStart;
		private long startTime = 0;

		void Start ()
		{

				
				myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
				
				mouseStart = Vector3.zero;
				
				long period = 10L * 60L * 1000L * 10000L; // In ticks
				long timeStamp = System.DateTime.Now.Ticks + period;
				startTime = timeStamp;
				//point = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, Screen.width), Random.Range (0, Screen.height), 1));
				drawTargetPoint ();
				
		}

		void drawTargetPoint ()
		{


		
				//point = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width , Screen.height , 1));
				float x = Random.Range (0, Screen.width);
				float y = Random.Range (0, Screen.height);
	
				CircleCollider2D temp = GetComponents<CircleCollider2D> () [0]as CircleCollider2D;
				float r = temp.radius;
		
				Vector3 minP = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 1));
				Vector3 maxP = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 1));
				point = Camera.main.ScreenToWorldPoint (new Vector3 (x, y, 1));
				if (point.x + r >= maxP.x)
						point.x -= r;
				if (point.y + r >= maxP.y)
						point.y -= r;
				if (point.x - r <= minP.x)
						point.x += r;
				if (point.y - r <= minP.y)
						point.y += r;
				gameObject.transform.position = point;
		}
		
		void  OnTriggerEnter2D (Collider2D col)
		{

				if (col.gameObject.name == "spot(Clone)" && success < num_points) {
					
		
			
						success++;
						if (success >= num_points) {
								roundEnd ();
						} else 
								drawTargetPoint ();

				}
		}
		// Update is called once per frame
		void Update ()
		{
				
				if (Input.touchCount > 0) {
						if (Input.GetTouch (0).phase == TouchPhase.Began) {									//begin
								mouseStart = Input.GetTouch (0).position;
								
								mouseStart = Camera.main.ScreenToWorldPoint (new Vector3 (mouseStart.x, mouseStart.y, 1));
								
								Instantiate (TouchingPt, mouseStart, Quaternion.identity);
								
								
								
						}
				}

		
		}

		void roundEnd ()
		{
				if (Network.isClient) {
			
						myFields.syncState (Multi_Fields.States.CLIENT_FINISH, true);
						long period = 10L * 60L * 1000L * 10000L; // In ticks
						long timeStamp = System.DateTime.Now.Ticks + period;
						timeStamp -= startTime;
						myFields.syncTime (timeStamp, false);
						Destroy (gameObject);
			
				} else {
						myFields.syncState (Multi_Fields.States.SERVER_FINISH, true);
						long period = 10L * 60L * 1000L * 10000L; // In ticks
						long timeStamp = System.DateTime.Now.Ticks + period;
						timeStamp -= startTime;
						myFields.syncTime (timeStamp, true);
						Destroy (gameObject);
				}
		}

}