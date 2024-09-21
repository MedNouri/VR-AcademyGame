using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class RobotManger : MonoBehaviour
{
	public delegate void EvenFillThForm();

	public static event EvenFillThForm FillTheFormorder;
     
	Renderer m_Renderer;
 
	private AudioSource audio;
	public AudioClip helloHumain;
	public AudioClip FillTheForm;
	
	public Texture RoboTTalk1;
	public Texture RoboTTalk2;
	public Texture RoboTTalk3;
	public Texture RoboTTalk4;
	public Texture RoboTTalkidle;

	private bool istalking;
	
	private void OnEnable()
	{
		RobotPathTween.OnRobotgetinPostion +=startRobot ;
	}
	private void OnDestroy()
	{
		RobotPathTween.OnRobotgetinPostion -=startRobot ;
	}

 
	private void OnDisable()
	{
	
		RobotPathTween.OnRobotgetinPostion -=startRobot ;
	}

	void startRobot()
	{
		print("start robot " + Time.time);
	
		audio.Play();
		StartCoroutine( WaitFortheEnd(audio.clip.length));
	
	}


	private void Start()
	{
		istalking = true;
		m_Renderer = GetComponent<Renderer>();
		audio = GetComponent<AudioSource>();
		audio.clip = helloHumain;
	}

	private IEnumerator WaitFortheEnd(float waitTime)
	{
		float TotalwaitTime = waitTime + 2f;
		yield return new WaitForSeconds(TotalwaitTime);
			print("WaitAndPrint " + Time.time);
		audio.clip = FillTheForm;
		audio.Play();
		FillTheFormorder();
	}



	IEnumerator  talk()
	{

		while (istalking)
		{
			m_Renderer.material.SetTexture("_MainTex", RoboTTalk1);
			  yield return new WaitForSeconds(0.5f); 
			m_Renderer.material.SetTexture("_MainTex", RoboTTalk2);
			yield return new WaitForSeconds(0.5f); 
			m_Renderer.material.SetTexture("_MainTex", RoboTTalk3);
			yield return new WaitForSeconds(0.5f); 
			m_Renderer.material.SetTexture("_MainTex", RoboTTalk4);
			yield return new WaitForSeconds(0.5f); 
		}
		
	}

	private void Update()
	{
		 
	}
}
