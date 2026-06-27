using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;

    public List<Collider2D> detectedcolliders = new List<Collider2D>();
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
            detectedcolliders.Add(collision);
            Debug.Log("Player detected");
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      
            detectedcolliders.Remove(collision);
            Debug.Log("Player out of range");

        if (detectedcolliders.Count == 0)
        {
            noCollidersRemain.Invoke();
            Debug.Log("No colliders remain");
        }
        else if (detectedcolliders.Count == 1)
        {
            noCollidersRemain.Invoke();
            Debug.Log("No colliders remain");
        }


    }


}