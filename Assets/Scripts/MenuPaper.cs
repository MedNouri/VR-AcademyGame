using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class MenuPaper : MonoBehaviour
	
	
	
	{
		
		
		
		private Vector3 startPosition;
		private Quaternion startRotation;
		
		
		
		private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & ( ~Hand.AttachmentFlags.SnapOnAttach ) & ( ~Hand.AttachmentFlags.DetachOthers );

		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
				if ( hand.currentAttachedObject != gameObject )
				{
					 
					startPosition = transform.position;
					startRotation = transform.rotation;

				 
					hand.HoverLock( GetComponent<Interactable>() );

 
					hand.AttachObject( gameObject, attachmentFlags );
				}
				else
				{
		 
					hand.DetachObject( gameObject );

		 
					hand.HoverUnlock( GetComponent<Interactable>() );

					// Restore position/rotation
					transform.position = startPosition;
					transform.rotation = startRotation;
				}
			}
		}

	}
}