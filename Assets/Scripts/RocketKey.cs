using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class RocketKey : MonoBehaviour
	{
		[Header( "Events " )]
		public List<EventRelayFirstLevel.EventMessageType> EventHanded = new List<EventRelayFirstLevel.EventMessageType>();
	 
		private void OnDisable()
		{

			EventRelayFirstLevel.OnEventAction -= HandleEvent;
		}


private Rigidbody rb;

		private Vector3 oldPosition;
		private Quaternion oldRotation;
		private bool attached;
		private bool isActionMade;
		public GameObject DustEffect;

		private bool KeyInPlace;


		private void Start()
		{
			rb = GetComponent<Rigidbody>();
		}

		private void OnEnable()
		{

			EventRelayFirstLevel.OnEventAction += HandleEvent;
			oldPosition = transform.position;
			oldRotation = transform.rotation;
		}

		string HandleEvent(EventRelayFirstLevel.EventMessageType type)

		{

			if (EventHanded.Contains(type))
			{
				switch (type)
				{
					case EventRelayFirstLevel.EventMessageType.KeyFound:
						KeyInPlace = true;
			 
						rb.isKinematic = true;
						rb.useGravity = false;
						rb.detectCollisions = false;
				break;
				}
			}

			return "";
		}

		private void Update()
		{
			

			if ((!attached)&&(!KeyInPlace))
			{
				if (Vector3.Distance(oldPosition, transform.position) > 1.5)
				{

					if (!isActionMade)
					{
						isActionMade = true;
						StartCoroutine(coroutineA());
						ResetToSpawn();
					
					}

					}
			}



		}

		private void HandAttachedUpdate(Hand hand)
		{

			attached = true;

		}



		private void OnDetachedFromHand(Hand hand)
		{
			attached = false;
		}


		void ResetToSpawn()
		{



 
 
			transform.position = oldPosition;
			transform.rotation = oldRotation;



		}


		IEnumerator coroutineA()
		{


			yield return new WaitForSeconds(5);

			GameObject Dust = Instantiate(DustEffect, transform.position, transform.rotation);

			yield return new WaitForSeconds(2);
			Destroy(Dust);
			isActionMade = false;
		}


	}
}