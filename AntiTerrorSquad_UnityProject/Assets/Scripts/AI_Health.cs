using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Health : MonoBehaviour {

    public int maxHealth;
    public int health;
    private bool dead;
    public Rigidbody[] rigidBodies;
    private Animator anim;
    public GameObject testCube;

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

    public void TakeHit(int i, RaycastHit hit, float force)
    {
        health -= i;
        if(health <= 0)
        {
            if (!dead)
            {
                dead = true;
                // anim.SetTrigger("Dead");
                // StartCoroutine(WaitTillDying());

                Dead(hit.point, force);
            }
        }
    }

    /*
    IEnumerator WaitTillDying()
    {
        yield return new WaitForSeconds(1.35f);
        Dead(empty);
    }
    */

    private void Dead(Vector3 v, float force)
    {
        Destroy(gameObject.GetComponent<Animator>());
        transform.GetComponent<BoxCollider>().enabled = false;
        for (int i2 = 0; i2 < rigidBodies.Length; i2++)
        {
            rigidBodies[i2].isKinematic = false;
            rigidBodies[i2].useGravity = true;
            rigidBodies[i2].AddExplosionForce(force, v, 1f);
        }
    }
}
