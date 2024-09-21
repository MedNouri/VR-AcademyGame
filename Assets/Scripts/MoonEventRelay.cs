using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonEventRelay : MonoBehaviour {

	public List<MoonEventRelay.EventMessageType> EventHandeld=
		new List<MoonEventRelay.EventMessageType>();

	
	
	public delegate string EventAction(EventMessageType type);
    
	public static event EventAction OnEventAction;
    
	public enum  EventMessageType
	{
       Landing,
	    AskForStoppingTheEngin,
		DoorOpen,
		EnginShutDown,
		DoorIsOpen,
		PlayerIsOut,
		RobotShouldGetout,
		FixSignal,
		PlayerShouldStartClamb,
		RobotGetUp,
		ClambEnd,
		SignalFixed,
		EnemiesAttackStart,
		EnimesAttackEnd,
		EnemiGotKilled,
		ValidTask,
	    GobackToSapceShip,
		CloseTheSpaceShipe,
		EndLevel,
	}

	public static string RelayEvent(EventMessageType eventType)
	{
	
		return OnEventAction(eventType);
	}



}
