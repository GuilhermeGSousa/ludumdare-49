using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameEvent<bool> gamePauseEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPause(InputAction.CallbackContext value)
    {
        if(value.performed)
        {
            SetPaused(true);       
        }

    }

    public void SetPaused(bool pause)
    {
        isPaused = pause;

        if(isPaused)
        {
            //playerInput.actions.FindActionMap("Movement").Disable();
            Time.timeScale = 0f;
        }
        else
        {
            //playerInput.actions.FindActionMap("Movement").Enable();
            Time.timeScale = 1f;
        }
        gamePauseEvent.Raise(pause);
        pausePanel.SetActive(isPaused);

    }
}
