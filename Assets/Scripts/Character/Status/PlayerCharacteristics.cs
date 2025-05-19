using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


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
    [SerializeField] Slider _playerHealthSlider;
    [SerializeField] Slider _playerEnergySlider;

    private PlayerState _playerState;

    private float _energyRecoverTimer = 0f;
    private const float _energyRecoverInterval = 0.1f;




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
        


    }



    public void UpdatePlayerEnergyBar()
    {



    }



    private void Awake()
    {
        
        _playerState = GetComponent<PlayerState>();

    }



    private void Start()
    {

        PlayerCurrentHealth = PlayerMaxHealth;
        PlayerCurrentEnergy = PlayerMaxEnergy;

    }



    private void Update()
    {

        RecoverEnergy();

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



    private void PlayerDies()
    {

        gameObject.SetActive(false);
        Debug.Log(" Игрок умер :( ");

    }

}
