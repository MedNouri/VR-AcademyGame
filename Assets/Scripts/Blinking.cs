using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour {

   MeshRenderer flashingText;
 
	void Start(){
	 
		flashingText = GetComponent<MeshRenderer>();
	 
		StartCoroutine(BlinkText());
	}
 
	//function to blink the text
	public IEnumerator BlinkText(){
	 
		while(true){
			//set the Text's text to blank
			flashingText.enabled = false;
 
			yield return new  WaitForSeconds(0.7f);
		 
			flashingText.enabled = true;
			yield return new  WaitForSeconds(0.7f);
		}
	}
}
