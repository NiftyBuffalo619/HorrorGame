using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] public AnimatorController Controller;
    [SerializeField] public AnimationClip[] Animations;
    [SerializeField] public AnimationClip ExitAnimation;
    [SerializeField] public Canvas MainUI;
    [SerializeField] public Canvas PlayUI;
    [SerializeField] public Canvas ExitUI;
    void Start()
    {
        PlayUI.enabled = false;
        ExitUI.enabled = false;
        animator = GetComponent<Animator>();
        Debug.Log("UI Successfully started");
    }
    void Update()
    {
        
    }

    public void MoveToFilm()
    {
        animator.Play("FilmMoveCamera");
        MainUI.enabled = false;
        PlayUI.enabled = true;
        Debug.Log("Moved to film");
    }

    public void MoveBackFromFilm()
    {
       animator.Play("MoveBackFromFilm");
       MainUI.enabled = true;
       PlayUI.enabled = false;
       Debug.Log("Moved back from film");
    }
    public void MoveToDoor()
    {
        animator.Play("ExitMoveCamera");
        MainUI.enabled = false;
        ExitUI.enabled = true;
    }

    public void MoveBackFromDoor()
    {
        MainUI.enabled = true;
        ExitUI.enabled = false;
        animator.Play("MoveBackFromDoor");
    }
}
