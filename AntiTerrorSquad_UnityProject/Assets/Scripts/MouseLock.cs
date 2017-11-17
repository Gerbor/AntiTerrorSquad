using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour {

    public static MouseLock instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CheckLock(true);
    }

    public void CheckLock(bool useLock)
    {
        if (useLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
