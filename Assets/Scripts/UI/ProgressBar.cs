using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour, IEventListener<float>
{
    public Slider slider;
    public Image fill;
    public Color defaultColor;
    public float criticalValue = 0.3f;

    public Color criticalColor;

    [SerializeField] private GameEvent<float> onProgressEvent;

    private void Start() {
        fill.color = defaultColor;
    }
    public void SetProgress(float percentage)
    {
        slider.value = percentage;

        if(percentage <= criticalValue) fill.color = criticalColor;
        else fill.color = defaultColor;
    }

    public void OnEventRaised(float parameter)
    {
        SetProgress(parameter);
    }

    private void OnEnable() {
        onProgressEvent.RegisterListener(this);
    }

    private void OnDisable() {
        onProgressEvent.UnregisterListener(this);
    }
}
