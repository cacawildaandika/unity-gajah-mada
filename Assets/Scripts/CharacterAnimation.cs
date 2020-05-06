using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimateWalk(bool animate)
    {
        animator.SetBool(AnimationParams.WALK, animate);
    }
}
