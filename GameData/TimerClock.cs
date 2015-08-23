using UnityEngine;
using System.Collections;

public class TimerClock : MonoBehaviour {


	private int currentTime;
	private int timeElapsed;
	public int timeLimit;
		
	private bool timeStopped;
	private bool isMorning;
	private bool isNoon;
	private bool isAfternoon;
	private bool textAfficher;


	// Use this for initialization
	void Awake () {
	
		currentTime = (int)Time.time;//redemarre le countdown
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (timeStopped == false)
		{
			timeElapsed = (int)Time.time - currentTime;//faire tourner le conteur

			if (timeLimit < timeElapsed)//arrete le conteur si limite atteinte
			{
				timeStopped = true;
			}

			if ((timeLimit *.2 < timeElapsed))//verifie si le chrono est entre 30 - 40 %
			{
				textAfficher = true;

				if ((timeLimit *.3 < timeElapsed))
				{
					textAfficher = false;
				}
			}

			if((timeLimit *.5 < timeElapsed))//verifie si le chrono est entre 60 - 70 %
			{
				textAfficher = true;

				if (( timeLimit *.6 < timeElapsed))
				{
					textAfficher = false;
				}
			}
		}

		else 
		{
			timeElapsed = timeLimit;
		}

		if (textAfficher == true)
		{
			Debug.Log("sieste");
		}

		if (textAfficher == false)
		{
			Debug.Log("playtime");
		}

		Debug.Log (timeElapsed);

	
	}
}
