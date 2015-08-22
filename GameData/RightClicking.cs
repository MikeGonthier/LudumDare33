using UnityEngine;
using System.Collections;

public class RightClicking : MonoBehaviour {

	private bool isRightClicked = false;

	void Update(){
		if (Input.GetMouseButtonDown(1) && isRightClicked == false) {
			Debug.Log ("je right click");
			isRightClicked = true;

		} else
			isRightClicked = false;
	}
}