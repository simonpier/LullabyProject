using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerMove_KT move;
    ChangeWeapon_NN weapon;
    PlayerStats_ML stats;
    void Start()
    {
        animator = GetComponent<Animator>();
        move = GetComponent<PlayerMove_KT>();
        weapon = GetComponent<ChangeWeapon_NN>();
        stats = GetComponent<PlayerStats_ML>();
    }

    void Update()
    {
        animator.SetFloat("Speed", move.Speed);
        animator.SetBool("Candle", weapon.NowWeapon == ChangeWeapon_NN.WEAPON.Candle);
        animator.SetBool("Lantern", weapon.NowWeapon == ChangeWeapon_NN.WEAPON.Lantern);
        animator.SetBool("Raise", weapon.LightRaise);
        animator.SetBool("Front", move.FrontAnim);
        animator.SetBool("Back", move.BackAnim);
        animator.SetBool("Dead", stats.Health <= 0);
    }
}
