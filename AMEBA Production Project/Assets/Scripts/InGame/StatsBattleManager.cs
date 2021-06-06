using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBattleManager : Singleton<StatsBattleManager>
{
    public Slider temperatureSlider;
    public Slider infectionSlider;

    private const float initialTemperature = 36f;
    private const float maxTemperature = 40f;

    private const float initialInfectionLevel = 100f;

    public delegate void StatusGame();
    public static event StatusGame LostGame;
    public static event StatusGame WonGame;

    protected override void Awake()
    {
        base.Awake();

        GameManager.LevelStart += ConfigureStartGame;
    }

    private void Start()
    {
        ConfigureStartGame();
    }

    public void ConfigureStartGame()
    {
        temperatureSlider.minValue = initialTemperature;
        temperatureSlider.maxValue = maxTemperature;
        infectionSlider.minValue = 0f;
        infectionSlider.maxValue = initialInfectionLevel;

        temperatureSlider.value = initialTemperature;
        infectionSlider.value = initialInfectionLevel;
    }

    public void DamageInfection()
    {
        infectionSlider.value -= 10f;

        if (infectionSlider.value <= 0f)
        {
            infectionSlider.value = initialInfectionLevel;
            WonGame();
        }
    }

    public void IncreaseTemperature()
    {
        temperatureSlider.value += 0.5f;

        if (temperatureSlider.value >= maxTemperature)
        {
            temperatureSlider.value = initialTemperature;
            LostGame();
        }
    }
}
