using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdolls : MonoBehaviour {

    public static Ragdolls instance;
    public GameObject[] characterRagdolls;

    private void Awake()
    {
        instance = this;
    }
}
