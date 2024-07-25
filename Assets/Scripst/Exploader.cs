using System.Collections.Generic;
using UnityEngine;

public class Exploader : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _force = 100f;
    [SerializeField] private float _upwardsModifier = 3f;

    public float Radius => _radius;
    public float Force => _force;

    public void Init(Exploader exploader)
    {
        float raiusMultiplier = 2;
        float forceMultiplier = 2;

        _radius = exploader.Radius * raiusMultiplier;
        _force = exploader.Force * forceMultiplier;
    }

    public void Exploid(List<Rigidbody> parts)
    {
        foreach (Rigidbody part in parts)
        {
            float distance = Vector3.Distance(part.position, transform.position);
            part.AddExplosionForce(_force / distance, transform.position, _radius, _upwardsModifier);
        }
    }
}
