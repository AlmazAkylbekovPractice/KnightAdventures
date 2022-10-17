using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatBehavior : IPlayerBehavior
{
    private Vector3 rightAttack = new Vector3(0.7f,0,0);
    private Vector3 leftAttack = new Vector3(-0.7f,0,0);
    private Vector3 upAttack = new Vector3(0,0.6f,0);
    private Vector3 downAttack = new Vector3(0,-0.6f,0);

    void IPlayerBehavior.Enter(Player player)
    {
        if (player.side == 0.25)
            player.attackPoint.transform.position = player.transform.position + rightAttack;
        else if (player.side == 0.5)
            player.attackPoint.transform.position = player.transform.position + leftAttack;
        else if (player.side == 0.75)
            player.attackPoint.transform.position = player.transform.position + upAttack;
        else if (player.side == 1)
            player.attackPoint.transform.position = player.transform.position + downAttack;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.attackPoint.position, player.damageRange, player.enemiesLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(player.attackDamage);

        }

        player.animator.Play("Slash");
    }

    void IPlayerBehavior.Exit(Player player)
    {

    }

    void IPlayerBehavior.Update(Player player)
    {
        if (player.direction.magnitude != 0)
        {
            player.SetBehaviorMove();
        } else
        {
            player.SetBehaviorIdle();
        }
    }

    void IPlayerBehavior.FixedUpdate(Player player)
    {
        
    }

}
