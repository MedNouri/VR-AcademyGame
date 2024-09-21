using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabDisplay : MonoBehaviour {
	public List<EventRelayLevel2.EventMessageType> EventsHandeld=
		new List<EventRelayLevel2.EventMessageType>();
	public Texture WelcomeTotheLab;
	public Texture BeSafe;
	public Texture Carbon;
	public Texture Nacl ;
	public Texture Boil;
	public Texture Dilution;
	public Texture Virus;
	public Texture End;
 
	Renderer m_Renderer;
	private void OnEnable()
	{
	
		EventRelayLevel2.OnEventAction += HandleEvent;
	 
	}
 

	private void OnDisable()
	{
		EventRelayLevel2.OnEventAction -= HandleEvent;
	 
	}
 
	void Start () {
 
		m_Renderer = GetComponent<Renderer> ();

	 
 
	}

	string HandleEvent(EventRelayLevel2.EventMessageType type)
	{

		if (EventsHandeld.Contains(type))
		{
			 
			switch (type)
			{
				case EventRelayLevel2.EventMessageType.PlayerisOnTheLab:
					m_Renderer.material.SetTexture("_MainTex", WelcomeTotheLab);
					break;
					;

				case EventRelayLevel2.EventMessageType.BeStafe:
					m_Renderer.material.SetTexture("_MainTex", BeSafe);
					break;
					;
					
					
				case EventRelayLevel2.EventMessageType.CarbonMission:
					m_Renderer.material.SetTexture("_MainTex", Carbon);
					break;
					;
				case EventRelayLevel2.EventMessageType.InFoNacl:
					m_Renderer.material.SetTexture("_MainTex", Nacl);
					break;
					;				
				case EventRelayLevel2.EventMessageType.DilutionStart:
					m_Renderer.material.SetTexture("_MainTex", Dilution);
					break;
					;

				case EventRelayLevel2.EventMessageType.BoilStart:
					m_Renderer.material.SetTexture("_MainTex", Boil);
					break;
					;
										
	
					
				case EventRelayLevel2.EventMessageType.VirusStart:
					m_Renderer.material.SetTexture("_MainTex", Virus);
					break;
					;
					
				case EventRelayLevel2.EventMessageType.EnDMission:
					m_Renderer.material.SetTexture("_MainTex", End);
					break;
					;
			}

		}

		return "";
	}
}
