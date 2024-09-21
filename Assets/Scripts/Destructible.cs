using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class Destructible : MonoBehaviour
{
	private Vector3 _oldPostion;
	private Quaternion _oldRotation;
	public float DameConstant;
	public   GameObject DestoyedVersionPrefab;
	private GameObject _DestoyedVersionPrefabgameObject;
	public GameObject ParticleRestoreffects;
     private  GameObject _ParticleRestoreffectsgameObject;
	public bool Restored=false;


	private void Awake()
	{
 _oldPostion = transform.position;
		_oldRotation = transform.rotation;
	}

	public  void Destrcut()
	{
		if (_DestoyedVersionPrefabgameObject == null)
		{
			_DestoyedVersionPrefabgameObject = Instantiate(DestoyedVersionPrefab, transform.position, transform.rotation);
			Destroy(_DestoyedVersionPrefabgameObject,4f);
		}

		if (Restored)
			{
				// hdide the Object Game object 
				gameObject.GetComponent<Renderer>().enabled = false;
				StartCoroutine(Restorobject());

			}
			else
			{
				// Destroy the game object 
				Destroy(gameObject);
			}

	 
	}


	
	
	
	

	IEnumerator Restorobject()
	{
 
		yield return new  WaitForSeconds(5);
		Destroy(_DestoyedVersionPrefabgameObject);
		if(_ParticleRestoreffectsgameObject==null){
		_ParticleRestoreffectsgameObject =	Instantiate(ParticleRestoreffects, _oldPostion, _oldRotation);
		}
		yield return new  WaitForSeconds(2);
		 DestroyImmediate(_ParticleRestoreffectsgameObject);
		
	
		transform.position = _oldPostion;
		transform.rotation = _oldRotation; 
		gameObject.GetComponent<Renderer>().enabled = true;
		
	}
}
  
