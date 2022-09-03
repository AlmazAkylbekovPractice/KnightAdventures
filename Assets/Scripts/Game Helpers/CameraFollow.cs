using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private float _cameraStep = 1f;
    private float _tempStep;

    private Transform _playerPosition;

    private float _innerBorderMaxX = 2f;
    private float _innerBorderMinX = -2f;
    private float _innerBorderMaxY = 1.3f;
    private float _innerBorderMinY = -1.3f;


    private Vector3 _cameraPosition;
    

    private void Awake()
    {
        _playerPosition = GameObject.FindWithTag("Player").transform;
        _cameraPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _playerPosition.position;
        _tempStep = _cameraStep * Time.deltaTime; ;

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

        transform.position = _cameraPosition;
    }
}
