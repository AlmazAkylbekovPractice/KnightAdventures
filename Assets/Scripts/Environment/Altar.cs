using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] private GameObject _runes;

    private Transform _playerPosition;
    private CircleCollider2D _collider;
    private bool isNear;

    private void Awake()
    {
        _playerPosition = GameObject.FindWithTag("Player").transform;
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _runes.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _runes.SetActive(false);
    }
}
