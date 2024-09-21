using UnityEngine;
using System.Collections;
using UnityEditor;
using Valve.VR.InteractionSystem;

[CustomEditor(typeof(ComputerMouse))]
public class  buttonTester  : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
        
		ComputerMouse m0yScript = (ComputerMouse)target;
		if (GUILayout.Button("click Mosue "))
		{
//			m0yScript.ClickMouse();
			
		}
	}
}