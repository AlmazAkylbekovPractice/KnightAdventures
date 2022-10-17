using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBehavior : IPlayerBehavior
{
    void IPlayerBehavior.Enter(Player player)
    {
        
    }

    void IPlayerBehavior.Exit(Player player)
    {

    }

    void IPlayerBehavior.Update(Player player)
    {
        player.direction.x = Input.GetAxis("Horizontal");
        player.direction.y = Input.GetAxis("Vertical");
        

        if (player.direction.x > 0)
            player.side = 0.25f;

        if (player.direction.x < 0)
            player.side = 0.5f;

        if (player.direction.y > 0)
            player.side = 0.75f;

        if (player.direction.y < 0)
            player.side = 1f;


        //If player movement is equal to null
        if (player.direction.magnitude == 0)
        {
            player.SetBehaviorIdle();
        }

        Debug.Log(player.movementSpeed);
        Debug.Log(player.direction);
    }

    void IPlayerBehavior.FixedUpdate(Player player)
    {
        
        Debug.Log(player.movementSpeed);
        Debug.Log(player.direction);

        player.rigidbody.MovePosition(player.rigidbody.position + player.direction * player.movementSpeed * Time.fixedDeltaTime);

        player.animator.SetFloat("Horizontal", player.direction.x);
        player.animator.SetFloat("Vertical", player.direction.y);
        player.animator.SetFloat("Speed", player.direction.sqrMagnitude);
        player.animator.SetFloat("Side", player.side);
    }
}
