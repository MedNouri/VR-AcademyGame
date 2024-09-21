using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CarbonTask : MonoBehaviour {

private  int _numberOfSnapes =2;


	private void OnDisable()
	{
		OxygenOne.OxygenisSnaped-=OxygenOneOnOxygenisSnaped;
	}


	private void OnEnable()
	{
		OxygenOne.OxygenisSnaped+=OxygenOneOnOxygenisSnaped;
	}


	private bool _isActionIsMAde;

	private void OxygenOneOnOxygenisSnaped()
	{	 
		 
			_numberOfSnapes--;
		Debug.Log("number sis "+_numberOfSnapes);
	     if (_numberOfSnapes==0)
		{ 
			EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.CarbonMissionEnd);
			
			
			
		}
		}
 
		
	}
 
