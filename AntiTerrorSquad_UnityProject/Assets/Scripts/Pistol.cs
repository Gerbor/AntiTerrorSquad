using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pistol : MonoBehaviour {

	[Serializable]
    public class PistolStats
    {
        public float range;
        public int damageOutput;
        public int magSize;
        public int maxMags;
    }

    [Serializable]
    public class PistolInfo
    {
        public int bulletCount;
        public int magCount;
    }

    [Serializable]
    public class Settings
    {
        public GameObject bulletExit;
    }

    public PistolStats pistolStats = new PistolStats();
    public PistolInfo pistolInfo = new PistolInfo();
    public Settings settings = new Settings();

    private void Awake()
    {
        pistolInfo.bulletCount = pistolStats.magSize;
        pistolInfo.magCount = pistolStats.maxMags;
    }

    private void Update()
    {
        CheckIfShot();
    }

    private void CheckIfShot()
    {
        Vector3 fwd = settings.bulletExit.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(settings.bulletExit.transform.position, fwd * pistolStats.range, Color.green);
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, fwd, out hit, pistolStats.range))
            {
                if (hit.transform.GetComponent<AI_Health>())
                {
                    print("Hit " + hit.transform.name);
                    hit.transform.GetComponent<AI_Health>().TakeHit(pistolStats.damageOutput);
                }
            }
        }
    }
}
