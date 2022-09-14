using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _walkRadius = 1f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private GameObject _player;

    private Animator _animator;
    private CircleCollider2D _circleCollider2D;
    private Rigidbody2D _rigidbody;

    private Vector3 _startPosition;
    private Vector3 _nextPosition;
    private Vector3 _direction;
    private Vector2 _enemyDirection;

    private bool isChasing;
    

    // Start is called before the first frame update
    private void Awake()
    {
        LoadEnemy();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveToPoint();
    }


    private void LoadEnemy()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;

        _player = GameObject.FindWithTag("Player");

        ChooseNextPosition();
    }

    private void ChooseNextPosition()
    {
        var nextX = Random.Range(_startPosition.x - _walkRadius, _startPosition.x + _walkRadius);
        var nextY = Random.Range(_startPosition.y - _walkRadius, _startPosition.y + _walkRadius);

        _nextPosition.x = nextX;
        _nextPosition.y = nextY;
        _nextPosition.z = 0;

        Debug.Log(_nextPosition);
    }

    private void MoveToPoint()
    {
        if (!isChasing)
        {
            _direction = _nextPosition - transform.position;
        } else
        {
            _direction = _player.transform.position - transform.position;
        }


        if (_direction.normalized.x > _rigidbody.position.x)
            MoveRight();
        else if (_direction.normalized.x < _rigidbody.position.x)
            MoveLeft();
        else if (_direction.normalized.y > _rigidbody.position.y)
            MoveUp();  
        else if (_direction.normalized.y < _rigidbody.position.y)
            MoveDown();

        if ((int) _nextPosition.x == (int) _rigidbody.position.x &&
            (int) _nextPosition.y == (int) _rigidbody.position.y)
            ChooseNextPosition();

        _rigidbody.MovePosition(transform.position + _direction.normalized * _speed * Time.fixedDeltaTime);

        _animator.SetFloat("Horizontal",_enemyDirection.x);
        _animator.SetFloat("Vertical", _enemyDirection.y);
    }

    private void MoveRight()
    {
        _enemyDirection.x = 1;
        _enemyDirection.y = 0;

    }

    private void MoveLeft()
    {
        _enemyDirection.x = -1;
        _enemyDirection.y = 0;
    }

    private void MoveUp()
    {
        _enemyDirection.x = 0;
        _enemyDirection.y = 1;
    }

    private void MoveDown()
    {
        _enemyDirection.x = 0;
        _enemyDirection.y = -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChasing = true;
            _speed = 1.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChasing = false;
            _speed = 1f;
        }
    }

}
