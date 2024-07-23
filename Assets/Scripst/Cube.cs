using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner), typeof(Exploader), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Spawner _spawner;
    private Exploader _exploader;
    private Renderer _renderer;
    private List<Rigidbody> _createdParts;

    private void Awake() 
    {
        _spawner = GetComponent<Spawner>();
        _exploader = GetComponent<Exploader>();
        _renderer = GetComponent<Renderer>();
    }

    public void Init(Vector3 scale, float chanse)
    {
        float chaseMultiplier = 0.5f;
        float scaleMultiplier = 0.5f;

        transform.localScale = scale * scaleMultiplier;
        _renderer.material.color = UnityEngine.Random.ColorHSV();
        _spawner.Chanse = chanse * chaseMultiplier;
    }

    private void OnMouseDown() 
    {
        List<Rigidbody> partsBodies = new List<Rigidbody>();

        Destroy(gameObject);

        if (UnityEngine.Random.Range(0, 101) < _spawner.Chanse)
        {
            List<Cube> parts = _spawner.SpawnParts(this);

            foreach (Cube part in parts)
            {
                partsBodies.Add(part.GetComponent<Rigidbody>());
            }

           _exploader.Exploid(partsBodies);
        }
    }
}
