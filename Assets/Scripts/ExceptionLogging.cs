using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExceptionLogging : MonoBehaviour
{

	public String saveFile = "@VrAcademyLog.txt";
	private StringWriter logWriter;
	
	
	
	private void OnEnable()
	
	
	{
		Application.RegisterLogCallback(ExceptionWriter);
	}


	private void OnDisable()
	{
		Application.RegisterLogCallback(null);
	}




	void ExceptionWriter(string LogString, String stackTrace,LogType type)
	{
		switch (type)
		{
			case LogType.Exception:
			case LogType.Error:
			case LogType.Warning:
				using (StreamWriter writer = new StreamWriter(new FileStream(saveFile, FileMode.Append)))
				{
					writer.WriteLine(type);
                    writer.WriteLine(LogString);
                    writer.WriteLine(stackTrace);
                    writer.WriteLine(Time.captureFramerate);
                    writer.WriteLine(Time.deltaTime);
				}


				break;

			default:
				break;
		}


	}
	
}
