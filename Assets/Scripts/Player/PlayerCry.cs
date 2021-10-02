using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCry : MonoBehaviour
{
    [Header("Cry amount")]
    [SerializeField] private float cryRecoverRate = 0.01f;
    [SerializeField] private float cryConsumeRate = 0.01f;
    private float cryAmount = 1.0f;
    [SerializeField] private GameEvent<float> onCryEvent;

    [Header("Instantiation")]

    [SerializeField] private GameObject tearPrefab;
    [SerializeField] private float tearRate = 30f;
    private bool isCrying = false;
    private float waitedTime = 0;

    [Header("Cry flying")]
    private Rigidbody2D rb;
    [SerializeField] private float cryThrust = 5f;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(isCrying && cryAmount > 0)
        {
            if(waitedTime > 1 / tearRate)
            {
                Instantiate(tearPrefab, transform.position, transform.rotation);
                waitedTime = 0;
            }
            else
            {
                waitedTime += Time.deltaTime;
            }
            cryAmount -= Time.deltaTime * cryConsumeRate;
            onCryEvent.Raise(cryAmount);
        }
        else if (!isCrying && cryAmount < 1.0f)
        {
            cryAmount += Time.deltaTime * cryRecoverRate;
            onCryEvent.Raise(cryAmount);
        }
    }

    private void FixedUpdate() 
    {
        if(isCrying && cryAmount > 0)
        {
            rb.AddForce(transform.up * cryThrust);
        }
        
    }

    public void StartCrying()
    {
        isCrying = true;
    }

    public void StopCrying()
    {
        isCrying = false;
    }
}
