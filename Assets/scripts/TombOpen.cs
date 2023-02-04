using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombOpen : MonoBehaviour
{
    public Transform Ataud;
    public Transform AtaudAbierto;
    void  OnTriggerStay(Collider other) {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.DownArrow)) {
            Debug.Log("Pushing button");
            Ataud.position = AtaudAbierto.position;
            Ataud.rotation = AtaudAbierto.rotation;
            //SceneManager.LoadScene("TombRoom");
        }
    }
}
