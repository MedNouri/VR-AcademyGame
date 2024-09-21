using UnityEngine;
using System.Collections;
using System.IO;


public class SimpleObject : MonoBehaviour {


		public string aStringObject = "test";
	public float aFloatValue = 43.2f;
	
	public void WriteObjectState(BinaryWriter binaryWriter) {
		//Get all the subObjects that are children of this object.
		SimpleSubObject[] subObjects = this.transform.GetComponentsInChildren<SimpleSubObject>();
		//Write out how many objects there are, so we know how many to read in later
		binaryWriter.Write(subObjects.Length);

		//Each object is responsible for writing its own state.
		foreach(SimpleSubObject subObject in subObjects) {
			subObject.WriteObjectState(binaryWriter);
			Debug.Log("Simlpe  Object Saved");
		}

		//Now write our own state
		binaryWriter.Write(aStringObject);
		binaryWriter.Write(aFloatValue);

		binaryWriter.Write(this.gameObject.name);
	}

	public void ReadObjectState(BinaryReader binaryReader) {
		//Get the subObjects count
		int simpleSubCount = binaryReader.ReadInt32();
		for(int subCount = 0; subCount < simpleSubCount; subCount++) {
			GameObject simpleSub = new GameObject();
			SimpleSubObject simpleSubScript = simpleSub.AddComponent<SimpleSubObject>();
			simpleSubScript.ReadObjectState(binaryReader);
			simpleSub.transform.parent = this.transform;
		}

		this.aStringObject = binaryReader.ReadString();
		this.aFloatValue = binaryReader.ReadSingle();

		this.gameObject.name = binaryReader.ReadString();

	}

 
}
