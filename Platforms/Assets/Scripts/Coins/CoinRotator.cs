using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidad de rotación

    // Update is called once per frame
    void Update()
    {
        // Rotar el objeto sobre su eje Z
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
