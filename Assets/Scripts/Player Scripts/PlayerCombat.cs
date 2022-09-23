using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] private Button _slashButton;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.7f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int attackDamage = 20;

    [SerializeField] private Vector3[] attackPoints;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _slashButton.onClick.AddListener(Slash);
    }
     
    private void LateUpdate()
    {
        AdjustAttackPoint();
    }

    private void Slash()
    {
        SlashAttack();
        SlashAnimation();
    }

    private void AdjustAttackPoint()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
            attackPoint.transform.position =  transform.position + attackPoints[0];
        else if (Input.GetAxisRaw("Vertical") < 0)
            attackPoint.transform.position = transform.position + attackPoints[1];
        else if (Input.GetAxisRaw("Horizontal") > 0)
            attackPoint.transform.position = transform.position + attackPoints[2];
        else if (Input.GetAxisRaw("Horizontal") < 0)
            attackPoint.transform.position = transform.position + attackPoints[3];

    }

    private void SlashAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

        }

    }

    private void SlashAnimation()
    {
        _animator.Play("Slash");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
