using UnityEngine;
using System.Collections;

public class scr_character : MonoBehaviour 
{
	public enum navigationType {Waypoint, Objective};
	private navigationType myNavType;
	private int waypointID;
	private bool lineReversed;
	public enum spriteDirection {Up, Left, Down, Right};
	private spriteDirection mySpriteDirection;
	private Animator myAnimator;

	private scr_waypointManager waypointManager;
	public float distanceToStop;
	private NavMeshAgent agent;
	private Vector3 actualPosition;
	private Vector3 previousPosition;
	private bool moveTrigger;
	private Vector3 objective;

	public void StartMoving(RaycastHit hit)
	{
		agent.SetDestination(hit.point);
		objective = hit.point;
		moveTrigger = true;
		agent.updateRotation = false;
	}

	public void StartMoving()
	{
		if(waypointManager)
		{
			agent.SetDestination (waypointManager.waypoints[waypointID].transform.position);
			objective = waypointManager.waypoints[waypointID].transform.position;
			moveTrigger = true;
			agent.updateRotation = false;
		}
	}

	public void GetDistance()
	{
		float distance = Vector3.Distance (objective, this.transform.position);
		if (myNavType == navigationType.Waypoint && distance < distanceToStop)
		{
			waypointManager.GetNextPoint(ref waypointID, ref lineReversed);
			//Do what you are supposed to do there, according to your event

			StartMoving();
		}
	}

	public void SetDirection()
	{
		actualPosition = this.transform.position;
		Vector3 delta = actualPosition - previousPosition;
		float x = delta.x;
		float y = delta.z;
		float angle = Mathf.Atan2(y, x) * (180 / Mathf.PI);
		if(angle > 0 && angle < 45) 
		{
			mySpriteDirection = spriteDirection.Right;
			myAnimator.SetInteger ("direction", 4);
		}
		if(angle > 45 && angle < 135) 
		{
			mySpriteDirection = spriteDirection.Up;
			myAnimator.SetInteger ("direction", 1);
		}
		if(angle > 135 && angle < 225) 
		{
			mySpriteDirection = spriteDirection.Left;
			myAnimator.SetInteger ("direction", 2);
		}
		if(angle < 0 && angle > -45) 
		{
			mySpriteDirection = spriteDirection.Right;
			myAnimator.SetInteger ("direction", 4);
		}
		if(angle < -45 && angle > -135) 
		{
			mySpriteDirection = spriteDirection.Down;
			myAnimator.SetInteger ("direction", 3);
		}
		if(angle < -135 && angle > -225) 
		{
			mySpriteDirection = spriteDirection.Left;
			myAnimator.SetInteger ("direction", 2);
		}
		previousPosition = this.transform.position;
	}

	public void Stop()
	{
		agent.velocity = Vector3.zero;
		agent.updatePosition = false;
	}

	public navigationType MyNavType
	{
		get {return myNavType;}
		set {myNavType = value;}
	}

	public spriteDirection MySpriteDirection
	{
		get {return mySpriteDirection;}
		set {mySpriteDirection = value;}
	}

	public scr_waypointManager WaypointManager
	{
		get {return waypointManager;}
		set {waypointManager = value;}
	}

	public int WaypointID
	{
		get {return waypointID;}
		set {waypointID = value;}
	}

	public bool LineReversed
	{
		get {return lineReversed;}
		set {lineReversed = value;}
	}

	public Animator MyAnimator
	{
		get {return myAnimator;}
		set {myAnimator = value;}
	}

	public NavMeshAgent Agent
	{
		get {return agent;}
		set {agent = value;}
	}

	public Vector3 ActualPosition
	{
		get {return actualPosition;}
		set {actualPosition = value;}
	}

	public Vector3 PreviousPosition
	{
		get {return previousPosition;}
		set {previousPosition = value;}
	}

	public bool MoveTrigger
	{
		get {return moveTrigger;}
		set {moveTrigger = value;}
	}
}
