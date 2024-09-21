using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEffect : MonoBehaviour {

	
    public float amplitude = 0.01f;
	public float frequency = 0.5f;
     
 
	Vector3 posOffset ;
	Vector3 tempPos  ;
     
	// Use this for initialization
	void Start()
	{
 
		posOffset = transform.position;
	}

 
	void Update () {
		 
			 
			tempPos = posOffset;
			tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
			transform.position = tempPos;
        
        
	 
	}
}
