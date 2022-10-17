using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBehavior
{
    void Enter(Enemy enemy);
    void Exit(Enemy enemy);
    void Update(Enemy enemy);
    void FixedUpdate(Enemy enemy);
}
