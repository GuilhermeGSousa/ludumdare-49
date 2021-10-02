using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public void InstantiatePrefab()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
