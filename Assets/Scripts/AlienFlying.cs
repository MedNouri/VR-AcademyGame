using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;
using Random = UnityEngine.Random;

public class AlienFlying : MonoBehaviour
{

	public Material HitTexture;

	public Texture myTexture;
	private Renderer _rend;

	private int life = 3;
	public GameObject Explosion;
	public GameObject DestoyedAline;
	private NavMeshAgent agent;
 
	public GameObject Bullet;
	public Transform BuletSpawnPoint;
	private RaycastHit hit;
	private int lengthOfLineRenderer = 20;
	private LineRenderer _lineRenderer;
	public  AudioClip AudioClipLaser;
	public bool test;
	private AudioSource _audio;
	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
public  AudioClip BulletSoundeffect;

	private GameObject bulletClone;
	// Use this for initialization
	void Start ()
	{

		_rend = GetComponent<Renderer>();

		if (_audio == null)
		{
			_audio= GetComponent<AudioSource>();
		}
		_lineRenderer   = GetComponent<LineRenderer>();
		_lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		_lineRenderer.widthMultiplier = 0.1f;
		_lineRenderer.positionCount = lengthOfLineRenderer;

 
		float alpha = 1.0f;
		Gradient gradient = new Gradient();
		gradient.SetKeys(
			new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
		);
		
		
		
		
		
		
		_lineRenderer.colorGradient = gradient;
		
			agent = GetComponent<NavMeshAgent>();
	 
	     InvokeRepeating("FireBullet",1f,5f);
	
		
		
		
	}


	IEnumerator Gothit()
	{
		_rend.material = HitTexture;
   yield return new WaitForSeconds(1f);
		_rend.material.mainTexture = myTexture;
	}
 
	public void Death()
	{
		MoonEventRelay.RelayEvent(MoonEventRelay.EventMessageType.EnemiGotKilled);
		DoExplosion();
		CancelInvoke();
		GameObject destoyedAlineClone;
	    destoyedAlineClone= Instantiate(DestoyedAline, transform.position,Quaternion.identity) as GameObject;
		Destroy(destoyedAlineClone,5f);
		Destroy(gameObject);
	}

	// Update is called once per frame
	RaycastHit raycastHit ;
	void Update() {

		if (test)
		{
			Death();
		}	



		if (bulletClone)
		{
			 
			_lineRenderer.SetVertexCount(2);
			_lineRenderer.SetPosition(0, BuletSpawnPoint.transform.position);
			_lineRenderer.SetPosition(1,   	bulletClone.transform.position);
		
		}


		if (agent.isStopped)
		{
			NewDestination();
		}

		if(!agent.pathPending){

			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath ||agent.velocity.sqrMagnitude== 0)
				{
					
					NewDestination();
				
				}

				
			}
		       }	
		}

	private void NewDestination()
	{
		Vector3 newDest =Random.insideUnitSphere *    100f + Player.instance.transform.position ;
		NavMeshHit hit;
		bool hasDestination = NavMesh.SamplePosition(newDest, out hit, 100f, 1);
		
		if (hasDestination)
		{
			agent.SetDestination(hit.position);
		}
	}

	
	private void DoExplosion()
	{		
		GameObject explosionGameObject= Instantiate(Explosion, transform.position, transform.rotation);
	
	
//		_audioSource.Play();
		Destroy( explosionGameObject,2f);
	}
	

	private void OnDrawGizmos()
	{
 		//Gizmos.DrawLine(this.transform.position, goal.transform.position);
		Gizmos.color = new Color(1f, 1f, 1f);
	}

 

	private float bulletSpeed = 1000f;
	private float bulletLife = 2f;

 
	private void FireBullet()
	{
  
		StartCoroutine(showLaser());
		bulletClone= Instantiate(Bullet, BuletSpawnPoint.transform.position,BuletSpawnPoint.transform.rotation) as GameObject;
         
		Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
		rb.AddForce(BuletSpawnPoint.transform.forward * bulletSpeed);
 
	}

	IEnumerator showLaser()
	{
		_lineRenderer.enabled = true;
		yield return new WaitForSeconds(0.9f);
		_lineRenderer.enabled = false;
	}
 
	private void OnTriggerEnter(Collider other)
	{
	
	 
		if (other.gameObject.CompareTag("Bullet"))
		{
			Debug.Log("My life is"+life);
		
 	if (life==0)
			{
			Death();

			
			}
			else
			{
				life--;
				StartCoroutine(Gothit());
			}
		}
	}
}
