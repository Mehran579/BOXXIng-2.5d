using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public CharacterController controller;
    public Animator animator;
    public Vector3 move;
    public float speed;
    public float jump;
    public float gravity = -9.81f;
    public Vector2 input;
    float verticalvelocity;
    bool facingright = true;
    bool isturning;
    bool cantmove;
    public bool knockedback;
    public bool canjump = true;
    public bool candodge = true;
    public void Start()
    {
        controller = GetComponent<CharacterController>();
        animator= GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)      //reads the right left movement
    {
        input = context.ReadValue<Vector2>();
        move = new Vector3(speed * input.x, 0, 0);
        if (context.performed)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }
    public void OnJump(InputAction.CallbackContext context)       //triggers after getting jump input
    {
        if (context.performed && controller.isGrounded && canjump && candodge)
        {
            animator.ResetTrigger("dodge");
            animator.SetTrigger("jump");                        
            canjump = false;
        }
    }
    IEnumerator jumpdelay()
    {
        yield return new WaitForSeconds(0.5f);
        canjump = true;
    }
    public void launchplayer()              //actaul jump after animation wind up
    {
        verticalvelocity = Mathf.Sqrt(jump * -2f * gravity);
        StartCoroutine(jumpdelay());   

    }
    public void OnTurn(InputAction.CallbackContext context)
    {
        if (isturning) return;
        if (context.performed)
        {
            if (context.control.name== "d")
            {
                if (!facingright)
                {
                    StartCoroutine(Turn());
                }
            }else if (context.control.name == "a")
            {
                if ((facingright))
                {
                    StartCoroutine(Turn());
                }
            }
        }
    }
    IEnumerator Turn()
    {
        isturning = true;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, 180f, 0f);

        float elapsed = 0f;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 0.5f;

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            yield return null;
        }
        transform.rotation = targetRotation;
        isturning = false;
        facingright = !facingright;
    }
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!canjump) return;
        if (context.performed && candodge)
        {
            animator.ResetTrigger("jump");
            animator.ResetTrigger("punch1");
            animator.ResetTrigger("punch2");
            animator.ResetTrigger("punch3");
            animator.SetTrigger("dodge");
            cantmove = true;
            StartCoroutine(candodgedelay());
        }
    }
    IEnumerator candodgedelay()
    {
        candodge = false;
        yield return new WaitForSeconds(1);
        candodge = true;
    }
    public void enablemovement()
    {
        cantmove = false;
    }
    public void disablemovement()
    {
        cantmove = true;
    }
    public void enableknockback()
    {
        cantmove = true;
        knockedback = true;
    }
    public void disableknockback()
    {
        cantmove = false;
        knockedback = false;
    }
    public void Update()
    {
        if (cantmove) return;
        if (controller.isGrounded && verticalvelocity <0)
        {
            verticalvelocity = -2f;            //keeping player on the ground
        } 
        else
        {
            verticalvelocity += gravity * Time.deltaTime;             //lowering velocity when the player is jumping
        }
        move.y = verticalvelocity;
        controller.Move(move * Time.deltaTime);         //final movement
    }
    void LateUpdate()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            0f
        );
    }
}
