using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Health : MonoBehaviour {

    public int characterId;
    public int maxHealth;
    public int health;
    private bool dead;
    //private List<Rigidbody> rigidBodies = new List<Rigidbody>();
    public Rigidbody[] rigidBodies;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigidBodies.Length; i++)
        {
            rigidBodies[i].useGravity = false;
            rigidBodies[i].isKinematic = true;
        }
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeHit(int i)
    {
        health -= i;
        if(health <= 0)
        {
            if (!dead)
            {
                dead = true;
                anim.SetTrigger("Dead");
                StartCoroutine(WaitTillDying());
            }
        }
    }

    IEnumerator WaitTillDying()
    {
        yield return new WaitForSeconds(0.2f);
        Dead();
    }

    private void Dead()
    {
        Destroy(gameObject.GetComponent<Animator>());
        for (int i2 = 0; i2 < rigidBodies.Length; i2++)
        {
            rigidBodies[i2].isKinematic = false;
            rigidBodies[i2].useGravity = true;
        //    rigidBodies[i2].AddExplosionForce(3, transform.position, 2);
        }
    }
}
