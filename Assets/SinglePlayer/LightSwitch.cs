using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public bool isOn;
    [Header("Lights")] public Light[] lights;
    private bool isElectricityOn = true;
    void Start()
    {
        
    }
    void Update()
    {
        // Check if electricity is on
    }

    private void OnMouseDown()
    {
        switch (isOn)
            {
                case true:
                    foreach (Light light in lights)
                    {
                        light.enabled = false;
                    }
                    isOn = false;
                    break;
                case false:
                    foreach (Light light in lights)
                    {
                        light.enabled = true;
                    }
                    isOn = true;
                    break;
            }
    }
}