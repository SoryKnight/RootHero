using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitScene : MonoBehaviour
{
    
    private bool triggered = false;    

    void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){            
            Debug.Log("Triggered! "+ name);
            triggered = true;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.tag == "Player"){            
            Debug.Log("Not Triggered! "+ name);
            triggered = false;
        }
    }

    void  Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && triggered == true){
            SceneManager.LoadScene("FinalBoss");
        }

    }
}
