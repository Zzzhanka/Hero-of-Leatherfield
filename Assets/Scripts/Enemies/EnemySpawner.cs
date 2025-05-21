using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemySpawn;
    [SerializeField] private Transform[] _spawnPoin;

    public float StartSpawnInterval;
    public float SpawnInterval;

    public int AllEnemies;
    public int NowEnemies;

    private int _randEnemies;
    private int _randPoint;
    void Start()
    {
        SpawnInterval = StartSpawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnInterval <= 0 && NowEnemies <= AllEnemies)
        {
            _randEnemies = Random.Range(0, _enemySpawn.Length);
            _randPoint = Random.Range(0, _spawnPoin.Length);

            Instantiate(_enemySpawn[_randEnemies], _spawnPoin[_randPoint].transform.position, Quaternion.identity);

            SpawnInterval = StartSpawnInterval;
            NowEnemies++;
        }
        else
        {
            SpawnInterval -= Time.deltaTime;
        }
    }
}
