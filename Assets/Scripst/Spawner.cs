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

    public List<GameObject> SpawnParts(GameObject template)
    {
        int minCubeParts = 2;
        int maxCubeParts = 6;
        float scaleMultiplier = 0.5f;
        float chaseMultiplier = 0.5f;

        List<GameObject> parts = new List<GameObject>();

        for (int i = 0; i < UnityEngine.Random.Range(minCubeParts, maxCubeParts + 1); i++)
        {
            Vector3 position = transform.position + UnityEngine.Random.insideUnitSphere;
            GameObject newObject = GameObject.Instantiate(template, position, new Quaternion());
            
            newObject.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
            newObject.transform.localScale = transform.localScale * scaleMultiplier;
            newObject.GetComponent<Spawner>().Chanse = _chanse * chaseMultiplier;

            parts.Add(newObject);
        }

        return parts;
    }
}