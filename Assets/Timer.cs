using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; 
    private float elapsedTime; 

    void Update()
    {
        // Update elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Update timer text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
