using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	const string saveFile = @"SaveFile.save";
 

	
	
public 	void WritePlayerName(string name ) {
		//using statement will dispose of the object inside when we're done using it.
		//This is important for objects like files, that we don't want to leave open.
		using(FileStream fs = File.OpenWrite(saveFile)) {
	
			BinaryWriter fileWriter = new BinaryWriter(fs);
			if (name!= null)
			{
				fileWriter.Write(name);
			}
		
	 
		}
	}

	public static string DisplayPlayerName()
	{
 
		string PlayerName = null;



		if (File.Exists(saveFile))
		{
			using (FileStream fs = File.OpenRead(saveFile))
			{
				BinaryReader fileReader = new BinaryReader(fs);
				PlayerName = fileReader.ReadString();
			 
			}
		}

		return PlayerName;
	}


	public bool  CheckData()
	{
		String DATA;
		using (FileStream fs = File.OpenRead(saveFile))
		{
			BinaryReader fileReader = new BinaryReader(fs);
			DATA = fileReader.ReadString();

			if (DATA!=null)
			{
				return true;
			}
		}

		return false;
	}


}
