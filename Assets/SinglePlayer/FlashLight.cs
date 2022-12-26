using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Light flashlight;
    public bool isOn;
    public AudioSource flashlightsound;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (isOn)
            {
                case true:
                    flashlightsound.Play();
                    flashlight.enabled = false;
                    isOn = false;
                    break;
                case false:
                    flashlightsound.Play();
                    flashlight.enabled = true;
                    isOn = true;
                    break;
            }
        }
    }
}