using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    bool cancombo;
    bool canattack = true;
    int currentattack = 1;
    public Collider rightpunchcollider;
    public Collider leftpunchcollider;
    public void Start()
    {
        animator = GetComponent<Animator>();
        leftpunchcollider.enabled = false;
        rightpunchcollider.enabled = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (cancombo)
        {
            currentattack++;

            if (currentattack > 3)
                currentattack = 3;

            animator.SetTrigger("punch" + currentattack);
            cancombo = false;
        }
        else if (canattack)
        {
            currentattack = 1;
            canattack = false;
            animator.SetTrigger("punch1");
        }
    }
    public void enablecombo()
    {
        cancombo = true;
    }

    public void endattack()
    {
        canattack = true;
        cancombo = false;
        rightpunchcollider.enabled = false;
        leftpunchcollider.enabled = false;

        if (currentattack >= 3)
            currentattack = 0;
    }
    public void enablerightpunchcollider()
    {
        rightpunchcollider.enabled = true;
    }
    public void disablerightpunchcollider()
    {
        rightpunchcollider.enabled = false;
    }
    public void enableleftpunchcollider()
    {
        leftpunchcollider.enabled = true;
    }
    public void disableleftpunchcollider()
    {
        leftpunchcollider.enabled = false;
    }
}
