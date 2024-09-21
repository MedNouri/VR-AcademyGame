using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem
{
 
	public class Gun :MonoBehaviour
	{

		 
		public GameObject BuletPrefab;
		public Transform BuletSpawnPoint;
		private Vector3 oldPosition;
		private float bulletSpeed = 1500f;
		private float bulletLife = 2f;

 
		public GameObject DustEffect;
		private bool _isActionMade;

		private GameObject bulletClone;
		
		
		
		
		
		
		private int lengthOfLineRenderer = 10;
		private LineRenderer _lineRenderer;
		public  AudioClip AudioClipLaser;
	 
		private AudioSource _audio;
		public Color c1 = Color.yellow;
		public Color c2 = Color.red;
		
		
		
		
		
		private void Start()
		{
			oldPosition = transform.position;
			
			
			_lineRenderer   = GetComponent<LineRenderer>();
			_lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
	 
			_lineRenderer.positionCount = lengthOfLineRenderer;


			float alpha = 1.0f;
			Gradient gradient = new Gradient();
			gradient.SetKeys(
				new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
				new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
			);


			_lineRenderer.colorGradient = gradient;
			
		}

		private void HandAttachedUpdate( Hand hand )
		{
			 

			if (hand.GetStandardInteractionButtonDown() ||
				((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip)))
			{


				hand.controller.TriggerHapticPulse( 400 );
			 
				Shoot();

			}
	
 
			

		}


		
  
		// Update is called once per frame
		void Update()
		{


 

			if (_lineRenderer.enabled)
			{
				_lineRenderer.SetPosition(0, BuletSpawnPoint.transform.position);
				_lineRenderer.SetPosition(1,bulletClone.transform.position );

			}
			_lineRenderer.SetVertexCount(2);
		 
			 
		}


 

		private void Shoot()
		{
	  bulletClone= Instantiate(BuletPrefab, BuletSpawnPoint.transform.position, BuletSpawnPoint.transform.rotation) as GameObject;
		 
			Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
			rb.AddForce(BuletSpawnPoint.transform.forward * bulletSpeed);
			StartCoroutine(showLaser());
			Destroy(bulletClone,bulletLife);
			 ;
			
		}
 

		IEnumerator coroutineRestor()
		{
	
 
			yield return new WaitForSeconds(5);
	
			GameObject Dust = Instantiate (DustEffect, transform.position, transform.rotation);
 
			yield return new WaitForSeconds (2);
			Destroy (Dust);
			transform.position = oldPosition;
			_isActionMade = false;
			Debug.Log("done");
		}
		IEnumerator showLaser()
		
		{	
			_lineRenderer.enabled = true;
			
			
			
			yield return new WaitForSeconds(0.2f);
			if (!bulletClone)
			{
				
			
			_lineRenderer.enabled = false;
			}
			else
			{
					
			
				yield return new WaitForSeconds(0.1f);
					
				_lineRenderer.enabled = false;
			}
		}
		
		
	}
}