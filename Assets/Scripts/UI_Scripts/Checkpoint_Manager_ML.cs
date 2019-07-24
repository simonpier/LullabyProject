using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Checkpoint_Manager_ML : MonoBehaviour
{

    [SerializeField] GameObject check1, check2, check3;

    Checkpoint_ML checkpoint1, checkpoint2, checkpoint3;

    // Start is called before the first frame update
    void Start()
    {

        checkpoint1 = check1.GetComponent<Checkpoint_ML>();
        checkpoint2 = check2.GetComponent<Checkpoint_ML>();
        checkpoint3 = check3.GetComponent<Checkpoint_ML>();

       

    }

    // Update is called once per frame
    void Update()
    {



    }

    private void RespawnCheck()
    {
        if (checkpoint3.FirstCheck)
        {

            checkpoint1.FirstCheck = false;
            checkpoint2.FirstCheck = false;
            checkpoint3.Respawn();

        }
        else if (checkpoint2.FirstCheck)
        {

            checkpoint1.FirstCheck = false;
            checkpoint2.Respawn();

        }

        else if (checkpoint2.FirstCheck)
        {

            checkpoint1.Respawn();

        }
    }
}
