using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Valve.VR.InteractionSystem;

public class SoundSetting :  MonoBehaviour
{

   public TextMesh SoundSettingText;
    public List<AudioSource> AudioSources = new List<AudioSource>();
    public AudioMixer AudioMixer;

    public LinearMapping linearMapping;
    private float currentLinearMapping = 0;
    private float _volume = 0;
    private void Start()
    {
      _volume= PlayerPrefs.GetFloat("Volume");
        if  (_volume!=0)
        {
            SoundSettingText.text = _volume.ToString();
            foreach (var SoundSource in AudioSources)
            {
                SoundSource.volume = _volume*10;
                print("MyVoume is "+_volume);
            }

        
        }
        else
        {
            foreach (var SoundSource in AudioSources)
            {
                SoundSource.volume = 1f;
                print("Setdefault value");
                SoundSettingText.text = "100 %";
            }   
            
        }
     
    }
    void FixedUpdate()
    {
        if (currentLinearMapping != linearMapping.value)
        {

            currentLinearMapping = linearMapping.value;
			 
            foreach (var SoundSource in AudioSources)
            {
                SoundSource.volume = currentLinearMapping;
                PlayerPrefs.SetFloat("Volume", currentLinearMapping); // 4
                print("i saved  "+currentLinearMapping);
            }
            SoundSettingText.text = (currentLinearMapping).ToString();
        }
    }
    private void SoudUpdat()
    {

        foreach (var SoundSource in AudioSources)
        {
            SoundSource.volume = currentLinearMapping;
            PlayerPrefs.SetFloat("Volume", currentLinearMapping); // 4
            print("i saved  "+currentLinearMapping);
        }
    }
}
