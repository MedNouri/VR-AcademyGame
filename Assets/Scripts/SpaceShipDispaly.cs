using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipDispaly : MonoBehaviour {

	
	public List<EventRelayFirstLevel.EventMessageType> EventsHandeld =
		new List<EventRelayFirstLevel.EventMessageType>();
	
 
public Texture m_Fin_The_Key;
public Texture m_Set_Distination;
public Texture m_pressThe_Button;
public Texture m_End;
	public Texture m_Info;
public Texture Camera;
Renderer m_Renderer;

// Use this for initialization
void Start () {
//Fetch the Renderer from the GameObject
m_Renderer = GetComponent<Renderer> ();

 

 

 
}
	
	

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

		if (EventsHandeld.Contains(type))
		{
			Debug.Log("Event Is recived Robot leve1 " + type);

			switch (type)
			{
				case EventRelayFirstLevel.EventMessageType.LookForKey:
					m_Renderer.material.SetTexture("_MainTex", m_Fin_The_Key);
					break;
				case EventRelayFirstLevel.EventMessageType.KeyFound:
					m_Renderer.material.SetTexture("_MainTex", m_Info);
					break;
				case  EventRelayFirstLevel.EventMessageType.SetDestination :
					m_Renderer.material.SetTexture("_MainTex", m_Set_Distination);
					break;
				 
					case EventRelayFirstLevel.EventMessageType.StartTheEngin:
						m_Renderer.material.SetTexture("_MainTex", m_pressThe_Button);	
						break;
				case EventRelayFirstLevel.EventMessageType.EnginStarted:
					m_Renderer.material.SetTexture("_MainTex",m_End);	
					Invoke("StartCamera",9f);
					break;
			}
		}

		return "Spas ship Disaply ";
	}


	private void StartCamera()
	{
		m_Renderer.material.SetTexture("_MainTex",Camera);	
		
		
	}
	

	
}
