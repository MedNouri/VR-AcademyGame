using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;
 

[RequireComponent(typeof(AudioSource))]
public abstract class  Robot : MonoBehaviour
{

 protected readonly float RoborSpeed = 10f;
	private bool _isPerformingFaceAction;
	/// <summary>
	/// Robot  Talking Animation
	/// </summary>
	/// <param name="Robot Talking ANimation ">Attempt to change texture</param>
	private bool _isMoving;
	[Tooltip(
		"The default events for the control. This parameter is deprecated and will be removed in a future version of VRTK.")]
	[SerializeField]
	private  Texture _roboTTalk1;
	[SerializeField]
	private Texture _roboTTalk2;
	[SerializeField]
	private   Texture _roboTTalk3;
	[SerializeField]
	private Texture _roboTTalk4;
	[SerializeField]
	private   Texture _roboTTalkidle;
	[SerializeField]
	private   Texture _roboThappyT;
	[SerializeField]
	private   Texture _roboTAngeryT;
	[SerializeField]
	private   Texture _roboTvalidT;
	[SerializeField]
	private   Texture _robotScared;
	[SerializeField]
	private  GameObject _robotDiaply;
	private Renderer _mRenderer;
	[SerializeField]
	public GameObject Target;

	public AudioClip ValidTaskSound;
	public GameObject  CollisionEffect;
 

	private bool _expresion;
	private float _amplitude = 0.008f;
	private float _frequency = 0.5f;
     
 

	private AudioSource _audioSource;
 
// drawing The Player Pos to follow 
	private void OnDrawGizmos()
	{
		if(Target!=null){
		Gizmos.DrawLine(this.transform.position,Target.transform.position);
		Gizmos.color=Color.red;
		}
	}

	private void Awake()
	{
		if ( _mRenderer == null )
		{
			_mRenderer = _robotDiaply.gameObject.GetComponent<Renderer>();
		}
		if ( _audioSource== null )
		{
			_audioSource = GetComponent<AudioSource>();
			 
		}

	}

	private void Start()
	{
		_isMoving = false;




	}



	protected  void ValidTask()
	{
		StartCoroutine(ValidEnumerator());
	}

protected void Happy()
	{
		
		StartCoroutine(HappyEnumerator());
	}

	
	protected void Scared()
	{
		
		StartCoroutine((ScaredEnumerator()));
	}
	protected void StayScared()
	{
		_isPerformingFaceAction = true;
		SetTexture(_robotScared); 
	}
	protected IEnumerator ScaredEnumerator()
	{
		_isPerformingFaceAction = true;
		SetTexture(_robotScared);
		yield return new WaitForSeconds(5f);
		_isPerformingFaceAction = false;
	 
		
	}
	
	public delegate void AudioCallback();
	
	public void PlaySoundWithCallback(AudioClip clip, AudioCallback callback)
	{
		_audioSource.PlayOneShot(clip);
		StartCoroutine(DelayedCallback(clip.length, callback));
	}
	private IEnumerator DelayedCallback(float time, AudioCallback callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}

	//-------------------------------------------------
		public void Disable()
		{
			gameObject.SetActive( false );
		}

 
	private IEnumerator  CollistionEffectEnumerator()
	{
		GameObject effect = Instantiate (CollisionEffect, transform.position, transform.rotation);
	 
		yield return new WaitForSeconds (1);
		Destroy (effect );
	}
  

	 

	 protected IEnumerator  Gethurt()
	 {
		 _isPerformingFaceAction = true;
     	SetTexture(_roboTAngeryT);
		 StartCoroutine(CollistionEffectEnumerator());
		yield return new WaitForSeconds(2f);
		 _isPerformingFaceAction = false;
	 Idle();
	
		 
	 }

	protected IEnumerator	HappyEnumerator()
	{
		
		_isPerformingFaceAction = true;
		SetTexture(_roboThappyT);
		yield return new WaitForSeconds(5f);
		_isPerformingFaceAction = false;
		Idle();
		
	}


	protected IEnumerator	ValidEnumerator()
	{
		_audioSource.clip = null;
		_audioSource.clip = ValidTaskSound;
		_audioSource.Play();
		_isPerformingFaceAction = true;
		SetTexture(_roboTvalidT);
		yield return new WaitForSeconds(4f);
		_isPerformingFaceAction = false;
		Idle();
		
	}

	protected void Idle()
	{
		
		SetTexture(_roboTTalkidle);
	}

	private void OnCollisionEnter(Collision other)
	{
		StartCoroutine( Gethurt());
	}
	
	
	
	public bool IsMoving
	{
		get{ return _isMoving; }
	}

	private Vector3 _curPos;
	private Vector3 _lastPos;



	void LookToPlayer()
	{
		if (!_isMoving)
		{
 
			Vector3 lookPos = Player.instance.hmdTransform.position - transform.position;
			lookPos.y = 0;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime * 8f);
			
		 
		}
		else
		{
			LookForward();
		}
		
		
	}

	private void  LookForward()
	{
		 
		Quaternion rotation = Quaternion.LookRotation(transform.forward);
		transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime * 8f);

		
	}

	private  bool _isPerformingTalk;
	private void Update()
	{
		
     	_curPos = transform.position;
		if(_curPos == _lastPos)
		{
			_isMoving = false;
			LookToPlayer();
		}
		else
		{
			_isMoving = true;
		}

		_lastPos = _curPos;
	
	 
		if (((_audioSource.isPlaying)&&(!_isPerformingFaceAction)))
		{
			if (!_isPerformingTalk)
			{
				_isPerformingTalk = true;
		    	StartCoroutine(RobotTalking());
			}
			}
		else if(!_isPerformingFaceAction)
		{
		 Idle();
		}
	}

 

	
	

	protected void PlaySoundOnshoot(AudioClip audioClip)
	{
		 
		_audioSource.clip = null;
		_audioSource.clip = audioClip;
		_audioSource.Play();

	}

	IEnumerator RobotTalking()
	{

		while (_audioSource.isPlaying)
		{
			
		
		_mRenderer.material.SetTexture("_MainTex", _roboTTalk1);
		yield return new WaitForSeconds(0.1f);
 
	
		_mRenderer.material.SetTexture("_MainTex", _roboTTalk2);
		yield return new WaitForSeconds(0.1f);	
 
 


			_mRenderer.material.SetTexture("_MainTex", _roboTTalk3);
			yield return new WaitForSeconds(0.1f);
	 

	 
			_mRenderer.material.SetTexture("_MainTex", _roboTTalk4);
			yield return new WaitForSeconds(0.1f);
		}

		_isPerformingTalk = false;


	}

	void SetTexture(Texture texture)
	{
		if (_mRenderer)
		{
			_mRenderer.material.SetTexture("_MainTex", texture);
		}
		else
		{
			Debug.Log("texture cant be set");
		}
		
	}

 
}
