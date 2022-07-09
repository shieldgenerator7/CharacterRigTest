using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;
    public Transform center;
    public Transform bowHand;
    public float bowHandDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerController.onPlayerStateChanged += UpdateAnimator;
    }

    // Update is called once per frame
    void UpdateAnimator(PlayerState playerState)
    {
        //Anthro/Feral form
        animator.SetBool("feral", playerState.form == PlayerState.Form.FERAL);
        //Flip X
        float lookDirection = playerState.lookDirection.x;
        if (lookDirection != 0)
        {
            Vector3 scale = playerController.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(lookDirection);
            playerController.transform.localScale = scale;
        }
        //Aiming
        bowHand.position = (Vector2)center.position + (playerState.lookDirection * bowHandDistance);
    }
}
