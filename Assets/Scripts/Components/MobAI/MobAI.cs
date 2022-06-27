using Assets.Scripts.Components.ColliderBased;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Components.MobAI
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheckComponent _sightRange;
        [SerializeField] private LayerCheckComponent _attackRange;
        [SerializeField] private LayerCheckComponent _groundChecker;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private LayerMask _mask;

        [SerializeField] private float _attackCooldown;
        [SerializeField] private Vector3 _walkPoint;
        [SerializeField] private float _walkRange;

        private GameObject _target;
        private IEnumerator _current;
        private Animator _animator;
        [SerializeField] private bool _walkPointSet;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
          
        }

        private void Update()
        {
            _animator.SetFloat("speed", _agent.velocity.magnitude/ _agent.speed);
            

        }

        private void FixedUpdate()
        {

            if (!_sightRange.IsTouchingLayer)
                StartState(Patrol());
        }

        public void OnGoToHero(GameObject go)
        {
            _target = go;
            StartState(GoToHero());
        }

        private void SetDirection()
        {
            _agent.SetDestination(_target.transform.position);
            
            transform.LookAt(_target.transform);
        }

        private void StartState(IEnumerator coroutine)
        {
            if (_current != null)
            {
                StopCoroutine(_current);
            }
            _current = coroutine;
            StartCoroutine(coroutine);
        }

        private IEnumerator Attack()
        {
            while (_attackRange.IsTouchingLayer)
            {
                //_agent.SetDestination(transform.position);
                _animator.SetTrigger("attack");
                yield return new WaitForSeconds(1f);
            }
            StartState(GoToHero());
        }

        private IEnumerator GoToHero()
        {
            while (_sightRange.IsTouchingLayer)
            {
              
                Debug.Log("gotohero");
                if (_attackRange.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirection();
                }
                yield return null;
            }


            StartState(Patrol());
        }

        private IEnumerator Patrol()
        {
            transform.LookAt(_walkPoint);
            if (!_walkPointSet)
            {
                SetWalkPoint();
                yield return null;
            }
            if (_walkPointSet)
            {
                _agent.SetDestination(_walkPoint);
                if (IsOnPoint())
                {
                    _walkPointSet = false;
                }
                yield return new WaitForSeconds(2f);

            }
        }

        private void SetWalkPoint()
        {
            float randomZ = Random.Range(-_walkRange, _walkRange);
            float randomX = Random.Range(-_walkRange, _walkRange);

            _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            if (Physics.Raycast(_walkPoint, -transform.up, 2f, _mask))
            {
                _walkPointSet = true;
            }
        }

        private bool IsOnPoint()
        {
            return (transform.position - _walkPoint).magnitude<1f;
        }

        public void TakeDamage()
        {
            _animator.SetTrigger("hit");
        }


    }
}
