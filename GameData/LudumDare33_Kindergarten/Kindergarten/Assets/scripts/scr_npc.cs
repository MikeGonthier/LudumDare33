using UnityEngine;
using System.Collections;

public class scr_npc : scr_character {

	// Use this for initialization
	void Start () {
		Agent = this.GetComponent<NavMeshAgent>();
		MyNavType = navigationType.Waypoint;
		MySpriteDirection = spriteDirection.Down;
		MoveTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!MoveTrigger)
		{
			WaypointID = 0;
			StartMoving();
		}
		GetDistance();
	}
}
