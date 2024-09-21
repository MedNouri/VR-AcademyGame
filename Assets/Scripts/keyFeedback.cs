 
using UnityEngine;
using System.Collections;


//======= Copyright (c) VRACADEMY 2018 , All rights reserved. ===============
 
 

namespace Valve.VR.InteractionSystem
{
 
	[RequireComponent( typeof( Interactable ) )]
	public class keyFeedback: MonoBehaviour
	{
		private TextMesh textMesh;
		private Vector3 oldPosition;
		private Quaternion oldRotation;

		private float attachTime;

		private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & ( ~Hand.AttachmentFlags.SnapOnAttach ) & ( ~Hand.AttachmentFlags.DetachOthers );

 


  
 
		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
				if ( hand.currentAttachedObject != gameObject )
				{
					// Save our position/rotation so that we can restore it when we detach
					oldPosition = transform.position;
					oldRotation = transform.rotation;

					// Call this to continue receiving HandHoverUpdate messages,
					// and prevent the hand from hovering over anything else
					hand.HoverLock( GetComponent<Interactable>() );

					// Attach this object to the hand
					hand.AttachObject( gameObject, attachmentFlags );
				}
			
			}
			else
			{
				// Detach this object from the hand
				hand.DetachObject( gameObject );

				// Call this to undo HoverLock
				hand.HoverUnlock( GetComponent<Interactable>() );

				// Restore position/rotation
				transform.position = oldPosition;
				transform.rotation = oldRotation;
			}
		}


 
 
	 
	}
 
}