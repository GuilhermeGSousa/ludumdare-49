using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverListener : MonoBehaviour, IEventListener
{
    [SerializeField] private GameEvent onGameOver;
    [SerializeField] private SceneSwitcher sceneSwitcher;
    public void OnEventRaised()
    {
        sceneSwitcher.SwitchScene("Game Over Scene");
    }

    private void OnEnable() {
        onGameOver.RegisterListener(this);
    }

    private void OnDisable() {
        onGameOver.UnregisterListener(this);
    }
}
