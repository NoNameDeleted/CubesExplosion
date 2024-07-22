using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner), typeof(Exploader))]
public class Cube : MonoBehaviour
{
    private Spawner _spawner;
    private Exploader _exploader;
    private MeshFilter _meshFilter;
    private List<Rigidbody> _createdParts;

    private void Awake() 
    {
        _spawner = GetComponent<Spawner>();
        _exploader = GetComponent<Exploader>();
    }

    private void OnMouseDown() 
    {
        List<Rigidbody> partsBodies = new List<Rigidbody>();

        Destroy(gameObject);

        if (UnityEngine.Random.Range(0, 101) < _spawner.Chanse)
        {
            List<GameObject> parts = _spawner.SpawnParts(gameObject);

            foreach (GameObject part in parts)
            {
                partsBodies.Add(part.GetComponent<Rigidbody>());
            }

           _exploader.Exploid(partsBodies);
        }
    }
}
