using UnityEngine;
using System.Collections;
using System;

namespace ZombieSplasher
{
    public class EnemyMover : MonoBehaviour, IEnemyMover
    {
        private Vector3 _destinationPos;
        private float _speed;
        private float _rotationSpeed;


        public void Initialize(Vector3 destinationPos, float speed, float rotationSpeed)
        {
            _destinationPos = destinationPos;
            _speed = speed;
            _rotationSpeed = rotationSpeed;
        }

        public void SetDestination(Vector3 destinationPos)
        {
            _destinationPos = destinationPos;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        void Update()
        {
            UpdatePosition();
        }

        void UpdatePosition()
        {
            Vector3 heading = _destinationPos - transform.position;
            transform.Translate(heading.normalized * _speed * Time.deltaTime, Space.World);

            //UpdateRotation(_destinationPos);
            UpdateRotation(heading);

            // TODO: Super slow - improve performance
            //Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            //Vector3 heading = _destinationPos - transform.position;
            //Debug.Log(_destinationPos);
            //rb2d.velocity = heading.normalized * _speed;
        }

        void UpdateRotation(Vector3 heading)
        {
            //transform.Rotate(heading, Space.World);

            Vector3 target = heading;

            //Quaternion _lookRotation = Quaternion.LookRotation(target);
            //transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);

            //transform.LookAt(_destinationPos);  // _destinationPos

            //Vector3 lookAtPoint = _lastPos + heading.normalized;

            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_destinationPos), Time.deltaTime * rotationSpeed);


            //transform.rotation = Quaternion.LookRotation(_destinationPos);

            Quaternion newRotation = Quaternion.LookRotation(transform.position - _destinationPos, Vector3.forward);
            newRotation.x = 0.0f;
            newRotation.z = 0.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * _rotationSpeed);

            //transform.LookAt(lookAtPoint);

            //_lastPos = transform.position;



            //_lastPos = transform.position;
        }

    }
}
