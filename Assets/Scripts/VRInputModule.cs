
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Valve.VR.InteractionSystem
{
public class VRInputModule : BaseInputModule {

 //======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Makes the hand act as an input module for Unity's event system
//
//=============================================================================

 
	//-------------------------------------------------------------------------
	 
		private GameObject submitObject;

		//-------------------------------------------------
		private static InputModule _instance;
		public static InputModule instance
		{
			get
			{
				if ( _instance == null )
					_instance = GameObject.FindObjectOfType<InputModule>();

				return _instance;
			}
		}


		//-------------------------------------------------
		public override bool ShouldActivateModule()
		{
			if ( !base.ShouldActivateModule() )
				return false;

			return submitObject != null;
		}


	 
	private void OnTriggerEnter(Collider other)
	{
	 
		Debug.Log("trigger entred");
		PointerEventData pointerEventData = new PointerEventData( eventSystem );
		ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.pointerEnterHandler );
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log("trigger exit");
		 
		PointerEventData pointerEventData = new PointerEventData( eventSystem );
		pointerEventData.selectedObject = null;
		ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.pointerExitHandler );
	}
 

 
		//-------------------------------------------------
		public override void Process()
		{
			if ( submitObject )
			{
				BaseEventData data = GetBaseEventData();
				data.selectedObject = submitObject;
				ExecuteEvents.Execute( submitObject, data, ExecuteEvents.submitHandler );

				submitObject = null;
			}
		}
	}
}

 
