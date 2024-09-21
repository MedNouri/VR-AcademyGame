using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratoryDoor : MonoBehaviour {

	 
	 
	public List<EventRelayLevel2.EventMessageType> EventsHandled=
		new List<EventRelayLevel2.EventMessageType>();

	
 
	
	
	
	
	
	
	
	private void OnEnable()
	{
		EventRelayLevel2.OnEventAction += HandleEvent;
	 
	}
 

	private void OnDisable()
	{
		EventRelayLevel2.OnEventAction -= HandleEvent;

	}

	
	
		
	
	string HandleEvent(EventRelayLevel2.EventMessageType type)
	{

		if (EventsHandled.Contains(type))
		{
			Debug.Log("Level manger HAndel Evtn Recevide  ");
			switch (type)
			{
				case EventRelayLevel2.EventMessageType.EnterPassword:
			   
					Debug.Log("Door says enter the Password");
					break;
					;


			}

		}

		return "";
	}


	
}
