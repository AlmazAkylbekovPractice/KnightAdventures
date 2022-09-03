using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private float _movementSpeed = 3f;

    private Vector2 _playerDirection;

    private float _playerSide = 1;


    private void Awake()
    {
        LoadCompanents();
    }

    private void Update()
    {
        GetPlayerInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void LoadCompanents()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void GetPlayerInput()
    {
        _playerDirection.x = Input.GetAxisRaw("Horizontal");
        _playerDirection.y = Input.GetAxisRaw("Vertical");

        if (_playerDirection.x > 0)
            _playerSide = 0.25f;

        if (_playerDirection.x < 0)
            _playerSide = 0.5f;

        if (_playerDirection.y > 0)
            _playerSide = 0.75f;

        if (_playerDirection.y < 0)
            _playerSide = 1f;
    }

    private void MovePlayer() {
   
        _rigidbody.MovePosition(_rigidbody.position + _playerDirection * _movementSpeed * Time.deltaTime);

        _animator.SetFloat("Horizontal", _playerDirection.x);
        _animator.SetFloat("Vertical", _playerDirection.y);
        _animator.SetFloat("Speed", _playerDirection.sqrMagnitude);

        _animator.SetFloat("Side", _playerSide);

    }

    
    
}
