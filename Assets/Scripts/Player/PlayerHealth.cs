using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float playerMaxHealth = 10f;
    private float playerHealth;
    [SerializeField] private GameEvent<float> onPlayerHealthChanged;
    [SerializeField] private GameEvent onGameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
        onPlayerHealthChanged.Raise(playerMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ApplyDamage(float damage)
    {
        playerHealth -= damage;

        if(playerHealth <= 0f) onGameOver.Raise();
        else onPlayerHealthChanged.Raise(playerHealth);
    }
}
