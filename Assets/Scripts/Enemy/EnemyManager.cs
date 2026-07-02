using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Animator animator;
    CharacterController controller;
    public GameObject player;
    public Collider righthand;
    public Collider lefthand;
    public Collider head;
    bool busy = true;
    private void Start()
    {
        righthand.enabled = false;
        lefthand.enabled = false;
        head.enabled = false;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        animator.SetTrigger("roar");
    }
    private void Update()
    {
        if (busy) return;
        decide();
        
    }
    void decide()
    {
        facetowardsplayer();
        if (Vector3.Distance(transform.position, player.transform.position) <= 1f)
        {
            punch();
        }
        else
        {
            int choice = UnityEngine.Random.Range(0, 3);
            switch (choice)
            {
                case 0:
                    walktowardsplayer();
                    break;
                case 1:
                    jumpattack();
                    break;
                case 2:
                    dashattack();
                    break;
            }
        }
    }
    void punch()
    {
        busy = true;
        animator.SetTrigger("punch");
    }
    void jumpattack()
    {
        busy = true;
        animator.SetTrigger("jumppunch");
    }
    void dashattack()
    {
        busy = true;
        animator.SetTrigger("dash");
    }
    void walktowardsplayer()
    {
        Debug.Log("walking towards player");
        busy = true;
        animator.SetBool("walk",true);
        StartCoroutine(walktowardsplayercoroutine());
    }
    IEnumerator walktowardsplayercoroutine()
    {
        float duration = UnityEngine.Random.Range(0.5f, 2f);

        while (duration > 0)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 1f)
                break;
            Vector3 dir = (player.transform.position - transform.position).normalized;
            controller.Move(dir * Time.deltaTime);

            duration -= Time.deltaTime;

            yield return null;
        }
        animator.SetBool("walk", false);
        busy = false;
    }
    public void actionfinished()
    {
        StartCoroutine(thinkdelay());
    }

    IEnumerator thinkdelay()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
        busy = false;
    }
    void facetowardsplayer()
    {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
            scale.z = -Mathf.Abs(scale.z);
        else
            scale.z = Mathf.Abs(scale.z);

        transform.localScale = scale;
    }
    void LateUpdate()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            0f
        );
    }
    public void enablerighthand()
    {
        righthand.enabled = true;
    }
    public void disablerighthand()
    {
        righthand.enabled = false;
    }
    public void enablelefthand()
    {
        lefthand.enabled = true;
    }
    public void disablerlefthand()
    {
        lefthand.enabled = false;
    }
    public void enablehead()
    {
        head.enabled = true;
    }
    public void disablehead()
    {
        head.enabled = false;
    }
}
