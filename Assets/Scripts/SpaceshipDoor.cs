using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(BoxCollider))]
public class SpaceshipDoor : MonoBehaviour
{
	[Header( "Events " )]
	public List<EventRelayFirstLevel.EventMessageType> EventHanded = new List<EventRelayFirstLevel.EventMessageType>();
	private Animator _animator;
 

	private AudioSource _audioSource;

	public GameObject[] ObjectsToDestroy;
	private void OnEnable()
	{

		EventRelayFirstLevel.OnEventAction += HandleEvent;
	}



	private void OnDisable()
	{

		EventRelayFirstLevel.OnEventAction -= HandleEvent;
	}


	
	

	private void DestroyObjects()
	{
		foreach (var VARIABLE in ObjectsToDestroy)
		{
			Destroy(VARIABLE);
		}
		
	}

	
	void Start()
	{

		if (_animator==null)
		{
		
			_animator = GetComponent<Animator>();

		}

		if (_audioSource==null)
		{
			_audioSource = GetComponent<AudioSource>();
			_audioSource.playOnAwake = false;
			_audioSource.loop = false;
			
		}
		
	}




	string HandleEvent(EventRelayFirstLevel.EventMessageType type)

	{

		if (EventHanded.Contains(type))
		{
			switch (type)
			{
				case EventRelayFirstLevel.EventMessageType.OpenSpaceship:
					OpenDoor();
					_audioSource.Play();
					break;
				case EventRelayFirstLevel.EventMessageType.ClosepaceShip:
					_audioSource.Play();
					CloseDoor();
					break;
			}
		}

		return null;
	}

	private void  OpenDoor()
	{
		if (_animator)
		{
		_animator.SetBool("Open",true);
		}
	}
	private void  CloseDoor()
	{
		if (_animator)
		{
			_animator.SetBool("Open",false);
		}
	}


private  IEnumerator PlayerIsOnSpaceShip()
	{
		yield return new WaitForSeconds(2f);
		EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.ValidTask);
		yield return new WaitForSeconds(1f);
		EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.ClosepaceShip);
		yield return new WaitForSeconds(2f);
		EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.LookForKey);
		DestroyObjects();
	}


	private void OnTriggerExit(Collider other)
	{
 
		if (other.gameObject.transform.root.CompareTag("Player"))
		{
			// close the door 

			StartCoroutine(PlayerIsOnSpaceShip());
			gameObject.GetComponent<Collider>().enabled = false;

			gameObject.GetComponent<Rigidbody>().isKinematic = true;
		
		}

		{
			
		}
	}
}
