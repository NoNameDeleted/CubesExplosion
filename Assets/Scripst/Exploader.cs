using System.Collections.Generic;
using UnityEngine;

public class Exploader : MonoBehaviour
{
    [SerializeField] private float _radius = 20f;
    [SerializeField] private float _force = 500f;
    [SerializeField] private float _upwardsModifier = 3f;

    public void Exploid(List<Rigidbody> parts)
    {
        foreach (Rigidbody part in parts)
        {
            part.AddExplosionForce(_force, transform.position, _radius, _upwardsModifier);
        }
    }

    public void Exploid(List<Rigidbody> parts, float force, float radius)
    {
        foreach (Rigidbody part in parts)
        {
            Debug.Log(part.position);
            float distance = Vector3.Distance(part.position, transform.position);
            part.AddExplosionForce(force / distance, transform.position, radius, _upwardsModifier);
        }
    }
}
