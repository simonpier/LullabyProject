using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherLanters_ML : MonoBehaviour
{
    [SerializeField] GameObject light;
    [SerializeField] GameObject father;

    Father_Boss_Behaviour_ML fatherScript;

    private bool check;

    // Start is called before the first frame update
    void Start()
    {
        fatherScript = father.GetComponent<Father_Boss_Behaviour_ML>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetButtonDown("Interaction") && !check)
        {
            light.SetActive(true);
            check = true;
            fatherScript.LanternCount++;

            if (fatherScript.LanternCount >= 4)
            {
                fatherScript.Death = true;
            }

        }
    }
}
