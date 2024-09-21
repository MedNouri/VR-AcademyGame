using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]

	public class Cd : MonoBehaviour
	{



		// Cd 1
		private Vector3 _oldPosition;
		private Quaternion _oldRotation;
	 

		public GameObject ParticleRestoreffectsgameObject;
	
		 
	 

		private void OnEnable()
		{

 
		_oldPosition = transform.position;
		_oldRotation = transform.rotation;
	     }




		private void CheckPostion()
		{
			
			if (Vector3.Distance(_oldPosition, transform.position) > 1.5)
			{
				 
				 
					StartCoroutine(Restorobject());
				 
			}
		}
		
		
 
  


 
	private void OnDetachedFromHand( Hand hand )
	{
		 Debug.Log("hand detatched DForm cd");
		Invoke("CheckPostion",3f);
		
	}
 
 
 
		IEnumerator Restorobject()
		{
 
			yield return new  WaitForSeconds(2);

			if (ParticleRestoreffectsgameObject)
			{
				ParticleRestoreffectsgameObject =	Instantiate(ParticleRestoreffectsgameObject, _oldPosition , _oldRotation); 
			}

				
		 
			yield return new  WaitForSeconds(2);
			DestroyImmediate(ParticleRestoreffectsgameObject);
			transform.position = _oldPosition ;
			transform.rotation = _oldRotation;
		 

		}
 


}
}
