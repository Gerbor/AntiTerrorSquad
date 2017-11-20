using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFlare : MonoBehaviour {

    public float delay = 0.03f;
	private void Start () {
        Destroy(transform.gameObject, delay);
	}
}
