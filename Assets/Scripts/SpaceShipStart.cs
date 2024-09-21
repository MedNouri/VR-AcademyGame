using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SpaceShipStart : MonoBehaviour
{	public Material KeyMaterial;
	 
	public GameObject keyisOnObject;
	public LinearMapping linearMapping;
	private float currentLinearMapping = float.NaN;
	private bool DestnationIsSet;
	public List<EventRelayFirstLevel.EventMessageType> EventHanded = new List<EventRelayFirstLevel.EventMessageType>();
	private void OnEnable()
	{

		EventRelayFirstLevel.OnEventAction += HandleEvent;
	}



	private void OnDisable()
	{

		EventRelayFirstLevel.OnEventAction -= HandleEvent;
	}

	string HandleEvent(EventRelayFirstLevel.EventMessageType type)

	{

		if (EventHanded.Contains(type))
		{
			switch (type)
			{
				case EventRelayFirstLevel.EventMessageType.DestinationSet:
					DestnationIsSet = true;
					break;
		 
			}
		}

		return null;
	}

	
	// Use this for initialization
	void Start () {
		
		if ( linearMapping == null )
		{
			linearMapping = GetComponent<LinearMapping>();
		}
	}

	private bool isActionMade;

	private void FixedUpdate()
	{
 	if (currentLinearMapping != linearMapping.value)
		{
			currentLinearMapping = linearMapping.value;
			if (currentLinearMapping ==1f)
			{
				if (DestnationIsSet)
				{
					
				
				if (!isActionMade)
				{
					EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.EnginStarted);
					isActionMade = true;
					keyisOnObject.GetComponent<Renderer>().material = KeyMaterial;
				}
					
				}
			}

			{
				
			}
		}
	}
}
