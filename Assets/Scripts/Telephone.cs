using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class Telephone : MonoBehaviour
	{

		public AudioSource _audio;


		private void Start()
		{
	 InvokeRepeating("Ring",1,60);
		}
		
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
					StopRing();
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

		private void Ring()
		{

			StartCoroutine(RingIEnumerator());
		}

		private	IEnumerator RingIEnumerator()
		{	_audio.volume = 0.5f;
			Debug.Log("GridLayout.CellLayout phone");
			yield return  new WaitForSeconds(4f);
			if (_audio!=null)
			{	 
				_audio.Play();
		     yield return  new WaitForSeconds(2f);
				_audio.Stop();
				yield return  new WaitForSeconds(5f);	
				_audio.Play();
				yield return  new WaitForSeconds(2f);
				_audio.Stop();
			}
			
 
			
		}

		private void StopRing()
		{
			Debug.Log("Stop rinign");
			_audio.volume = 0f;
		}

	}
}