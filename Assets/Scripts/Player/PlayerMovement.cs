using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Vector3 move;
    public float speed;
    public float jump;
    public float gravity = -9.81f;
    float verticalvelocity;
    bool facingright = true;
    bool isturning;
    bool cantmove;
    public void Start()
    {
        controller = GetComponent<CharacterController>();
        animator= GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)      //reads the right left movement
    {
        Vector2 input = context.ReadValue<Vector2>();
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
        if (context.performed && controller.isGrounded)
        {
            animator.SetTrigger("jump");                        //actaul jump after animation wind up
        }
    }
    public void launchplayer()
    {
        verticalvelocity = Mathf.Sqrt(jump * -2f * gravity);

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
        if (context.performed)
        {
            animator.SetTrigger("dodge");
            cantmove = true;
        }
    }
    public void enablemovement()
    {
        cantmove = false;
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
