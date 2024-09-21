using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class GameExit : MonoBehaviour
	{
		public Canvas ConfirmQuitCanvasGroup;


		private void Start()
		{
			ConfirmQuitCanvasGroup.enabled = false;
		}

		
		
		public void DoConfirmQuitYes()
		{
 
			Application.Quit();
		}

		
		public void DoConfirmQuitNo()
		{
			SteamVR_LoadLevel.Begin("Main Menu");
		}



		private void ShowExitCanves()
		{
			ConfirmQuitCanvasGroup.enabled = true;
				ConfirmQuitCanvasGroup.transform.position=Player.instance.hmdTransform.position;
			
		}
		
		void Update()
		{
			for (int i = 0; i < Player.instance.handCount; i++)
			{
				Hand hand = Player.instance.GetHand(i);

				if (hand.controller != null)
				{
					if (hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu))
					{
						ShowExitCanves();
					}


				}
			}
		}


		
		
		
		
	}
}