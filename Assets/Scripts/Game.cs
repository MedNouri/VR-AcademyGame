using System;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour {

    
    
    
    
    public static string saveFile = @"SaveFile.save";

 

    void ReadSimpleObjects() {
        if(File.Exists(saveFile)) {
            using(FileStream fs = File.OpenRead(saveFile)) {
                BinaryReader fileReader = new BinaryReader(fs);
                int simpleObjectCount = fileReader.ReadInt32();
                for(int simpleCount = 0; simpleCount < simpleObjectCount; simpleCount++) {
                    GameObject simpleObject = new GameObject();
                    SimpleObject simpleScript = simpleObject.AddComponent<SimpleObject>();
                    simpleScript.ReadObjectState(fileReader);
                }
            }
        }
    }

    public static void WriteLevelPos(string LevelName)
    {
        using (FileStream fs = File.OpenWrite(saveFile))
        {
      
            BinaryWriter fileWriter = new BinaryWriter(fs);
            fileWriter.Write(LevelName);
        }
    }


    public static String ReadPos()
    {

        string  LevelName= null;



        if (File.Exists(saveFile))
        {
            using (FileStream fs = File.OpenRead(saveFile))
            {
                BinaryReader fileReader = new BinaryReader(fs);
                LevelName = fileReader.ReadString();

            }
        }

        return LevelName;
    }

    void WriteSimpleObjects() {

        using(FileStream fs = File.OpenWrite(saveFile)) {
            SimpleObject[] simpleObjects = FindObjectsOfType<SimpleObject>();
            BinaryWriter fileWriter = new BinaryWriter(fs);
            fileWriter.Write(simpleObjects.Length);
            foreach (SimpleObject simpleObject in simpleObjects) {
                simpleObject.WriteObjectState(fileWriter);
            }
        }
    }
}
