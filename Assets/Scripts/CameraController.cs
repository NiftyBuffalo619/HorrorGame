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
    void Start()
    {
        PlayUI.enabled = false;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
    }

    public void MoveToFilm()
    {
        animator.Play("FilmMoveCamera");
        MainUI.enabled = false;
        PlayUI.enabled = true;
    }

    public void MoveBackFromFilm()
    {
       // animator.Play("FilmMoveBack");
       MainUI.enabled = true;
       PlayUI.enabled = false;
    }
    public void MoveToDoor()
    {
        animator.Play("ExitMoveCamera");
    }

    public void MoveBackFromDoor()
    {
        
    }
}
