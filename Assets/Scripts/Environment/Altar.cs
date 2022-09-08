using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] private GameObject _runes;

    private Transform _playerPosition;
    private bool isNear;

    private void Awake()
    {
        _playerPosition = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(_playerPosition.position,transform.position) < 4f)
            _runes.SetActive(true);
        else _runes.SetActive(false);
    }
}
