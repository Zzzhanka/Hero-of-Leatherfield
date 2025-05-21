using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 3f;

    private NavMeshAgent _agent;
    private Transform _target;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _target = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            _agent.SetDestination(_target.position);

            _agent.speed = _enemySpeed;
        }
    }
}
