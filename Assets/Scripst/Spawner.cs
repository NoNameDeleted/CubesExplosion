using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _chanse = 100;

    public float Chanse
    {
        get { return _chanse; }
        set
        {
            int minChanse = 0;
            int maxChanse = 100;

            _chanse = Mathf.Clamp(value, minChanse, maxChanse);
        }
    }

    public List<Cube> SpawnParts(Cube template)
    {
        int minCubeParts = 2;
        int maxCubeParts = 6;
        
        List<Cube> parts = new List<Cube>();

        int partsAmount = UnityEngine.Random.Range(minCubeParts, maxCubeParts + 1);

        for (int i = 0; i < partsAmount; i++)
        {
            Vector3 position = transform.position + UnityEngine.Random.insideUnitSphere;
            Cube newCube = GameObject.Instantiate(template, position, new Quaternion());
            newCube.Init(transform.localScale, _chanse);

            parts.Add(newCube);
        }

        return parts;
    }
}