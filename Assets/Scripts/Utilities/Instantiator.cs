using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float instantiateRate = 30f;
    private bool canInstantiate = false;
    
    private float waitedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canInstantiate)
        {
            if(waitedTime > 1 / instantiateRate)
            {
                Instantiate(prefab, transform.position, transform.rotation);
                waitedTime = 0;
            }
            else
            {
                waitedTime += Time.deltaTime;
            }
            
        }
    }

    public void StartInstantiate()
    {
        canInstantiate = true;
    }

    public void StopInstantiate()
    {
        canInstantiate = false;
    }
}
