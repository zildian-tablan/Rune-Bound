using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class MapPointMessage : MonoBehaviour
{
    public GameObject messageText; // Assign the text UI GameObject in Inspector
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("Trigger Entered by: " + other.name);
        if (!hasTriggered && other.CompareTag("Player")) // Ensure it's the player
        {
            Debug.Log("Triggered by player!");
            hasTriggered = true;
            StartCoroutine(ShowMessage());
        }
    }

    private IEnumerator ShowMessage()
    {
        messageText.SetActive(true);
        yield return new WaitForSeconds(3f);
        messageText.SetActive(false);
    }
}
