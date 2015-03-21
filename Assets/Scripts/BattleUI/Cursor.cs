using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public GameObject cursorm;
	public GameObject cursora;
	public GameObject p;

	Vector3 mouseStart;
	Vector3 mouseCurrent;
	// Use this for initialization
	
	GameObject ca;
	GameObject cm;
	void Start () {
		mouseStart = Vector3.zero;
		mouseCurrent = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.touchCount > 0) {
						if (Input.GetTouch (0).phase == TouchPhase.Began) {									//begin
								mouseStart = Input.GetTouch (0).position;
						mouseCurrent = Input.GetTouch (0).position;
				mouseStart = Camera.main.ScreenToWorldPoint (new Vector3 (mouseStart.x, mouseStart.y, 1));
				mouseCurrent = Camera.main.ScreenToWorldPoint (new Vector3 (mouseCurrent.x, mouseCurrent.y, 1));

				GameObject p1=Instantiate (p, mouseStart, Quaternion.identity)as GameObject;
				cm = Instantiate (cursorm, mouseStart, Quaternion.identity)as GameObject;
				ca = Instantiate (cursora, mouseCurrent, Quaternion.identity)as GameObject;
				cm.SetActive(false);
				ca.SetActive(false);
			} else if (Input.GetTouch (0).phase == TouchPhase.Moved) {													//move
								mouseCurrent = Input.GetTouch (0).position;
				mouseCurrent = Camera.main.ScreenToWorldPoint (new Vector3 (mouseCurrent.x, mouseCurrent.y, 1));
				ca.transform.position=mouseCurrent;
				ca.SetActive(true);
				cm.SetActive(true);

				float	h = cursorm.renderer.bounds.size.y;
				float d=Vector3.Distance(mouseStart,mouseCurrent);
				float a=Vector3.Angle(Vector3.up,mouseCurrent-mouseStart);
				if(mouseCurrent.x>mouseStart.x)
					a=360-a;
				if (ca != null) {
					ca.transform.rotation = Quaternion.Euler (0, 0, a);
					ca.renderer.material.color=new Vector4(ca.renderer.material.color.r,ca.renderer.material.color.g,ca.renderer.material.color.b, Mathf.Min(d/3,1));
				}
				if (cm != null) {
					cm.transform.rotation = Quaternion.Euler (0, 0, a);
					cm.transform.localScale = new Vector3 (cursorm.transform.localScale.x, cursorm.transform.localScale.y * d / h, 1);
					
					cm.renderer.material.color=new Vector4(ca.renderer.material.color.r,ca.renderer.material.color.g,ca.renderer.material.color.b, Mathf.Min(d/3,1));
				}
				
			}
				else if(Input.GetTouch (0).phase == TouchPhase.Ended || Input.GetTouch (0).phase == TouchPhase.Canceled){
					Destroy(cm);
				Destroy(ca);
				}
			}
			

				

}
}