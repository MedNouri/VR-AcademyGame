using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LunarDoor : MonoBehaviour {	
	private Animator Animator;
	private AudioSource _audioSource;
	public Collider HandCollider;
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
	}

	private bool _enginShutdown;
	string HandleEvent(MoonEventRelay.EventMessageType type)
	{

		if (EventsHandeld.Contains(type))
		{
	 
			switch (type)
			{
				case MoonEventRelay.EventMessageType.EnginShutDown:
					_enginShutdown = true;
					if (HandCollider)
					{
						HandCollider.enabled = true;
					}
				
					break;
				case MoonEventRelay.EventMessageType.DoorOpen:
					if (_enginShutdown)
					{
						OpenDoor();
					}
					else
					{
						MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.AskForStoppingTheEngin);
			 
					}
 break;
	 case MoonEventRelay.EventMessageType.AskForStoppingTheEngin:
		 isActionMadeEngin = false;
		 break;

			}

		}

		return "";
	}

public void  OpenDoor()
	{
		Animator.SetBool("Open",true);
	
		_audioSource.Play();
		StartCoroutine(RobotGetGetOut());
	}


	IEnumerator RobotGetGetOut()
	{
		yield return  new  WaitForSeconds(3.0f);
		MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.DoorIsOpen);
		
	}

 
	
	
	public LinearMapping linearMapping;
	private float _currentLinearMapping = float.NaN;
 

	private bool isActionMadeEngin;
	
	
	void Update()
	{
		if (_currentLinearMapping != linearMapping.value)
		{
			_currentLinearMapping = linearMapping.value;
			if (_enginShutdown)
			{
				
			
			if (_currentLinearMapping>0.9)
			{
				Debug.Log("Avreg event door ");
				if (!isActionMadeEngin)
				{
					Debug.Log("Avreg event door  sent ");		
					MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.DoorOpen);
					isActionMadeEngin =true;
				}
			}
		 
		}
		}
	}

	private bool isaActionDone;
	private void OnTriggerExit(Collider other)
	{
	 Debug.Log("Somting is Exit ");
		if (other.gameObject.transform.root.CompareTag("Player"))
		{
			// close the door 
			if (	isActionMadeEngin )
			{
				
			
			if (!isaActionDone)
			{
				isaActionDone = true;
			
		
			MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.PlayerIsOut);
			}
			}
		}

	 
	}
}

 
