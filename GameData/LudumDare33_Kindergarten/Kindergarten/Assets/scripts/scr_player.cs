using UnityEngine;
using System.Collections;

public class scr_player : scr_character {	 

	// Use this for initialization
	void Start () 
	{
		Agent = this.GetComponent<NavMeshAgent>();
		MyNavType = navigationType.Objective;
		MySpriteDirection = spriteDirection.Down;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(MoveTrigger) SetDirection();
		if(Input.GetMouseButtonDown(0))
		{
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast (ray, out hit, 100))
			{
				StartMoving (hit);
			}
			PreviousPosition = this.transform.position;
		}
	}
}
