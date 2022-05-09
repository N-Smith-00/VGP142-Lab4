using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public Player player;
    void ToggleAttacking()
    {
        player.attacking = !player.attacking;
    }
}
