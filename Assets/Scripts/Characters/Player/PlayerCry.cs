using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerCry : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> onCryStateChanged;
    [Header("Cry amount")]
    [SerializeField] private float cryRecoverRate = 0.01f;
    [SerializeField] private float cryConsumeRate = 0.01f;
    private float cryAmount = 1.0f;
    [SerializeField] private GameEvent<float> onCryEvent;

    [Header("Instantiation")]
    [SerializeField] private Transform pointLeft;
    [SerializeField] private Transform pointRight;
    [SerializeField] private float initialTearSpeed = 3f;
    [SerializeField] private GameObject tearPrefab;
    [SerializeField] private float tearRate = 30f;
    private bool isCrying = true;
    private bool isCryingInput = false;
    private float waitedTime = 0;

    [Header("Cry flying")]
    private Rigidbody2D rb;
    [SerializeField] private float cryThrust = 5f;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();

        onCryStateChanged.Invoke(isCrying);
    }
    private void Update()
    {
        if(isCrying && isCryingInput && cryAmount > 0)
        {
            if(waitedTime > 1 / tearRate)
            {
                GameObject leftTear = Instantiate(tearPrefab, pointLeft.position, transform.rotation, null);
                GameObject rightTear = Instantiate(tearPrefab, pointRight.position, transform.rotation, null);

                leftTear.GetComponent<Rigidbody2D>().velocity = pointLeft.right * initialTearSpeed;
                rightTear.GetComponent<Rigidbody2D>().velocity = pointRight.right * initialTearSpeed;

                waitedTime = 0;
            }
            else
            {
                waitedTime += Time.deltaTime;
            }
            cryAmount -= Time.deltaTime * cryConsumeRate;
            onCryEvent.Raise(cryAmount);

            if(cryAmount <= 0)
            {
                isCrying = false;
                onCryStateChanged.Invoke(isCrying);
            }
        }
        else if (!isCrying && cryAmount < 1.0f)
        {
            isCryingInput = false;
            cryAmount += Time.deltaTime * cryRecoverRate;
            onCryEvent.Raise(cryAmount);

            if(cryAmount >= 1f)
            {
                isCrying = true;
                onCryStateChanged.Invoke(isCrying);
            }
        }
    }

    private void FixedUpdate() 
    {
        if(isCrying && isCryingInput && cryAmount > 0)
        {
            rb.AddForce(transform.up * cryThrust);
        }
        
    }

    public void SetCrying(bool cry)
    {
        isCryingInput = cry;
    }
}
