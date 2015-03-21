using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Component[] c=gameObject.GetComponentsInChildren(typeof(Renderer));
		foreach(Component r in c){
			StartCoroutine("anim",(Renderer)r);
			
		}
	}
	

	IEnumerator anim (Renderer r)
	{
		float oria = r.material.color.a;
		for (float i=0; i<1; i+=0.05f) {
			r.material.color = new Color (r.material.color.r, r.material.color.b, r.material.color.g, i * oria);
			yield return new WaitForSeconds (0.01f);
		}
		r.material.color = new Color (r.material.color.r, r.material.color.b, r.material.color.g, oria);
		
	}
}
