using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        // Rotar el objeto sobre su propio eje
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
