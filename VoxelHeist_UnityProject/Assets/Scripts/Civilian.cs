using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Civilian : MonoBehaviour {

	[Serializable]
    public class CivilianStats
    {
        public float speed = 3;
        public int maxHealth = 20;
    }

    public CivilianStats civilianStats = new CivilianStats();

    private void Awake()
    {
        this.GetComponent<AI_Health>().maxHealth = civilianStats.maxHealth;
    }
}
