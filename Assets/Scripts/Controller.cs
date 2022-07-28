using Maze;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _maxRotationX = 45f;
    [SerializeField] private float _maxRotationY = 45f;
    [SerializeField] private float _maxRotationZ = 45f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if(transform.rotation.x < _maxRotationX / 100)
            {
                transform.Rotate(Vector3.right, _rotationSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.rotation.z < _maxRotationZ / 100)
            {
                transform.Rotate(Vector3.forward, Mathf.Clamp(_rotationSpeed * Time.deltaTime, -_maxRotationZ, _maxRotationZ));
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if(transform.rotation.x > -_maxRotationX / 100)
            {
                transform.Rotate(Vector3.right, Mathf.Clamp(-_rotationSpeed * Time.deltaTime, -_maxRotationX, _maxRotationX));
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.rotation.z > -_maxRotationZ / 100)
            {
                transform.Rotate(Vector3.forward, Mathf.Clamp(-_rotationSpeed * Time.deltaTime, -_maxRotationZ, _maxRotationZ));
            }

        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (transform.rotation.y > -_maxRotationY / 100)
            {
                transform.Rotate(Vector3.up, Mathf.Clamp(-_rotationSpeed * Time.deltaTime, -_maxRotationY, _maxRotationY));
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (transform.rotation.y < _maxRotationY / 100)
            {
                transform.Rotate(Vector3.up, Mathf.Clamp(_rotationSpeed * Time.deltaTime, -_maxRotationY, _maxRotationY));
            }
        }
    }
}
