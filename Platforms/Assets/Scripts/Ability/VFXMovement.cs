using UnityEngine;

public class VFXMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad a la que se moverá el VFX

    void Update()
    {
        // Mover el VFX hacia adelante
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
