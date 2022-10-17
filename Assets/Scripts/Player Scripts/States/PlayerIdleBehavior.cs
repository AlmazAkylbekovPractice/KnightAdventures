using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleBehavior : IPlayerBehavior
{
    void IPlayerBehavior.Enter(Player player)
    {
        player.animator.SetFloat("Speed", player.direction.sqrMagnitude);
    }

    void IPlayerBehavior.Exit(Player player)
    {

    }

    void IPlayerBehavior.Update(Player player)
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            player.SetBehaviorMove();
        }
    }

    void IPlayerBehavior.FixedUpdate(Player player)
    {

    }
}
