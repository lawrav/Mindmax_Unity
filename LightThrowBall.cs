using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LightThrowBall : MonoBehaviour
{
    [Header("Configuración de físicas")]
    public float mass = 0.5f;
    public float linearDamping = 0.05f;
    public float angularDamping = 0.05f;
    public float extraThrowForce = 1.5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Configurar propiedades físicas "ligeras"
        rb.mass = mass;
        rb.drag = linearDamping;
        rb.angularDrag = angularDamping;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void Throw(Vector3 direction, float force)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(direction.normalized * force * extraThrowForce, ForceMode.Impulse);
    }
}
