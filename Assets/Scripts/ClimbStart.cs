using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbStart : MonoBehaviour {
	private bool isaActionDone;
	private bool CanStart;
	
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




	string HandleEvent(MoonEventRelay.EventMessageType type)
	{

		if (EventsHandeld.Contains(type))
		{
			Debug.Log("Level 2 M1 Event Recevide  ");
			switch (type)
			{
				case MoonEventRelay.EventMessageType.PlayerShouldStartClamb:
					CanStart = true;
					break;
			}
		}

		return null;
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Robot Get Up 11");

		if (other.gameObject.transform.root.CompareTag("Player"))
		{
			
			if(CanStart){
		 
				if (!isaActionDone)
				{
					isaActionDone = true;
			
					Debug.Log("Robot Get Up");
					MoonEventRelay.RelayEvent (MoonEventRelay.EventMessageType.RobotGetUp);
					gameObject.GetComponent<Collider>().enabled=false; 
					Destroy(gameObject);
				}
		 
		}
		}
	 
	}
}
