using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SmallStation : MonoBehaviour
{
 
 
    public LinearMapping linearMapping;
    private float currentLinearMapping = float.NaN;
    public AudioSource _AudioSource;
    private void Awake()
    {
        if ( linearMapping == null )
        {
            linearMapping = GetComponent<LinearMapping>();
        }
    }

 
    private void Update()
    {
        
            if ( currentLinearMapping != linearMapping.value )
            {
                currentLinearMapping = linearMapping.value;
                _AudioSource.volume = currentLinearMapping;

            }
    }
}
