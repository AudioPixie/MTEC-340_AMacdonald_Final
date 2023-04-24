using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam : MonoBehaviour
{
    public float yRotationSpeed = 10f;

    Transform CamTransform;

    void Start()
    {
        CamTransform = GetComponent<Transform>();
        CamTransform.rotation = Quaternion.Euler(-4, 0, 7);
    }

    void Update()
    {
        CamTransform.Rotate(0f, yRotationSpeed * Time.deltaTime, 0f);
    }
}
