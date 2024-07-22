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
}
