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
        if(value.performed) playerCry.StartCrying();

        if(value.canceled) playerCry.StopCrying();
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
}
