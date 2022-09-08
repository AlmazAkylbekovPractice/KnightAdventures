using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private float _cameraStep = 2.5f;
    [SerializeField] private float _tempStep;

    private Transform _playerPosition;

    [SerializeField] private float _innerBorderMaxX = 2f;
    [SerializeField] private float _innerBorderMinX = -2f;
    [SerializeField] private float _innerBorderMaxY = 1.3f;
    [SerializeField] private float _innerBorderMinY = -1.3f;

    [SerializeField] private float _outterBorderMaxX = 6.5f;
    [SerializeField] private float _outterBorderMaxY = 9f;
    [SerializeField] private float _outterBorderMinX = -6.5f;
    [SerializeField] private float _outterBorderMinY = -6f;

    private Vector3 _cameraPosition;
    

    private void Awake()
    {
        _playerPosition = GameObject.FindWithTag("Player").transform;
        _cameraPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerPosition != null)
        {

            transform.position = _playerPosition.position;
            _tempStep = _cameraStep * Time.deltaTime;

            if (transform.position.x >= _innerBorderMaxX)
            {
                _innerBorderMaxX += _tempStep;
                _innerBorderMinX += _tempStep;
                _cameraPosition.x += _tempStep;
            }


            if (transform.position.x <= _innerBorderMinX)
            {
                _innerBorderMinX -= _tempStep;
                _innerBorderMaxX -= _tempStep;
                _cameraPosition.x -= _tempStep;
            }

            if (transform.position.y >= _innerBorderMaxY)
            {
                _innerBorderMaxY += _tempStep;
                _innerBorderMinY += _tempStep;
                _cameraPosition.y += _tempStep;
            }

            if (transform.position.y <= _innerBorderMinY)
            {

                _innerBorderMaxY -= _tempStep;
                _innerBorderMinY -= _tempStep;
                _cameraPosition.y -= _tempStep;
            }

            if (_cameraPosition.x > _outterBorderMaxX)
                _cameraPosition.x = _outterBorderMaxX;

            if (_cameraPosition.x < _outterBorderMinX)
                _cameraPosition.x = _outterBorderMinX;

            if (_cameraPosition.y > _outterBorderMaxY)
                _cameraPosition.y = _outterBorderMaxY;

            if (_cameraPosition.y < _outterBorderMinY)
                _cameraPosition.y = _outterBorderMinY;

            transform.position = _cameraPosition;
        }
    }
}
