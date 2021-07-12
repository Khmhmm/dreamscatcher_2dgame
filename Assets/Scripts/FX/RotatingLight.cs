using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingLight : MonoBehaviour
{
    public float fastRotationSpeed = 95.1f;
    public float rotationSpeed = 0.1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float rotY = this.transform.rotation.eulerAngles[1];
        if (rotY > 75f && rotY < 285f) {
            this.transform.Rotate(0f, fastRotationSpeed, 0f, Space.World);
        }
        else {
            this.transform.Rotate(0f, rotationSpeed, 0f, Space.World);
        }
    }
}
