using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorScript : MonoBehaviour
{
    public Animator animator;
    public bool isOpen;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isOpen" , true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isOpen" , false);
    }

    public void OnMouseDown()
    {
        switch (isOpen)
        {
            case true:
                animator.SetBool("isOpen" , false);
                isOpen = false;
                break;
            case false:
                animator.SetBool("isOpen" , true);
                isOpen = true;
                break;
        }
    }
}
