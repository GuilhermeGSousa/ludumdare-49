using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerMovementBehaviour playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerCry playerCry;

    public void OnMovement(InputAction.CallbackContext value)
    {
        float inputMovement = value.ReadValue<float>();

        playerMovement.SetMovementSpeed(inputMovement);
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if(value.performed) playerCry.SetCrying(true);

        if(value.canceled) playerCry.SetCrying(false);
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if(value.performed) 
        {
            playerMovement.Jump();
        }
    }

    private void ProcessAttack()
    {
        
    }

    public void OnDamage(float damage)
    {
        playerHealth.ApplyDamage(damage);
    }

    public void onCryStateChanged(bool cryState)
    {
        if(cryState)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
