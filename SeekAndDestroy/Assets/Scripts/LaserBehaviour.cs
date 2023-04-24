using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    public float OnscreenDelay;
    public float laserSpeed;

    void Start()
    {
        Destroy(this.gameObject, OnscreenDelay);
    }

    private void Update()
    {
        transform.position += transform.right * laserSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level" || other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}