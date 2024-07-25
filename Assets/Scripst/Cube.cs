using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner), typeof(Exploader), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _chanse = 100f;

    private Spawner _spawner;
    private Exploader _exploader;
    private Renderer _renderer;

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

    public void Init(Cube cube)
    {
        float chaseMultiplier = 0.5f;
        float scaleMultiplier = 0.5f;
        

        transform.localScale = cube.transform.localScale * scaleMultiplier;
        _renderer.material.color = UnityEngine.Random.ColorHSV();
        Chanse = cube.Chanse * chaseMultiplier;

        if (cube.TryGetComponent<Exploader>(out Exploader exploader))
            _exploader.Init(exploader);
        
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
                if (part.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                    bodies.Add(rigidbody);
            }

           _exploader.Exploid(bodies);
        }
        else
        {
            Collider[] collidersAround = Physics.OverlapSphere(transform.position, _exploader.Radius);

            foreach (Collider collider in collidersAround)
            {
                if (collider.TryGetComponent<Rigidbody>(out Rigidbody body) && collider.gameObject != this.gameObject)
                    bodies.Add(body);
            }

            _exploader.Exploid(bodies);
        }
    }
}
