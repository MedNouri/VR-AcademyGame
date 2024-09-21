using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ChemistryDoor : MonoBehaviour {
	private Animator Animator;
	private AudioSource _audioSource;
	private int  _fakepressedButton = 5;
	public TextMesh TextdoorMesh;
	public TeleportArea InsideTheLab;
	
	public List<EventRelayLevel2.EventMessageType> EventsHandeld=
		new List<EventRelayLevel2.EventMessageType>();
	
	 
	private void OnEnable()
	{
		EventRelayLevel2.OnEventAction += HandleEvent;
	 
	}
 

	private void OnDisable()
	{
		EventRelayLevel2.OnEventAction -= HandleEvent;
	 
	}

	private void Start()
	{
		if ( Animator == null )
		{
			Animator = GetComponent<Animator>();
		}

		if (_audioSource==null)
		{
			_audioSource = GetComponent<AudioSource>();
		}

		TextdoorMesh.text = "Enter Password";
	}
	
	
	string HandleEvent(EventRelayLevel2.EventMessageType type)
	{

		if (EventsHandeld.Contains(type))
		{
			 
			switch (type)
			{
				case EventRelayLevel2.EventMessageType.DoorisOpen:
			   
					OpenDoor();
					break;
					;


			}

		}

		return "";
	}
private void  OpenDoor()
	{
		Animator.SetBool("Open",true);
	
		_audioSource.Play();
		 
	}
	private void  CloseDoor()
	{
		Animator.SetBool("Open",false);
	
		_audioSource.Play();
		 
	}


private 	bool EnterPasswordWasMade=false;
	public void EnterPoosword()
	{
	
		if (_fakepressedButton == 0)
		{
			if (!EnterPasswordWasMade)
			{
				EnterPasswordWasMade = true;
				EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.DoorisOpen);
				
			}

			TextdoorMesh.text = "Good enough";

			InsideTheLab.SetLocked(false);
		
		}
		else
		{
			_fakepressedButton--;
			TextdoorMesh.text = "Invalid Password";
		}
		
	}

	private bool isEventSent;
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.CompareTag("Player"))
		{Debug.Log("Ihave The Player");
			if (!isEventSent)
			{


				isEventSent = true;
			EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.EnterPassword);


	
			}
		}
	}


	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.transform.root.CompareTag("Player"))
		{
			

		gameObject.GetComponent<Collider>().enabled = false;

		Debug.Log("Move inside henLab");
		Invoke("CloseDoor",2f);
		EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.PlayerisOnTheLab);
		GetComponent<Collider>().enabled = false;
		}
	}
}