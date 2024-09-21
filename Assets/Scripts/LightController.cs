using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using Valve.VR.InteractionSystem;

/// <summary>
/// Example script. Controls a point light and a material to sets the light bulb's colour and intensity.
/// </summary>
[RequireComponent(typeof(Light))]
public class LightController : MonoBehaviour
{
 
	// Required components
	private Light mLight;
	private float oldintensity;


	void OnEnable()
	{
		
		mLight = GetComponent<Light> ();
		if (mLight == null)
		{
			Debug.LogError("Liight is missing from " + name);
		}
		else
		{
			oldintensity=mLight.intensity;
		}

	}


	/// <summary>
	///Function called by the button to turn on / off the light on the Game Object 
	/// </summary>
	public void TurnOffOnLight()
	{
 
		if (mLight.intensity != 0)
		{
			mLight.intensity = 0;
		}else
		{
			mLight.intensity = oldintensity;
		}


	}

}
