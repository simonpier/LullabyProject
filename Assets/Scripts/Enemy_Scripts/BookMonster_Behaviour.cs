using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookMonster_Behaviour : EnemyController_ML
{
    public override void DeathChecker()
    {
        if (canDie && hitPoint <= 0)
        {
            anim.SetBool("death", true);
            isDied = true;
            transform.DOMoveY(respawnPoint.y, 1.5f);
        }
    }
}

