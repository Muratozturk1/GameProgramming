using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _playerTransform;

    [Header("Settings")]
    [SerializeField] private float _defaultSpeed = 5f;
    [SerializeField] private float _chaseSpeed = 7f;


    [Header("Navigation Settings")]
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _patrolRadius = 10f;
    [SerializeField] private int _maxDestinationAttempts = 10;
    [SerializeField] private float _chaseDistanceThreshold = 1.5f;
    [SerializeField] private float _chaseDistance = 2f;
    private NavMeshAgent _catAgent;
    private CatStateController _catStateController;
    private float _timer;
    private bool _isWaiting;
    private bool _isChasing;
    private Vector3 _initalPosition;

    private void Awake()
    {
        _catAgent = GetComponent<NavMeshAgent>();
        _catStateController = GetComponent<CatStateController>();
        SetRandomDestination();
    }
    private void Start()
    {
        _initalPosition = transform.position;
    }
    private void Update()
    {
        if (_playerController.CanCatChase())
        {
            SetChaseMovement();
        }
        else
        {
            SetPatrolMovement();
        }

    }

    private void SetChaseMovement()
    {
        Vector3 directonToPlayer = (_playerTransform.position - transform.position).normalized;
        Vector3 offsetPosition = _playerTransform.position - directonToPlayer * _chaseDistanceThreshold;
        _catAgent.SetDestination(offsetPosition);
        _catAgent.speed = _chaseSpeed;
        _catStateController.ChangeState(CatState.Running);

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _chaseDistance && _isChasing)
        {
            //CATCHED THE CHICK
            _catStateController.ChangeState(CatState.Attacking);
            _isChasing = false;
        }
    }

    private void SetPatrolMovement()
    {
        _catAgent.speed = _defaultSpeed;

        if (!_catAgent.pathPending && _catAgent.remainingDistance <= _catAgent.stoppingDistance)
        {
            if (!_isWaiting)
            {
                _isWaiting = true;
                _timer = _waitTime;
                _catStateController.ChangeState(CatState.Idle);
            }
        }
        if (_isWaiting)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _isWaiting = false;
                SetRandomDestination();
                _catStateController.ChangeState(CatState.Walking);
            }
        }
    }
    private void SetRandomDestination()
    {
        int attempts = 0;
        bool destionationSet = false;

        while (attempts < _maxDestinationAttempts && !destionationSet)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * _patrolRadius;
            randomDirection += _initalPosition;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _patrolRadius, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;

                if (!IsPositionBlocekd(finalPosition))
                {
                    _catAgent.SetDestination(finalPosition);
                    destionationSet = true;
                }
                else
                {
                    attempts++;
                }
            }
            else
            {
                attempts++;
            }
        }
        if (!destionationSet)
        {
            Debug.LogWarning("Failed to find a valid destination ");
            _isWaiting = true;
            _timer = _waitTime * 2;
        }
    }
    private bool IsPositionBlocekd(Vector3 position)
    {
        if (NavMesh.Raycast(transform.position, position, out NavMeshHit hit, NavMesh.AllAreas))
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 pos = (_initalPosition != Vector3.zero) ? _initalPosition : transform.position;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, _patrolRadius);
    }
}
