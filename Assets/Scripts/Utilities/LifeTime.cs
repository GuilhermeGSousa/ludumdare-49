using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float lifeTime = 20f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
