using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
public class VRInputModuleUI : MonoBehaviour {
 
		public CustomEvents.UnityEventHand onHandClick;

		private Hand currentHand;

		//-------------------------------------------------
		void Awake()
		{
			VRButton button = GetComponent<VRButton>();
			if ( button )
			{
				//button.onClick.AddListener( OnButtonClick );
			}
		}


		//-------------------------------------------------
		private void OnTriggerEnter(Collider other)
		{
			InputModule.instance.HoverBegin( gameObject );
			Debug.Log("Button Trigger ENTRED ");
		}

 

		private void OnTriggerExit(Collider other)
		{
			InputModule.instance.HoverEnd( gameObject );
		 
			Debug.Log("Button  Trigger EXIT EXIT ");
		}

		//-------------------------------------------------


		private bool _isActionPerformed=true;
	 
		private void OnCollisionEnter(Collision other)
		{
			if (_isActionPerformed)
			{
				Debug.Log("Button Clicked we have A Collision");
				InputModule.instance.Submit(gameObject);
				_isActionPerformed = false;
			}
		}


		private void OnCollisionExit(Collision other)
		{
			Debug.Log("Button Clicked we have A Collison Exit ");
			_isActionPerformed = true;
		}

		private IEnumerator PlayKeySound(Transform keyTransform)
		{
		 
			yield return new WaitForSeconds (1);
		 
		}
		
	}
	
	
}
