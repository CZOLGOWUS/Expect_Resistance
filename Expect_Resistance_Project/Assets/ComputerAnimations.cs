using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ComputerAnimations : MonoBehaviour
{
    Animator animator;
    bool isHacked;
    bool isDone;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void SetHack(bool b)
    {
        if(isHacked!=b)
        {
            isHacked = b;
            animator.SetBool("IsHacked", b);
        }
    }
    public void SetDone(bool b)
    {
        if(isDone!=b)
        {
            isDone = b;
            animator.SetBool("IsDone", b);
        }
    }

}
