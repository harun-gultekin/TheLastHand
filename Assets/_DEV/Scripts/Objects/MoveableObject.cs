using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public float pushForce = 1;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody _rigg = collision.collider.attachedRigidbody;

        if (_rigg != null)
        {
            Vector3 forceDirection = collision.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            _rigg.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse);
        }
    }
}