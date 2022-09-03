using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Button _slashButton;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _slashButton.onClick.AddListener(SlashAnimation);
    }

    private void SlashAnimation()
    {
        _animator.Play("Slash");
    }


}
