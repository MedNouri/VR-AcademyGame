using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//======= Copyright (c) VRACADEMY 2018 , All rights reserved. ===============

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]
	public class Key : MonoBehaviour
	{
		private KeySoundController _keySoundController;


		private LoginManger _loginManger;
		private Rigidbody _rigidbody;
		private bool _keyPressed = false;
		public Color PressedKeycapColor;
		public Color KeycapColor;
		private Renderer _meshRenderer;
 

	    private Transform InitialPosition;

		private Text _keyCapText;
		private Vector3 _initialLocalPosition;
		private Quaternion _initialLocalRotation;
		private Vector3 _constrainedPosition;
		private Quaternion _constrainedRotation;
 
		private bool _checkForButton = true;
		private const float DistanceToBePressed = 0.03f;
		private const float KeyBounceBackMultiplier = 1500f;
		 
		private float _currentDistance = -1;

		void Start()
		{

			_meshRenderer = gameObject.GetComponent<Renderer>();
			_keyCapText = gameObject.GetComponentInChildren<Text>();
			KeycapColor = gameObject.GetComponent<Renderer>().material.color;
		 

			InitialPosition = new GameObject(string.Format("[{0}] initialPosition", this.gameObject.name)).transform;
			InitialPosition.parent = transform.parent;
			InitialPosition.localPosition = Vector3.zero;
			InitialPosition.localRotation = Quaternion.identity;

			if (_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody>();
			}

			_initialLocalPosition = transform.localPosition;
			_initialLocalRotation = transform.localRotation;

			_constrainedPosition = _initialLocalPosition;
			_constrainedRotation = _initialLocalRotation;
		
		}

		void FixedUpdate()
		{
			ConstrainPosition();
			_currentDistance = Vector3.Distance(transform.position, InitialPosition.position);

			Vector3 PositionDelta = InitialPosition.position - transform.position;
	       _rigidbody.velocity = PositionDelta * KeyBounceBackMultiplier * Time.deltaTime;
		}

		void Update()
		{
			if (_checkForButton)
			{
				if (_currentDistance > DistanceToBePressed)
				{
					_keyPressed = true;
					keyWasPressed();
					_checkForButton = false;

				}
			}
			else if (!_checkForButton)
			{
				if (_currentDistance < DistanceToBePressed)
				{
					_keyPressed = false;
					_checkForButton = true;
			
				}
			}
			ChangeKeyColorOnPress();
		
		}

		void LateUpdate()
		{
			ConstrainPosition();
		}

		
		void keyWasPressed(){


			LoginManger loginM = GameObject.Find("Scene Manger").GetComponent<LoginManger>();;
			string keypressed = gameObject.transform.GetChild(0).GetComponent<Text>().text;
			loginM.InputFiledGeter(keypressed);

			if(loginM ==null){

				Debug.Log ("soory cant find this gaem object ");
			}


		}
		void ChangeKeyColorOnPress()
		{
			if (_keyPressed)
			{
				_meshRenderer.material.color= PressedKeycapColor;
			}
			else
			{
					_meshRenderer.material.color= KeycapColor;
			}
		}

		void ConstrainPosition()
         		{
			_constrainedPosition.y = transform.localPosition.y;
			if (transform.localPosition.y > _initialLocalPosition.y)
			{
				_constrainedPosition.y = _initialLocalPosition.y;
			}

			transform.localPosition = _constrainedPosition;
			transform.localRotation = _constrainedRotation;
		}


	 

		public Key(KeySoundController keySoundController, LoginManger loginManger)
		{
			_keySoundController = keySoundController;
			_loginManger = loginManger;
		}

 

	}
}