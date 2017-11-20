using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlares : MonoBehaviour {

    public static MuzzleFlares instance;
    public GameObject[] flares;

    private void Awake()
    {
        instance = this;
    }
}
