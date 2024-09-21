using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]

public class SmallRobot : MonoBehaviour
	{
		private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & ( ~Hand.AttachmentFlags.SnapOnAttach ) & ( ~Hand.AttachmentFlags.DetachOthers );
		private AudioSource _audioSource;
 
		private void OnEnable()
		{
			_audioSource = GetComponent<AudioSource>();
			 
			_audioSource.playOnAwake = false;
		}

 
		
 
		private void OnAttachedToHand( Hand hand )
		{

			if (!_audioSource.isPlaying)
			{
				_audioSource.Play();
			}
		}

 
}
}