using UnityEngine;
using System.Collections;

public class scr_waypointManager : MonoBehaviour {
	public enum waypointType {Line, Lap};
	public waypointType myWaypointType;
	public scr_waypoint[] waypoints;
	public scr_waypoint waypoint;


	// Use this for initialization
	void Start () {
		waypoints = GetComponentsInChildren<scr_waypoint>();
		if(waypoints.Length == 0)
		{
			scr_waypoint safetyWaypoint = Instantiate (waypoint, this.transform.position, Quaternion.identity) as scr_waypoint;
			safetyWaypoint.transform.parent = this.transform;
			waypoints = GetComponentsInChildren<scr_waypoint>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Transform GetNextPoint(ref int waypointID, ref bool lineReversed)
	{
		if(lineReversed)
		{
			waypointID--;
		}
		else
		{
			waypointID++;
		}
		if(waypointID < 0)
		{
			switch(myWaypointType)
			{
			case waypointType.Lap:
				waypointID = waypoints.Length - 1;
				return waypoints[waypointID].transform;
			case waypointType.Line:
				waypointID = 0;
				lineReversed = !lineReversed;
				return waypoints[waypointID].transform;
			default:
				break;
			}
		}
		if(waypointID > waypoints.Length - 1)
		{
			switch(myWaypointType)
			{
			case waypointType.Lap:
				waypointID = 0;
				return waypoints[waypointID].transform;
			case waypointType.Line:
				waypointID = waypoints.Length - 1;
				lineReversed = !lineReversed;
				return waypoints[waypointID].transform;
			default:
				break;
			}
		}
		return waypoints[waypointID].transform;
	}

	public waypointType MyWaypointType
	{
		get {return myWaypointType;}
		set {myWaypointType = value;}
	}

	void OnDrawGizmos() {
		// Draw a yellow sphere at the transform's position
		Gizmos.color = new Color(1f, 0f, 0f, 0.75f);
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.color = new Color(1f, 0f, 0f, 1f);
		Gizmos.DrawSphere(Vector3.zero, 0.5f);
		for(int i = 1; i < waypoints.Length; i++)
		{
			Gizmos.DrawLine (waypoints[i-1].transform.localPosition, waypoints[i].transform.localPosition);
		}
	}
}
