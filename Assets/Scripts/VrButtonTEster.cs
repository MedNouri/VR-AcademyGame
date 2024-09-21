using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR.InteractionSystem;

[CustomEditor(typeof(VRButton))]
public class VrButtonTEster : Editor {
 
 
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
        
		VRButton Vrbutton = (VRButton)target;
		if (GUILayout.Button("click tester "))
		{
		 Vrbutton.Testbutton();
			
		}
	}
}
 
