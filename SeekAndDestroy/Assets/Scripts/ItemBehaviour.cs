using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public int xRotationSpeed = 100;
    public int yRotationSpeed = 50;
    public int zRotationSpeed = 25;

    public GameObject Inner;
    Transform ItemTransform;

    void Start()
    {
        ItemTransform = Inner.GetComponent<Transform>();
    }

    void Update()
    {
        ItemTransform.Rotate(
            xRotationSpeed * Time.deltaTime,
            yRotationSpeed * Time.deltaTime,
            zRotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
