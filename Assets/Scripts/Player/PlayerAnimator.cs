using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;
    [Header("Bow Pieces")]
    public Transform center;
    public Transform rightBowHand;
    public Transform leftBowHand;
    public float bowHandDistance = 2;
    public float arrowHandDistance = -0.1f;

    // Start is called before the first frame update
    void Start()
    {
        playerController.onPlayerStateChanged += UpdateAnimator;
    }

    // Update is called once per frame
    void UpdateAnimator(PlayerState playerState)
    {
        bool feral = playerState.form == PlayerState.Form.FERAL;
        //Anthro/Feral form
        animator.SetBool("feral", feral);
        //Flip X
        float lookDirection = (feral) ? playerState.moveDirection : playerState.lookDirection.x;
        if (lookDirection != 0)
        {
            Vector3 scale = playerController.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(lookDirection);
            playerController.transform.localScale = scale;
        }
        //Aiming
        Transform bowHand = (lookDirection > 0) ? leftBowHand : rightBowHand;
        Transform arrowHand = (lookDirection > 0) ? rightBowHand : leftBowHand;
        bowHand.position = (Vector2)center.position + (playerState.lookDirection * bowHandDistance);
        arrowHand.position = (Vector2)center.position + (playerState.lookDirection * arrowHandDistance);
    }
}
