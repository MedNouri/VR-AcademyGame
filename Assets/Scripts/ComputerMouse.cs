using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting.APIUpdating;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class ComputerMouse : MonoBehaviour
	{
		
		public Transform BottomPosition;
		public UnityEvent onTouchpadDown;
		public UnityEvent onTouchpadUp;
		public Transform TopPosition;
		public Transform RighPosition;
		public Transform LeftPosition;
		public LinearMapping linearMapping;
		public bool repositionGameObject = true;
		public bool maintainMomemntum = true;
		public float momemtumDampenRate = 5.0f;

		private float initialMappingOffset;
		private int numMappingChangeSamples = 5;
		private float[] mappingChangeSamples;
		private float prevMapping = 0.0f;
		private float mappingChangeRate;
		private int sampleCount = 0;


		//-------------------------------------------------
		void Awake()
		{
			mappingChangeSamples = new float[numMappingChangeSamples];
		}


		//-------------------------------------------------
		void Start()
		{
		 
			if ( linearMapping == null )
			{
				linearMapping = GetComponent<LinearMapping>();
			}

			if ( linearMapping == null )
			{
				linearMapping = gameObject.AddComponent<LinearMapping>();
			}

            initialMappingOffset = linearMapping.value;

			if ( repositionGameObject )
			{
				UpdateLinearMapping( transform );
			}
		}


		//-------------------------------------------------
		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() )
			{
				hand.HoverLock( GetComponent<Interactable>() );

				initialMappingOffset = linearMapping.value - CalculateLinearMapping( hand.transform );
				sampleCount = 0;
				mappingChangeRate = 0.0f;
			}

			if ( hand.GetStandardInteractionButtonUp() )
			{
				hand.HoverUnlock( GetComponent<Interactable>() );

				CalculateMappingChangeRate();
			}

			if ( hand.GetStandardInteractionButton() )
			{
				UpdateLinearMapping( hand.transform );
			}

			if ( hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
			{
				onTouchpadDown.Invoke();
			}

			if ( hand.controller.GetPressUp( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
			{
				onTouchpadUp.Invoke();
			}


		}
 
		//-------------------------------------------------
		private void CalculateMappingChangeRate()
		{
			//Compute the mapping change rate
			mappingChangeRate = 0.0f;
			int mappingSamplesCount = Mathf.Min( sampleCount, mappingChangeSamples.Length );
			if ( mappingSamplesCount != 0 )
			{
				for ( int i = 0; i < mappingSamplesCount; ++i )
				{
					mappingChangeRate += mappingChangeSamples[i];
				}
				mappingChangeRate /= mappingSamplesCount;
			}
		}


		//-------------------------------------------------
		private void UpdateLinearMapping( Transform tr )
		{
			prevMapping = linearMapping.value;
			linearMapping.value = Mathf.Clamp01( initialMappingOffset + CalculateLinearMapping( tr ) );

			mappingChangeSamples[sampleCount % mappingChangeSamples.Length] = ( 1.0f / Time.deltaTime ) * ( linearMapping.value - prevMapping );
			sampleCount++;

			if ( repositionGameObject )
			{
				//	transform.position = new Vector3(Mathf.Lerp(TopPosition.transform.position.x, RighPosition.transform.position.x, linearMapping.value), 0, Mathf.Lerp(RighPosition.transform.position.x, LeftPosition.transform.position.x, linearMapping.value));
			transform.position = Vector3.Lerp( BottomPosition.position, TopPosition.position, linearMapping.value );
				 //	transform.position = new Vector3(Mathf.Clamp(Top, bottom, linearMapping.value), 0, Mathf.Clamp(left, right, linearMapping.value));
			}
		}

		private void OnHandHoverBegin(Hand hand)
		{
			MenuEventRelay.RelayEvent(
				MenuEventRelay.EventMessageType.Lostfocus);

		}


		//-------------------------------------------------
		private float CalculateLinearMapping( Transform tr )
		{
			Vector3 direction = TopPosition.position - BottomPosition.position;
			float length = direction.magnitude;
			direction.Normalize();

			Vector3 displacement = tr.position - BottomPosition.position;

			return Vector3.Dot( displacement, direction ) / length;
		}

		public bool istatchUp;
		public bool istatchdowns;
		//-------------------------------------------------
		void Update()
		{
			if (istatchUp)
			{
				onTouchpadUp.Invoke();
			}

			if (istatchdowns)
			{
				onTouchpadDown.Invoke();
			}
			
			if ( maintainMomemntum && mappingChangeRate != 0.0f )
			{
				//Dampen the mapping change rate and apply it to the mapping
				mappingChangeRate = Mathf.Lerp( mappingChangeRate, 0.0f, momemtumDampenRate * Time.deltaTime );
				linearMapping.value = Mathf.Clamp01( linearMapping.value + ( mappingChangeRate * Time.deltaTime ) );

				if ( repositionGameObject )
				{
			    transform.position = Vector3.Lerp( BottomPosition.position, TopPosition.position, linearMapping.value );
				 
				//	transform.position = new Vector3( Vector3.Lerp( BottomPosition.position, TopPosition.position, linearMapping.value ), 0, transform.position.z);
				}
			}
		}
	}
		
	}
		






 
 