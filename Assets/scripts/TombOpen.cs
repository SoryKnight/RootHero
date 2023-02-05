using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombOpen : MonoBehaviour
{
    public Transform Ataud;
    public Transform AtaudAbierto;
    public GameObject Player;
    public GameObject newPlayer;
    private bool triggered = false;
    public bool finished = false;

    void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            triggered = true;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.tag == "Player"){
            triggered = false;
        }
    }

    void  Update() {
        if (triggered == true && (Input.GetAxis("Vertical") < 0) && finished == false) {
            Debug.Log("Pushing button");
            Ataud.position = AtaudAbierto.position;
            Ataud.rotation = AtaudAbierto.rotation;
            //SceneManager.LoadScene("TombRoom");
            Player.SetActive(false);
            newPlayer.SetActive(true);
            finished = true;
        }
    }
}
