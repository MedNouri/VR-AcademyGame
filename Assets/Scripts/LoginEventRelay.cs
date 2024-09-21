using System.Collections;
using System.Collections.Generic;
using UnityEngine;



 
public class LoginEventRelay : MonoBehaviour {
 

	public delegate string EventAction(EventMessageType type);
    
	public static event EventAction OnEventAction;
    
	
	// Liste Of Events To Relay  
	public enum  EventMessageType
	{
		RobotFinishTalking,
		FillTheForm,
		FillName,
		ErrorInput,
		Validinput,
		FormCompleted,
		Exit
	}

	
	// Event Relay Sender 
	public static string RelayEvent(EventMessageType eventType)
	{
	 
			return OnEventAction(eventType);
		 
	}




}

 
