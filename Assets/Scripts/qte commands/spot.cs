using UnityEngine;
using System.Collections;

public class spot : MonoBehaviour {
	bool canTouch=true;

	void Start ()
	{
		StartCoroutine ("FadeOut");
	}




		IEnumerator FadeOut ()
		{
			for (float i=0; i<=1; i+=0.1f) {
				transform.localScale = new Vector3 (1.5f + 1.5f*i, 1.5f + 1.5f*i, 1);
				renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.b, renderer.material.color.g, 1 - i);
				yield return new WaitForSeconds (0.05f);

			}
			renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.b, renderer.material.color.g, 0);
			Destroy (gameObject);
		}
}

