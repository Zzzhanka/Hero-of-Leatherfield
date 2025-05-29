using UnityEngine;


public class PlayerProjectile : MonoBehaviour
{

    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    private int _damage;
    private Vector2 _direction;




    public void Setup(Vector2 direction, int damage)
    {

        _direction = direction.normalized;
        _damage = damage;
        Destroy(gameObject, _lifeTime);

    }



    private void Update()
    {

        transform.Translate((_direction * _speed) * Time.deltaTime);

        if (_direction != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (((1 << collision.gameObject.layer) & _enemyLayer) != 0)
        {
            if (collision.TryGetComponent<EnemyCharacteristics>(out var enemy))
            {
                enemy.EnemyTakesDamage(_damage);
            }

            Destroy(gameObject);
        }
        else if (((1 << collision.gameObject.layer) & _obstacleLayer) != 0)
        {
            Destroy(gameObject);
        }

    }

}
