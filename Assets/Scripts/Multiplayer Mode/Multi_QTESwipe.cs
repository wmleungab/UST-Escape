using UnityEngine;
using System.Collections;

public class Multi_QTESwipe : MonoBehaviour
{
	

		public GameObject mouseCollider;
		public Multi_Fields myFields;
		private int success = 0;
		private int num_Swipe = 5;
		public GameObject ptChild1;
		public GameObject ptChild2;
		public GameObject arrow;
		public GameObject hint1;
		public GameObject hint2;
		Vector3 point1;
		Vector3 point2;
		Vector3 mouseStartPos;
		Vector3 mouseCurrentPos;
		GameObject mouseStart;
		GameObject mouseCurrent;
		bool started;
		bool moving;
		bool failed;

		private long startTime = 0;

		void Start ()
		{
		
		
				myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
		
				mouseStartPos = Vector3.zero;
				mouseCurrentPos = Vector3.zero;
				started = false;
				moving = false;
				failed = false;
				long period = 10L * 60L * 1000L * 10000L; // In ticks
				long timeStamp = System.DateTime.Now.Ticks + period;
				startTime = timeStamp;
				//point = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, Screen.width), Random.Range (0, Screen.height), 1));
				drawSwipeLocus ();
		
		}

		bool collisionWithEdges ()
		{
				Vector3 minP = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 1));
				Vector3 maxP = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 1));

				CircleCollider2D temp1 = ptChild1.GetComponents<CircleCollider2D> () [0]as CircleCollider2D;
				float r1 = temp1.radius;
				CircleCollider2D temp2 = ptChild2.GetComponents<CircleCollider2D> () [0]as CircleCollider2D;
				float r2 = temp2.radius;
				point1 = ptChild1.transform.position;
				point2 = ptChild2.transform.position;
				/*Debug.Log ("parent x: " + transform.position.x);
				Debug.Log ("point1x: " + (point1.x));
				Debug.Log ("point1x+r: " + (point1.x + r1));
				Debug.Log ("point1x-r: " + (point1.x - r1));
				Debug.Log ("point2x: " + (point2.x));
				Debug.Log ("point2x+r: " + (point2.x + r2));
				Debug.Log ("point2x-r: " + (point2.x - r2));
				Debug.Log ("maxP x: " + maxP.x);
				Debug.Log ("minP x: " + minP.x);

				Debug.Log ("parent y: " + transform.position.y);
				Debug.Log ("point1y: " + (point1.y));
				Debug.Log ("point1y+r: " + (point1.y + r1));
				Debug.Log ("point1y-r: " + (point1.y - r1));
				Debug.Log ("point2y: " + (point2.y));
				Debug.Log ("point2y+r: " + (point2.y + r2));
				Debug.Log ("point2y-r: " + (point2.y - r2));
				Debug.Log ("maxP y: " + maxP.y);
				Debug.Log ("minP y: " + minP.y);*/

				if ((point1.x + r1 >= maxP.x)
						|| (point1.y + r1 >= maxP.y)
						|| (point1.x - r1 <= minP.x)
						|| (point1.y - r1 <= minP.y)
						|| (point2.x + r2 >= maxP.x)
						|| (point2.y + r2 >= maxP.y)
						|| (point2.x - r2 <= minP.x)
						|| (point2.y - r2 <= minP.y)) {
						
						//Debug.Log ("collisionWithEdges!!!");
						return true;
				} else {
						
						//Debug.Log ("No collisionWithEdges!!!");
						return false;
				}
		}

		void drawSwipeLocus ()
		{
				gameObject.SetActive (false);
				int angle = Random.Range (0, 359);
				transform.RotateAround (transform.position, Vector3.forward, angle);
		
				//point = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width , Screen.height , 1));
				do {
						float x = Random.Range (0, Screen.width);
						float y = Random.Range (0, Screen.height);
						transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (x, y, 1));
						
				} while (collisionWithEdges());
				

				
				if (angle < 270 && angle > 90) {
						
						hint2.SetActive (true);
						hint1.SetActive (false);
				} else {
						hint1.SetActive (true);
						hint2.SetActive (false);
				}

				gameObject.SetActive (true);
		
		}

		public void triggerEnterArrow (Collider2D col)
		{
	
				if (col.gameObject == mouseCurrent && success < num_Swipe) {
			
						//Debug.Log ("moving on tracking");

		
			
				}
		}

		public void triggerStayArrow (Collider2D col)
		{
				if (col.gameObject == mouseCurrent && success < num_Swipe) {
						if (started && !failed) {
								moving = true;
								Debug.Log ("moving on tracking");
						}
				}
		}

		public void triggerExitArrow (Collider2D col)
		{
				if (col.gameObject == mouseCurrent && success < num_Swipe) {
						if (moving && started & !failed) {
								moving = false;
								started = false;
								failed = true;
								Debug.Log ("exit track");
						}
		
				}
		}
		
		public void collisionStartPt (Collider2D col)
		{
				if (col.gameObject == mouseCurrent && success < num_Swipe) {
			
						Debug.Log ("starts");
						started = true;
						failed = false;
				}
		}

		public void collisionEndPt (Collider2D col)
		{
				if (col.gameObject == mouseCurrent && success < num_Swipe) {
						if (moving && started && !failed) {

								moving = false;
								started = false;
								Debug.Log ("finish");
								success++;
				if (success >= num_Swipe) {roundEnd();}
							else drawSwipeLocus ();
						}			
				}
		}

		// Update is called once per frame
		void Update ()
		{

				if (Input.touchCount > 0) {
						if (Input.GetTouch (0).phase == TouchPhase.Began) {									//begin
								mouseStartPos = Input.GetTouch (0).position;

								mouseStartPos = Camera.main.ScreenToWorldPoint (new Vector3 (mouseStartPos.x, mouseStartPos.y, 1));

								if (mouseCurrent != null) {
					
										mouseCurrent.transform.position = mouseCurrentPos;
								} else {
										mouseCurrent = Instantiate (mouseCollider, mouseStartPos, Quaternion.identity)as GameObject;
								}
				
			
				
						} else if (Input.GetTouch (0).phase == TouchPhase.Moved) {													//move
								mouseCurrentPos = Input.GetTouch (0).position;
								mouseCurrentPos = Camera.main.ScreenToWorldPoint (new Vector3 (mouseCurrentPos.x, mouseCurrentPos.y, 1));
								if (mouseCurrent != null) {

										mouseCurrent.transform.position = mouseCurrentPos;
								} else {
										mouseCurrent = Instantiate (mouseCollider, mouseStartPos, Quaternion.identity)as GameObject;
								}
						} else if (Input.GetTouch (0).phase == TouchPhase.Ended || Input.GetTouch (0).phase == TouchPhase.Canceled) {
								if (moving && started & !failed) {
										moving = false;
										started = false;
										failed = true;
										Debug.Log ("exit track");
										Destroy (mouseCurrent);
								}
						}
				
				}
		}

	void roundEnd(){

	
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