using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRelayFirstLevel : MonoBehaviour {
	public List<EventRelayFirstLevel.EventMessageType> EventHandeld=
		new List<EventRelayFirstLevel.EventMessageType>();

	
	
	public delegate string EventAction(EventMessageType type);
	private static EventRelayFirstLevel eventManager;
	public static event EventAction OnEventAction;
    
	public enum  EventMessageType
	{
		
		OpenElevator,
		GetintoElevator,
		GetOutOfElvator,
		LookForKey,
		KeyFound,
		StartTheEngin,
		SetDestination,
		DestinationSet,
		EnginStarted,
		ClosepaceShip,
		OpenSpaceship,
		LaodLevelM2,
		Talkinfo1,
		Talkinfo2,
		Talkinfo3,
		ValidTask
	 
	}


	public static string RelayEvent(EventMessageType eventType)
	{
		if (OnEventAction != null) return OnEventAction(eventType);
		return null;
	}

 



}
