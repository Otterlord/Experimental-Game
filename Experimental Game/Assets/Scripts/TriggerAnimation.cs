using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public Animator animator;
    private bool done = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !done)
        {
            animator.Play("reveal");
            done = true;
        }
            
    }
}
