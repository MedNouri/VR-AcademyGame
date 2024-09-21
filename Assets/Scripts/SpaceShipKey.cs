using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SpaceShipKey :SnapToPostion
{


	public Material KeyMaterial;
	 
	public GameObject keyisOnObject;
	// Use this for initialization
	void Start ()
	{
		StartDetection = true;
	}

	private bool _isActionMad;


	private void OnEnable()
	{
		SnapToPostion.ObjectSnappedToDropZone+= OnKeySnap;
	}

	private void OnDisable()
	{
		SnapToPostion.ObjectSnappedToDropZone-= OnKeySnap;
	}

 



	void OnKeySnap()
	{
		if (!_isActionMad)
		{_isActionMad = true;
		 
			EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.KeyFound);
			keyisOnObject.GetComponent<Renderer>().material = KeyMaterial;
		}
		}

}
