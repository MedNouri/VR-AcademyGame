using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
public class VrHand : MonoBehaviour {
	private Animator _animatro;
		private   MeshRenderer mesh ;
		public Material highLightMaterial;
		public bool fireHapticsOnHightlight = true;

		private Hand hand;

		private MeshRenderer bodyMeshRenderer;
		private MeshRenderer trackingHatMeshRenderer;
		private SteamVR_RenderModel renderModel;
		private bool renderModelLoaded = false;

		SteamVR_Events.Action renderModelLoadedAction;

		void OnEnable()
		{
//			renderModelLoadedAction.enabled = true;
		}


		//-------------------------------------------------
		void OnDisable()
		{
//			renderModelLoadedAction.enabled = false;
		}


	void Start()
	{
	
			_animatro = gameObject.GetComponent<Animator> ();
	}

	void Update()
	{
		for ( int i = 0; i < Player.instance.handCount; i++ )
		{
			Hand hand = Player.instance.GetHand( i );

			if ( hand.controller != null )
			{
				if ( hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger ) )
				{
				
					Debug.Log ("Trigger is Up ");
					_animatro.SetBool ("Trigger", true);
				
				
				}

				if ( hand.controller.GetPressUp( Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger ) )
				{
						_animatro.SetBool ("Trigger", false);
					Debug.Log ("Trigger is Down ");
				}


			}
		}

	}




}


}