using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SimpleSubObject : MonoBehaviour {

	public string subString = "this is a sub string";
	public int valueX = 43;
	public byte valueB = 2;
	
	public void WriteObjectState(BinaryWriter binaryWriter) {
		//write our own state
		binaryWriter.Write(subString);
		binaryWriter.Write(valueX);
		binaryWriter.Write(valueB);

		binaryWriter.Write(this.gameObject.name);
		Debug.Log("Simlpe sip Object Saved");
	}

	public void ReadObjectState(BinaryReader binaryReader) {

		this.subString = binaryReader.ReadString();
		this.valueX = binaryReader.ReadInt32();
		this.valueB = binaryReader.ReadByte();
		
		this.gameObject.name = binaryReader.ReadString();
		
	}
	
 
}
