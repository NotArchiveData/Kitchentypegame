using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    
    private const string IS_WALKING = "IsWalking";
    private Animator animate_this;

    private void Awake() {
        animate_this = GetComponent<Animator>();
        
    }

    private void Update() {
        animate_this.SetBool(IS_WALKING, player.IsWalking());
    }
}
