using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    public Animator animator;

    public void SetBoolFalse()
    {
        animator.SetBool("IsInteract", false);
    }
}
