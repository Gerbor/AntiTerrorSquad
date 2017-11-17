using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {

    public FireWeapon[] weapons;
    private int activeWeapon;

    private void Awake()
    {
        weapons = GetComponentsInChildren<FireWeapon>();
        weapons[1].gameObject.SetActive(false);
    }

    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.GetButtonDown("1"))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetButtonDown("2"))
        {
            SwitchWeapon(1);
        }
    }

    private void SwitchWeapon(int i)
    {
        if(i != activeWeapon)
        {
            weapons[activeWeapon].gameObject.SetActive(false);
            weapons[i].gameObject.SetActive(true);
            activeWeapon = i;
        }
    }
}
