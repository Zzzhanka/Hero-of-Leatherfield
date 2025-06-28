using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;


public class PlayerCharacteristics : MonoBehaviour
{

    [Space(5)]
    [Header("Характеристики игрока:")]

    [Space(5)]
    public int PlayerMaxHealth;
    public int PlayerCurrentHealth;
    public int PlayerDamage;
    public int PlayerDefense;

    [Space(5)]
    public float PlayerMoveSpeed;
    public float PlayerAttackReload;

    [Space(5)]
    public float PlayerMaxEnergy;
    public float PlayerCurrentEnergy;
    public float PlayerEnergyRecoveringPower;

    [Space(5)]
    [SerializeField] PlayerHealthBar _playerHealthSlider;
    [SerializeField] PlayerEnegyBar _playerEnergySlider;

    private PlayerState _playerState;

    private float _energyRecoverTimer = 0f;
    private const float _energyRecoverInterval = 0.1f;

    private float _energyLoseTimer = 0f;
    private const float _energyLoseInterval = 0.1f;
    private const float _losingEnergyWhileRunning = 1;

    public void PlayerTakesDamage(int damage)
    {

        if (_playerState.PlayerCanTakeDamage)
        {
            PlayerCurrentHealth -= damage;
            UpdatePlayerHealthBar();

            if (_playerState.PlayerCanDie && PlayerCurrentHealth <= 0)
            {
                PlayerCurrentHealth = 0;
                UpdatePlayerHealthBar();
                PlayerDies();
            }
        }

    }



    public void PlayerTakesHealth(int health)
    {

        if (_playerState.PlayerCanTakeHealh)
        {
            PlayerCurrentHealth += health;
            UpdatePlayerHealthBar();

            if (PlayerCurrentHealth > PlayerMaxHealth)
            {
                PlayerCurrentHealth = PlayerMaxHealth;
            }
        }

    }



    public void UpdatePlayerHealthBar()
    {

        _playerHealthSlider.UpdateHealthBar(PlayerMaxHealth, PlayerCurrentHealth);

    }



    public void UpdatePlayerEnergyBar()
    {

        _playerEnergySlider.UpdateEnergyBar(PlayerMaxEnergy, PlayerCurrentEnergy);

    }



    private void Awake()
    {
        
        _playerState = GetComponent<PlayerState>();

    }



    private void Start()
    {

        PlayerCurrentHealth = PlayerMaxHealth;
        PlayerCurrentEnergy = PlayerMaxEnergy;

        UpdatePlayerHealthBar();
        UpdatePlayerEnergyBar();

    }



    private void Update()
    {

        RecoverEnergy();
        LoseEnergyWhileRunning();

    }



    private void RecoverEnergy()
    {
        
        if (!_playerState.PlayerIsRunning && !_playerState.PlayerIsDashing)
        {
            _energyRecoverTimer += Time.deltaTime;

            if (_energyRecoverTimer >= _energyRecoverInterval)
            {
                _energyRecoverTimer = 0f;

                PlayerCurrentEnergy += PlayerEnergyRecoveringPower;

                if (PlayerCurrentEnergy > PlayerMaxEnergy)
                    PlayerCurrentEnergy = PlayerMaxEnergy;

                UpdatePlayerEnergyBar();
            }
        }

    }



    private void LoseEnergyWhileRunning()
    {

        while (_playerState.PlayerIsRunning)
        {
            _energyLoseTimer += Time.deltaTime;

            if (_energyLoseTimer >= _energyLoseInterval)
            {
                _energyLoseTimer = 0f;

                PlayerCurrentEnergy -= _losingEnergyWhileRunning;

                UpdatePlayerEnergyBar();
            }
        }

    }



    private void PlayerDies()
    {
        PlayerEvents.InvokePlayerDeath();
        gameObject.SetActive(false);
    }

}
