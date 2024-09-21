using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    public GameObject PaperPrefab;
    public GameObject PaperPos;
    public AudioSource AudioSource;
    public GameObject EnPos;
    private bool _isActionMade;
 

    public void Print()
    {
        if (! _isActionMade)
        {
           
        
        StartCoroutine(PrinEnumerator());
        }
    }


    private IEnumerator PrinEnumerator()
    {
        if (!AudioSource.isPlaying)
        {
            AudioSource.Play();
        }

        yield return new WaitForSeconds(10f);

       GameObject clone = Instantiate(PaperPrefab, PaperPos.transform.position, PaperPos.transform.rotation) as GameObject;
         
        clone.transform.positionTo(9f, EnPos.transform.position);
        _isActionMade = false;
    }
 
}
