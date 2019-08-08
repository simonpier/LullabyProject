using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SingleChepointManagerPlaceholder_SP : MonoBehaviour
{
    [SerializeField] GameObject savepoint;

    Checkpoint_ML checkpoint;

    private void Start()
    {
        checkpoint = savepoint.GetComponent<Checkpoint_ML>();
    }

    public void RespawnCheck()
    {

        if (checkpoint.FirstCheck)
        {

            checkpoint.Respawn();

        }

        else if (checkpoint.FirstCheck == false)
        {

            checkpoint.Respawn();

        }
    }
}
