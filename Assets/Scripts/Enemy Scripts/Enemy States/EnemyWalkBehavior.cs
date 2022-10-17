using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkBehavior : IEnemyBehavior
{
    void IEnemyBehavior.Enter(Enemy enemy)
    {
        //Choosing next position
        enemy.nextPosition.x = Random.Range(enemy.startPos.x - enemy.walkingRadius, enemy.startPos.x + enemy.walkingRadius);
        enemy.nextPosition.y = Random.Range(enemy.startPos.y - enemy.walkingRadius, enemy.startPos.y + enemy.walkingRadius); 
        enemy.targetPosition = enemy.nextPosition;

        //Setting walk speed
        enemy.walkingSpeed = 1.0f;

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
            } else
            {
                enemy.direction.x = -1;
                enemy.direction.y = 0;
            }
        } else
        {
            if (enemy.targetPosition.y > enemy.transform.position.y)
            {
                enemy.direction.x = 0;
                enemy.direction.y = 1;
            } else
            {
                enemy.direction.x = 0;
                enemy.direction.y = -1;
            }
        }

        if ((int)enemy.targetPosition.x == (int)enemy.transform.position.x &&
            (int)enemy.targetPosition.y == (int)enemy.transform.position.y){
            enemy.nextPosition.x = Random.Range(enemy.startPos.x - enemy.walkingRadius, enemy.startPos.x + enemy.walkingRadius);
            enemy.nextPosition.y = Random.Range(enemy.startPos.y - enemy.walkingRadius, enemy.startPos.y + enemy.walkingRadius);
            enemy.targetPosition = enemy.nextPosition;
        }
            

        //Detect the Player
        if (Vector2.Distance(enemy.transform.position,enemy.player.transform.position) < enemy.veiwRange)
        {
            enemy.SetCombatBehavior();
        }
    }

    void IEnemyBehavior.FixedUpdate(Enemy enemy)
    {
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.targetPosition, enemy.walkingSpeed * Time.deltaTime);

        enemy.animator.SetFloat("Horizontal", enemy.direction.x);
        enemy.animator.SetFloat("Vertical", enemy.direction.y);
    }
}
