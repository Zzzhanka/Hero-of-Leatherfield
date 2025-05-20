using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class EnemyFollow : MonoBehaviour
{

    [SerializeField] private float _enemySpeed;

    private PlayerCharacteristics _playerChars;
    private GameObject _playerGameObject;




    private void Start()
    {

        _playerGameObject = GameObject.FindWithTag("Player");
        _playerChars = _playerGameObject.GetComponent<PlayerCharacteristics>();
        
    }



    private void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, _playerGameObject.transform.position, _enemySpeed * Time.deltaTime);

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            _playerChars.PlayerTakesDamage(10);
        }

    }

}
