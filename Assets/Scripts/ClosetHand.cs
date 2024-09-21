using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//======= Copyright (c) VRACADEMY 2018 , All rights reserved. ===============



namespace Valve.VR.InteractionSystem
{
	public class ClosetHand : MonoBehaviour
	{

		private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & ( ~Hand.AttachmentFlags.SnapOnAttach ) & ( ~Hand.AttachmentFlags.DetachOthers );





		private void HandHoverUpdate(Hand hand)
		{
			if (hand.GetStandardInteractionButtonDown() ||
			    ((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip)))
			{
				if (hand.currentAttachedObject != gameObject)
				{
					print("press is down ");
					hand.HoverLock(GetComponent<Interactable>());

					// Attach this object to the hand
					hand.AttachObject(gameObject, attachmentFlags);
				}
				else
				{

					hand.DetachObject(gameObject);

					hand.HoverUnlock(GetComponent<Interactable>());

				}
			}
				if (hand.GetStandardInteractionButtonUp()||
				    ((hand.controller != null) && hand.controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_Grip))  )
				{
					print("press is out ");
					hand.DetachObject(gameObject);

					hand.HoverUnlock(GetComponent<Interactable>());
		

				}
			
		}


 
		
	}
}