using UnityEngine;
using System.Collections;

public class scr_waypoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos() {
		// Draw a yellow sphere at the transform's position
		Gizmos.color = new Color(0.5f, 1f, 0f, 0.5f);
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawSphere(Vector3.zero, 0.5f);
	}
}
