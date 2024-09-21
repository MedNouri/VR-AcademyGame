using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	
	
	public class ExitMenu : MonoBehaviour
	{
		public Canvas ConfirmQuitCanvasGroup;
	

		private void Start()
		{
			ConfirmQuitCanvasGroup.enabled = false;
		}


		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
				if ( hand.currentAttachedObject != gameObject )
				{
					ConfirmQuitCanvasGroup.enabled = true;
				}
			 
			}
		}
		



	
	
	public void DoConfirmQuitYes()
	{
 
	Application.Quit();
	}

		public void DoConfirmQuitNo()
		{
			ConfirmQuitCanvasGroup.enabled = false;
		}


	}

}