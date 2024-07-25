using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class Spawner : MonoBehaviour
{
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
            newCube.Init(template);

            parts.Add(newCube);
        }

        return parts;
    }
}