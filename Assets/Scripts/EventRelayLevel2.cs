using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRelayLevel2 : MonoBehaviour {
	
	
	public List<EventRelayLevel2.EventMessageType> EventHandeld=
		new List<EventRelayLevel2.EventMessageType>();

	
	
	public delegate string EventAction(EventMessageType type);
    
	public static event EventAction OnEventAction;
    
	public enum  EventMessageType
	{
		
		EnterPassword,
		DoorisOpen,
		PlayerisOnTheLab,
	   BeStafe,
		CarbonMission,
		CarbonMissionEnd,
		InFoNacl,
		DilutionStart,
		DilutionEnd,
		BoilStart,
		BoilEnd,
		VirusStart,
		VirusDead,
		VirusEnd,
		EnDMission
 
	}

	public static string RelayEvent(EventMessageType eventType)
	{
		return OnEventAction(eventType);
	}

}
