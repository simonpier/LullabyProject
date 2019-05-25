using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentInteraction_SP : MonoBehaviour
{

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DoorOpen()
    {

        anim.SetBool("Opens", true);

    }

    public void DoorClose()
    {

        anim.SetBool("Opens", false);

    }
}
