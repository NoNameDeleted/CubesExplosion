using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner), typeof(Exploader), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _chanse = 100f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 100f;

    private Spawner _spawner;
    private Exploader _exploader;
    private Renderer _renderer;
    private List<Rigidbody> _createdParts;

    public float ExplosionRadius => _explosionRadius;
    public float ExplosionForce => _explosionForce;

    public float Chanse
    {
        get { return _chanse; }
        private set
        {
            int minChanse = 0;
            int maxChanse = 100;

            _chanse = Mathf.Clamp(value, minChanse, maxChanse);
        }
    }

    private void Awake() 
    {
        _spawner = GetComponent<Spawner>();
        _exploader = GetComponent<Exploader>();
        _renderer = GetComponent<Renderer>();
    }

    public void Init(Vector3 scale, float chanse, float radius, float force)
    {
        float chaseMultiplier = 0.5f;
        float scaleMultiplier = 0.5f;
        float raiusMultiplier = 2;
        float forceMultiplier = 2;

        transform.localScale = scale * scaleMultiplier;
        _renderer.material.color = UnityEngine.Random.ColorHSV();
        Chanse = chanse * chaseMultiplier;
        _explosionRadius = radius * raiusMultiplier;
        _explosionForce = force * forceMultiplier;
    }

    private void OnMouseDown() 
    {
        List<Rigidbody> bodies = new List<Rigidbody>();

        Destroy(gameObject);

        if (UnityEngine.Random.Range(0, 101) < Chanse)
        {
            List<Cube> parts = _spawner.SpawnParts(this);

            foreach (Cube part in parts)
            {
                bodies.Add(part.GetComponent<Rigidbody>());
            }

           _exploader.Exploid(bodies);
        }
        else
        {
            Collider[] collidersAround = Physics.OverlapSphere(transform.position, _explosionRadius);

            foreach (Collider collider in collidersAround)
            {
                if (collider.TryGetComponent<Rigidbody>(out Rigidbody body) && collider.gameObject != this.gameObject)
                {
                    bodies.Add(body);
                }
            }

            _exploader.Exploid(bodies, ExplosionForce, ExplosionRadius);
        }
    }
}
