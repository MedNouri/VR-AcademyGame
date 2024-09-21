using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LunarEngin : MonoBehaviour {

	public LinearMapping linearMapping;
	private float _currentLinearMapping = float.NaN;
 

private bool isActionMadeEngin;
	
	public List<MoonEventRelay.EventMessageType> EventsHandeld=
		new List<MoonEventRelay.EventMessageType>();

 
	private void OnEnable()
	{
		MoonEventRelay.OnEventAction += HandleEvent;
	 
	}
 

	private void OnDisable()
	{
		MoonEventRelay.OnEventAction -= HandleEvent;
	 
	}

	private bool _canStart;


	string HandleEvent(MoonEventRelay.EventMessageType type)
	{

		if (EventsHandeld.Contains(type))
		{

			switch (type)
			{
				case MoonEventRelay.EventMessageType.AskForStoppingTheEngin:
					_canStart = true;
					break;
			}

			
		}

		return "";
	}

	void Update()
{
if (_currentLinearMapping != linearMapping.value)
{
_currentLinearMapping = linearMapping.value;

	if (_canStart)
	{
		
	
if (_currentLinearMapping>0.9)
{
	Debug.Log("Avreg event door ");
	if (!isActionMadeEngin)
	{
		Debug.Log("Avreg event door  sent ");		
		MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.EnginShutDown);
		isActionMadeEngin =true;
	}
}
	}
	else
	{
		linearMapping.value = 0;

	}
		 
}
}
	
}
