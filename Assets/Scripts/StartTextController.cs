using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartTextController : MonoBehaviour
{
    public TextMeshProUGUI levelDisplay;
	public float duration;
	private float startTime;
    
    // Start is called before the first frame update
    void Start()
    {
        levelDisplay.enabled = true;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > duration) 
        	levelDisplay.enabled = false;
    }
}
