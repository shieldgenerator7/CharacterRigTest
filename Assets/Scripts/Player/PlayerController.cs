using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerState playerState;
    public delegate void OnPlayerStateChanged(PlayerState playerState);
    public event OnPlayerStateChanged onPlayerStateChanged;

    public void processInputState(InputState inputState)
    {
        //Movement
        playerState.moveDirection = inputState.movementDirection.x;
        playerState.running = inputState.run;
        //Look Direction
        playerState.lookDirection = inputState.lookDirection;
        //Jumping
        if (playerState.jumping != inputState.jump)
        {
            if (!playerState.jumping && inputState.jump && playerState.grounded)
            {
                playerState.jumping = true;
                playerState.falling = false;
                //playerState.grounded = true;
            }
            else if (playerState.jumping && !inputState.jump)
            {
                playerState.jumping = false;
                if (!playerState.grounded)
                {
                    playerState.falling = true;
                }
            }
        }
        //Transforming
        if (playerState.grounded)
        {
            if (inputState.movementDirection.y < 0)
            {
                playerState.form = PlayerState.Form.FERAL;
            }
            if (inputState.movementDirection.y > 0)
            {
                playerState.form = PlayerState.Form.ANTHRO;
            }
        }
        //Ability
        playerState.ability1 = inputState.ability1;
        playerState.ability2 = inputState.ability2;
        //Delegate
        onPlayerStateChanged?.Invoke(playerState);
    }


    ///TODO: move to some other script, perhaps the environment state updater one
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0 && collision.contacts[0].point.y < transform.position.y)
        {
            playerState.grounded = true;
            onPlayerStateChanged?.Invoke(playerState);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.contacts.Length == 0 || collision.contacts[0].point.y < transform.position.y)
        {
            playerState.grounded = false;
            onPlayerStateChanged?.Invoke(playerState);
        }
    }
}
