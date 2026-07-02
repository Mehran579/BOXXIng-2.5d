using System.Collections;
using UnityEngine;

public class hitdetector : MonoBehaviour
{
    bool justgothit;
    Animator animator;
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((!justgothit))
        {
            justgothit = true;
            if (collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Animator>().SetTrigger("knockback");
                Debug.Log("punch 1 or 2 have same knockback meow");   
            }
            StartCoroutine(hitcooldown());
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Animator>().SetTrigger("knockback");
            }
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("punch3") && collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("knockback3");
            Debug.Log("pucnh3 knockback");
        }
    }
    IEnumerator hitcooldown()
    {
        yield return new WaitForSeconds(1f);
        justgothit = false;
    }
}
