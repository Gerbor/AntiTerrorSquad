using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireWeapon : MonoBehaviour {

	[Serializable]
    public class WeaponStats
    {
        public float range;
        public int damageOutput;
        public int magSize;
        public int maxMags;
        public bool autoFire;
        public float delayBetweenShots;
        public float reloadTime;
        public float knockBackForce;
    }

    [Serializable]
    public class WeaponInfo
    {
        public int bulletCount;
        public int magCount;
        public bool onCoolDown;
    }

    [Serializable]
    public class Settings
    {
        public GameObject bulletExit;
    }

    public WeaponStats weaponStats = new WeaponStats();
    public WeaponInfo weaponInfo = new WeaponInfo();
    public Settings settings = new Settings();

    private void Awake()
    {
        weaponInfo.bulletCount = weaponStats.magSize;
        weaponInfo.magCount = weaponStats.maxMags;
    }

    private void Update()
    {
        CheckIfTriggered();
    }

    private void CheckIfTriggered()
    {
        if (!weaponStats.autoFire)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!weaponInfo.onCoolDown)
                {
                    UseWeapon();
                }
            }
        }
        else if (Input.GetButton("Fire1"))
        {
            if (!weaponInfo.onCoolDown)
            {
                UseWeapon();
            }
        }
    }

    private void UseWeapon()
    {
        weaponInfo.onCoolDown = true;
        StartCoroutine(WeaponCooler());

        Vector3 fwd = settings.bulletExit.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(settings.bulletExit.transform.position, fwd * weaponStats.range, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, weaponStats.range))
        {
            if (hit.transform.GetComponent<AI_Health>())
            {
            //    print("Hit " + hit.transform.name);
                hit.transform.GetComponent<AI_Health>().TakeHit(weaponStats.damageOutput, hit, weaponStats.knockBackForce);
            }
        }
    }

    private IEnumerator WeaponCooler()
    {
        yield return new WaitForSeconds(weaponStats.delayBetweenShots);
        weaponInfo.onCoolDown = false;
    }
}
