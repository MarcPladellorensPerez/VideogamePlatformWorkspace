using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        // Rotar el objeto alrededor del eje Y constantemente
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
