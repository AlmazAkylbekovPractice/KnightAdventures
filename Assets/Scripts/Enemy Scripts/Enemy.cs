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

    private Vector3 _startPosition;
    private Vector2 _nextPosition;
    private Vector2 _enemyDirection;

    private bool isChasing;
    

    // Start is called before the first frame update
    void Awake()
    {
        LoadEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPoint();
    }


    private void LoadEnemy()
    {
        _animator = GetComponent<Animator>();
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
    }

    private void MoveToPoint()
    {
        if (!isChasing)
        {
            if (_nextPosition.x > transform.position.x)
                MoveRight(_nextPosition.x);
            else if (_nextPosition.x < transform.position.x)
                MoveLeft(_nextPosition.x);
            else if (_nextPosition.y > transform.position.y)
                MoveUp(_nextPosition.y);
            else if (_nextPosition.y < transform.position.y)
                MoveDown(_nextPosition.y);

            _animator.SetFloat("Horizontal", _enemyDirection.x);
            _animator.SetFloat("Vertical", _enemyDirection.y);

            if (transform.position.x == _nextPosition.x &&
                transform.position.y == _nextPosition.y)
                ChooseNextPosition();
        } else
        {
            ChasePlayer();
        }
    }

    private void MoveRight(float inputPosX)
    {
        _enemyDirection.x = 1;
        _enemyDirection.y = 0;

        transform.position =
            Vector2.MoveTowards(transform.position, new Vector2(inputPosX,transform.position.y),_speed * Time.deltaTime);
    }

    private void MoveLeft(float inputPosX)
    {
        _enemyDirection.x = -1;
        _enemyDirection.y = 0;

        transform.position =
            Vector2.MoveTowards(transform.position, new Vector2(inputPosX, transform.position.y), _speed * Time.deltaTime);
    }

    private void MoveUp(float inputY)
    {
        _enemyDirection.x = 0;
        _enemyDirection.y = 1;

        transform.position =
            Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, inputY), _speed * Time.deltaTime);
    }

    private void MoveDown(float inputY)
    {
        _enemyDirection.x = 0;
        _enemyDirection.y = -1;

        transform.position =
            Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, inputY), _speed * Time.deltaTime);
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);

        if (_player.transform.position.x > transform.position.x)
            MoveRight(_player.transform.position.x);
        else if (_player.transform.position.x < transform.position.x)
            MoveLeft(_player.transform.position.x);
        else if (_player.transform.position.y > transform.position.y)
            MoveUp(_player.transform.position.y);
        else if (_player.transform.position.y < transform.position.y)
            MoveDown(_player.transform.position.y);

        _animator.SetFloat("Horizontal", _enemyDirection.x);
        _animator.SetFloat("Vertical", _enemyDirection.y);
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
