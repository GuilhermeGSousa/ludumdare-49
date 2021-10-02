using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour, IEventListener<float>
{
    [SerializeField] private FloatGameEvent playerHealthChangedEvent;
    [SerializeField] private GameObject heartContainerPrefab;
    List<GameObject> heartContainers;
    public int totalHearts;
    float currentHearts;
    HeartElementUI currentContainer;

    private void Awake() 
    {
        heartContainers = new List<GameObject>();
        SetupHearts(1);
    }
    
    public void SetupHearts(int hearts)
    {
        heartContainers.Clear();

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        totalHearts = hearts;
        currentHearts = (float)totalHearts;

        for (int i = 0; i < totalHearts; i++)
        {
            GameObject newHeart = Instantiate(heartContainerPrefab, transform);
            heartContainers.Add(newHeart);
            
            if(currentContainer != null)
            {
                currentContainer.next = newHeart.GetComponent<HeartElementUI>();
            }
            currentContainer = newHeart.GetComponent<HeartElementUI>();
        }
        currentContainer = heartContainers[0].GetComponent<HeartElementUI>();
    }

    public void SetHealth(float health)
    {
        Debug.Log("Setting health to " + health);
        currentHearts = health;
        currentContainer.SetHealth(health);
    }

    public void AddHearts(float health)
    {
        currentHearts += health;
        if(currentHearts > totalHearts)
        {
            currentHearts = (float)totalHearts;
        }
        currentContainer.SetHealth(currentHearts);
    }

    public void RemoveHearts(float health)
    {
        currentHearts += health;
        if(currentHearts < 0)
        {
            currentHearts = 0f;
        }
        currentContainer.SetHealth(currentHearts);
    }

    public void AddContainer()
    {
        GameObject newHeart = Instantiate(heartContainerPrefab, transform);
        currentContainer = heartContainers[heartContainers.Count - 1].GetComponent<HeartElementUI>();
        heartContainers.Add(newHeart);

        if(currentContainer)
        {
            currentContainer.next = newHeart.GetComponent<HeartElementUI>();
        }

        currentContainer = heartContainers[0].GetComponent<HeartElementUI>();

        totalHearts++;
        currentHearts = totalHearts;
    }
    
    private void OnEnable() {
        playerHealthChangedEvent.RegisterListener(this);
    }

    private void OnDisable() {
        playerHealthChangedEvent.UnregisterListener(this);
    }

    public void OnEventRaised(float parameter)
    {
        if(parameter > totalHearts)
        {
            SetupHearts(Mathf.CeilToInt(parameter));
        }
        
        SetHealth(parameter);
    }
}
