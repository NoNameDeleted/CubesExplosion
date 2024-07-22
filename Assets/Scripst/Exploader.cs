using System.Collections.Generic;
using UnityEngine;

public class Exploader: MonoBehaviour
{
    [SerializeField] private int _chanse;
    [SerializeField] private float _radius = 20f;
    [SerializeField] private float _force = 500f;
    [SerializeField] private float _upwardsModifier = 3f;

    private MeshFilter _meshFilter;
    private List<Rigidbody> _createdParts;
    
    public int Chanse
    {
        get { return _chanse; }
        set
        {
            if (value < 0)
            {
                _chanse = 0;
            }
            else if (value > 100)
            {
                _chanse = 100;
            }
            else 
            {
                _chanse = value;
            }
        }
    }

    private void Awake() 
    {
        _meshFilter = GetComponent<MeshFilter>();
        _createdParts = new List<Rigidbody>();
    }

    private void OnMouseDown() 
    {
        Destroy(gameObject);

        if (UnityEngine.Random.Range(0, 101) < _chanse) 
        {
            Divide();
            Explode();
        }
    }

    private void Divide()
    {
        int minCubeParts = 2;
        int maxCubeParts = 6;
        
        for (int i = 0; i < UnityEngine.Random.Range(minCubeParts, maxCubeParts + 1); i++) 
        {
            GameObject newCube = CreateCubePart();
            _createdParts.Add(newCube.GetComponent<Rigidbody>());
        }
    }

    private void Explode()
    {
        foreach (Rigidbody cubePart in _createdParts)
        {
            cubePart.AddExplosionForce(_force, transform.position, _radius, _upwardsModifier);
        }
    }

    private GameObject CreateCubePart()
    {
        float scaleMultiplier = 0.5f;

        GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newCube.AddComponent<Rigidbody>();
        newCube.AddComponent<Exploader>();
        newCube.GetComponent<MeshFilter>().mesh = _meshFilter.mesh;
        newCube.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
        newCube.transform.localScale = transform.localScale * scaleMultiplier;
        newCube.transform.position = transform.position + UnityEngine.Random.insideUnitSphere;
        newCube.GetComponent<Exploader>().Chanse = _chanse / 2;

        return newCube;
    }
}
