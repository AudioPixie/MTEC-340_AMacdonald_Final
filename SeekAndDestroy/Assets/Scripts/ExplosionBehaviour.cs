using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public float OnscreenDelay = 2f;

    void Start()
    {
        Destroy(this.gameObject, OnscreenDelay);
    }
}
