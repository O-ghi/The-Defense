using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimationEvent : MonoBehaviour
{
    Animator animator;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void Impact()
    {
        animator.SetTrigger("Impact");
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }
}
