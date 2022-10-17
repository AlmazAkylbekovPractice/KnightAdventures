using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatBehavior : IEnemyBehavior
{
    void IEnemyBehavior.Enter(Enemy enemy)
    {
        enemy.targetPosition = enemy.player.transform.position;
        enemy.walkingSpeed = 1.5f;
    }

    void IEnemyBehavior.Exit(Enemy enemy)
    {

    }

    void IEnemyBehavior.Update(Enemy enemy)
    {
        if (Mathf.Abs(enemy.targetPosition.x - enemy.transform.position.x) > Mathf.Abs(enemy.targetPosition.y - enemy.transform.position.y))
        {
            if (enemy.targetPosition.x > enemy.transform.position.x)
            {
                enemy.direction.x = 1;
                enemy.direction.y = 0;
            }
            else
            {
                enemy.direction.x = -1;
                enemy.direction.y = 0;
            }
        }
        else
        {
            if (enemy.targetPosition.y > enemy.transform.position.y)
            {
                enemy.direction.x = 0;
                enemy.direction.y = 1;
            }
            else
            {
                enemy.direction.x = 0;
                enemy.direction.y = -1;
            }
        }

        if (Vector2.Distance(enemy.transform.position, enemy.player.transform.position) < 1.5f)
        {
            enemy.walkingSpeed = 0.5f;
            enemy.animator.SetBool("Slash", true);
        }
        else
        {
            enemy.animator.SetBool("Slash", false);
            enemy.walkingSpeed = 1.5f;
        }

        //Detect the Player
        if (Vector2.Distance(enemy.transform.position, enemy.player.transform.position) > enemy.veiwRange)
        {
            enemy.SetBehaviorWalk();
        }
    }

    void IEnemyBehavior.FixedUpdate(Enemy enemy)
    {

    }
}
