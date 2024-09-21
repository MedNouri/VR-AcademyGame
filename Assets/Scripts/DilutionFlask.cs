using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DilutionFlask : MonoBehaviour
{
    
    
    public ParticleSystem SmokeEffect;
    public Color StartColor;
    public Color EndColor;
    float ElapsedTime = 0.0f;
    float TotalTime = 105.0f;
    private bool _canStart;
    private bool _isOnRightPos;


    private void OnEnable()
    {
        SmokeEffect.Stop();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.name=="Water")
        {
            _isOnRightPos = true;
    print("im in water ");
        if (! _canStart)

        {
       
            _canStart = true;
            Debug.Log("Starting Infestation!");
        
            StartCoroutine(LerpColor());
            StartCoroutine(ScaleOverTime( duration));
        }
        
       
        }
     
   
    }
   
    IEnumerator ScaleOverTime(float time)
    {
        while (_isOnRightPos)
        {
            
        
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(0.1146927f, 0.004727064f, 0.1088611f);
        
        
        Vector3 destinationPos = new Vector3(-0.06f, -6.11f, 0.01f);
        Vector3 originalpos = transform.localPosition;
        float currentTime = 0.0f;

        do
        {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            transform.localPosition = Vector3.Lerp(transform.localPosition, destinationPos, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
        }
    }

    

    float duration = 15; // This will be your time in seconds.
    float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother. Really it's the time between updates.

    private bool isACtionMade;

    IEnumerator LerpColor()
    {
        

            float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
            float increment = smoothness / duration; //The amount of change to apply.
            while ((progress < 1)&&(!isACtionMade))
            {
                GetComponent<Renderer>().material.color = Color.Lerp(StartColor, EndColor, progress);
                progress += increment;
                yield return new WaitForSeconds(smoothness);
            }

            print("ok i'm Done");
            EndDiution();
         
            yield return null;
   
 
    }
 
 

 
    private  void EndDiution()
    {
     
          
        EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.DilutionEnd);
        isACtionMade = true;
    }


    public void Evaporation()
    {
        Debug.Log("Action Will Start");
        StartCoroutine(EvaporationAction());
        
    }

    IEnumerator EvaporationAction()
    {

        if (isACtionMade)
        {
            yield return  new  WaitForSeconds(5f);
            SmokeEffect.Play();
            yield return  new  WaitForSeconds(5f);
            StartCoroutine(DestroyOverTime(10));
            yield return new WaitForSeconds(10f);
            SmokeEffect.Clear();
            SmokeEffect.Stop();
        }
        else
        {
            print("Sorry i can start");
        }
        
   
    }
    
    
    IEnumerator DestroyOverTime(float time)
    {
       
            Vector3 originalScale = transform.localScale;
            Vector3 destinationScale = new Vector3(0f, 0f, 0f);
            Vector3 destinationPos = new Vector3(-0.06416439f, -7.36f, 0.009822846f);
            
            Vector3 originalpos = transform.localPosition;
            float currentTime = 0.0f;

            do
            {
                transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
                transform.localPosition = Vector3.Lerp(transform.localPosition, destinationPos, currentTime / time);
                currentTime += Time.deltaTime;
                yield return null;
            } while (currentTime <= time);
        
        EventRelayLevel2.RelayEvent(EventRelayLevel2.EventMessageType.BoilEnd);
        Destroy(gameObject);
    }
 


}
