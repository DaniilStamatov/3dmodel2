
using System.Collections;

using UnityEngine;

namespace Assets.Scripts.Components
{
    public class ArrowProjectile : MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] private Rigidbody _rigidbody;
        Cinemachine.CinemachineImpulseSource _source;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.centerOfMass = transform.position;
        }

        public void Fire()
        {
            _rigidbody.AddForce(transform.forward*_force, ForceMode.Impulse);
            _source = GetComponent<Cinemachine.CinemachineImpulseSource>();
            _source.GenerateImpulse(Camera.main.transform.forward);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name != "Player")
            {
                StartCoroutine(Countdown());
               _rigidbody.isKinematic = true;

            }
        }

        private IEnumerator Countdown()
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
    }
}
