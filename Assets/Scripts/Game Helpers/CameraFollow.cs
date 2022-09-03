using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private float _cameraStep = 1.2f;

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

        if (transform.position.x >= _innerBorderMaxX)
        {
            _innerBorderMaxX += _cameraStep * Time.deltaTime;
            _innerBorderMinX += _cameraStep * Time.deltaTime;
            _cameraPosition.x += _cameraStep * Time.deltaTime;
        }


        if (transform.position.x <= _innerBorderMinX)
        {
            _innerBorderMinX -= _cameraStep * Time.deltaTime;
            _innerBorderMaxX -= _cameraStep * Time.deltaTime;
            _cameraPosition.x -= _cameraStep * Time.deltaTime;
        }

        if (transform.position.y >= _innerBorderMaxY)
        {
            _innerBorderMaxY += _cameraStep * Time.deltaTime;
            _innerBorderMinY += _cameraStep * Time.deltaTime;
            _cameraPosition.y += _cameraStep * Time.deltaTime;
        }

        if (transform.position.y <= _innerBorderMinY)
        {
            _innerBorderMaxY -= _cameraStep * Time.deltaTime;
            _innerBorderMinY -= _cameraStep * Time.deltaTime;
            _cameraPosition.y -= _cameraStep * Time.deltaTime;
        }

        transform.position = _cameraPosition;
    }
}
