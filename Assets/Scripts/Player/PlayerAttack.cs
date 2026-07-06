using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    public bool canattack = true;
    public int currentattack = 1;
    public Collider rightpunchcollider;
    public Collider leftpunchcollider;
    public PlayerMovement movement;
    public void Start()
    {
        animator = GetComponent<Animator>();
        leftpunchcollider.enabled = false;
        rightpunchcollider.enabled = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!canattack||!context.performed) return;
        if(movement.input.y == 1)
        {
            animator.SetTrigger("punch2");

        }
        else if(movement.input.y == -1)
        {
            animator.SetTrigger("punch3");
        }
        else
        {
        animator.SetTrigger("punch1");
        }
        StartCoroutine(attackcooldown());
    }
    IEnumerator attackcooldown()
    {
        canattack = false;
        yield return new WaitForSeconds(1.5f);
        canattack = true;
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
