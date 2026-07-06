using System.Collections;
using UnityEngine;

public class hitdetector : MonoBehaviour
{
    bool justgothit;
    Animator animator;
    public PlayerAttack playerattack;
    public GameObject hiteffects;
    public GameObject uppercuteffects;
    void Start()
    {
        GetComponent<Collider>().enabled = false;
        animator = GetComponentInParent<Animator>();
        if(transform.parent != null && transform.parent.CompareTag("Player"))
        {
            playerattack = transform.parent.GetComponent<PlayerAttack>();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((!justgothit))
        {
            justgothit = true;
            //Debug.Log("the object colliding with "+collision.gameObject.name +"the object attacking" + transform.root.gameObject);
            StartCoroutine(hitcooldown());
            if (collision.CompareTag("Enemy") && transform.root.CompareTag("Player"))       //when attacking enenmy;
            {
                collision.GetComponent<Healthmanager>().takedamamge(1);
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("punch3"))
                {
                    Instantiate(uppercuteffects, transform.position, Quaternion.identity);
                    StartCoroutine(hitstop(0.1f));
                    collision.gameObject.GetComponent<Animator>().SetTrigger("knockback3");
                }
                else
                {
                    Instantiate(hiteffects, transform.position, Quaternion.identity);
                    StartCoroutine(hitstop(0.05f));
                    collision.gameObject.GetComponent<Animator>().SetTrigger("knockback");
                }
                //Debug.Log(collision.gameObject.name + transform.parent.gameObject);
            }
            if (collision.CompareTag("Player")&& transform.root.CompareTag("Enemy"))  // when attackig player;
            {
                collision.GetComponent<Healthmanager>().takedamamge(1);
                StartCoroutine(hitstop(0.05f));
                Instantiate(hiteffects,transform.position, Quaternion.identity);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("enemy dash"))
                {
                    //Debug.Log(collision.gameObject.name + transform.parent.gameObject);           
                    collision.gameObject.GetComponent<Animator>().SetTrigger("knockback3");      //trigger the knocked behind aninmation;

                }
                else
                {
                    //Debug.Log(collision.gameObject.name + transform.parent.gameObject);
                    collision.gameObject.GetComponent<Animator>().SetTrigger("knockback");
                    
                }
            }
        }
    }
    IEnumerator hitcooldown()
    {
        yield return new WaitForSeconds(1f);
        justgothit = false;
    }
    IEnumerator hitstop(float seconds)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1f;
    }
}
