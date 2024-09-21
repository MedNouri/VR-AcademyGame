

using UnityEngine;
using System.Collections;
using System;
//-------------------------------------------------------------------------
[RequireComponent( typeof( AudioSource ) )]
public class Clock: MonoBehaviour {
 
	/// <param name="Hours ">Hours Game Object</param>

	public Transform Hours;
	/// <param name="Minutes">Minutes Game Object</param>
	public Transform Minutes;
	/// <param name="Seconds ">SecondsGame Object</param>
	public Transform Seconds;


	private float _hour, _minute, _second;
 
	void Update () {
		  _hour = System.DateTime.Now.Hour;
		  _minute = System.DateTime.Now.Minute;
		  _second = System.DateTime.Now.Second;
	 


		_hour = _hour + _minute / 60f;
		_minute = _minute + _second / 60f;

	 


		if(Hours)
			Hours.localRotation = Quaternion.Euler (0, 0, _hour / 12 * 360);

		if(Minutes)
			Minutes.localRotation = Quaternion.Euler (0, 0, _minute / 60 * 360);

		if(Seconds)
			Seconds.localRotation = Quaternion.Euler (0, 0, _second / 60 * 360);


	}
}
