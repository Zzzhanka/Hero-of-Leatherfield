using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public PlayerCharacteristics characteristics;

    private Transform player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        characteristics = gameObject.GetComponent<PlayerCharacteristics>();
        
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player takes damage");
            characteristics.PlayerTakesDamage(10);
            Debug.Log("Player health: " + characteristics.PlayerCurrentHealth);
        }
    }
}
