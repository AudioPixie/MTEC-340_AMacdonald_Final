using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private bool Opening;
    private float openSpeed = 1.5f;

    Transform doorTransform;

    // Start is called before the first frame update
    void Start()
    {
        Opening = false;
        doorTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Opening == true)
        {
            if (doorTransform.position.x < 0 && doorTransform.position.x > -6)
            {
                doorTransform.position -= new Vector3(openSpeed * Time.deltaTime, 0, 0);
            }

            if (doorTransform.position.x > 0 && doorTransform.position.x < 6)
            {
                doorTransform.position += new Vector3(openSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    public void Open()
    {
        Opening = true;
    }
}
