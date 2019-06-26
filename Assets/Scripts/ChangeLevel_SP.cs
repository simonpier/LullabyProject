using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel_SP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.Return))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("change level");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
