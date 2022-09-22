using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float _walkingSpeed = 1f;
    [SerializeField] private float _walkingRadius = 1f;
    [SerializeField] private float _slashDamage = 2f;

    private GameObject _player;

    private Animator _animator;
    private CircleCollider2D _circleCollider2D;
    private Rigidbody2D _rigidbody;

    private Vector3 _startPosition;
    private Vector3 _nextPosition;
    private Vector2 _enemyDirection;
    private Vector3 _targetPosition;

    private bool isChasing;

    private string PLAYER_TAG = "Player";
    private string SLASH_ANIM_TAG = "Slash";
    

    // Start is called before the first frame update
    private void Awake()
    {
        LoadEnemy();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        MoveToPoint();
    }


    private void LoadEnemy()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;

        _player = GameObject.FindWithTag(PLAYER_TAG);

        ChooseNextPosition();
    }

    private void ChooseNextPosition()
    {
        var nextX = Random.Range(_startPosition.x - _walkingRadius, _startPosition.x + _walkingRadius);
        var nextY = Random.Range(_startPosition.y - _walkingRadius, _startPosition.y + _walkingRadius);

        _nextPosition.x = nextX;
        _nextPosition.y = nextY;
        _nextPosition.z = 0;
    }

    private void MoveToPoint()
    {
        if (isChasing)
            _targetPosition = _player.transform.position;
        else
            _targetPosition = _nextPosition;

        if (Mathf.Abs(_targetPosition.x - transform.position.x) > Mathf.Abs(_targetPosition.y - transform.position.y))
        {
            if (_targetPosition.x > transform.position.x)
                MoveRight();
            else if (_targetPosition.x < transform.position.x)
                MoveLeft();
        } else
        {
            if (_targetPosition.y > transform.position.y)
                MoveUp();
            else if (_targetPosition.y < transform.position.y)
                MoveDown();
        }

        if ((int)_targetPosition.x == (int) transform.position.x &&
            (int)_targetPosition.y == (int) transform.position.y)
            ChooseNextPosition();

        transform.position = Vector2.MoveTowards(transform.position, _targetPosition,_walkingSpeed * Time.deltaTime);

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
        if (collision.CompareTag(PLAYER_TAG))
        {
            isChasing = true;
            _walkingSpeed = 1.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            isChasing = false;
            _walkingSpeed = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG)
        {
            _animator.SetBool(SLASH_ANIM_TAG, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _animator.SetBool(SLASH_ANIM_TAG, false);
    }

}
