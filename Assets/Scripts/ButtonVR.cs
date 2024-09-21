using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

[System.Serializable]
public class VRButtonEvent : UnityEvent<ButtonVR> {}
public class ButtonVR : Interactable{
	public VRButtonEvent ButtonListeners;
	
	
	void OnTriggerEnter(Collider _collider)
	{	
	 
			Debug.Log("event happend");
 
	}

	void OnCollisionEnter(Collision _collision)
	{
		
		 
			TriggerButton (); // If the button hit's the contact switch it has been pressed
			Debug.Log("event happend");
	 


	}

	void OnCollisionExit(Collision _collision)
	{
		if (_collision.rigidbody == null)
			return;

	
			
	}

	public float TriggerHapticStrength = 0.5f;

	void TriggerButton ()
	{
	
	


	}

}
