using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	public class OxygenOne : MonoBehaviour
	{

	
		private Vector3 oldPosition;
		private Quaternion oldRotation;
		public Transform snapPos;
		public String Snapname;
		private float attachTime;
		private Collider[] hitColliders;

		private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) &
		                                               (~Hand.AttachmentFlags.DetachOthers);

		
			
		public delegate void OxygenInPlace();

		public static event OxygenInPlace OxygenisSnaped;
	 

		private void HandHoverUpdate(Hand hand)
		{
	

			if (hand.GetStandardInteractionButtonDown() ||
			    ((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip)))
			{
				if (hand.currentAttachedObject != gameObject)
				{

					oldPosition = transform.position;
					oldRotation = transform.rotation;


					hand.HoverLock(GetComponent<Interactable>());

					hand.AttachObject(gameObject, attachmentFlags);
				}
				else
				{
					// Detach this object from the hand
					hand.DetachObject(gameObject);

					// Call this to undo HoverLock	// Restore position/rotation
					transform.position = oldPosition;
					transform.rotation = oldRotation;
					hand.HoverUnlock(GetComponent<Interactable>());

				}

			}
		}



		private void DestroyOnTriggerEnter(Collider GameObject){




		}

		//-------------------------------------------------
		private void HandAttachedUpdate( Hand hand )
		{
			Debug.Log("Im aTTAHCED");
			
			hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);

			Debug.Log("Im hitting "+hitColliders[1]);
			if (hitColliders[1].gameObject.name == Snapname)
			{
				Debug.Log("Found Yu bitch");
				hand.DetachObject(gameObject);
				transform.position = snapPos.position;
				transform.rotation = snapPos.rotation;
				transform.transform.SetParent(hitColliders[1].transform);

				gameObject.GetComponent<Collider>().enabled = false;

				if (OxygenisSnaped != null) OxygenisSnaped();
				hand.HoverUnlock(GetComponent<Interactable>());
				 

			}
		}
			
		


	

		

	}
}