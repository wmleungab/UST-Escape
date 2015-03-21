using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

	public float  marginX = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0)).x;
	public float  marginY = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0)).y;
}
