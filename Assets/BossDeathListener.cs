using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathListener : MonoBehaviour, IEventListener
{
    [SerializeField] private GameEvent onBossDeath;
    [SerializeField] private SceneSwitcher sceneSwitcher;

    public void OnEventRaised()
    {
        sceneSwitcher.SwitchScene("End Cutscene");
    }

    private void OnEnable() {
        onBossDeath.RegisterListener(this);
    }

    private void OnDisable() {
        onBossDeath.UnregisterListener(this);
    }
}
