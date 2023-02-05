using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacherSwitch : MonoBehaviour
{
    public GameObject Player;
    public GameObject newPlayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Player.activeSelf){
            newPlayer.transform.position = Player.transform.position + new Vector3(0, 0.5f, 0);
            newPlayer.transform.rotation = Player.transform.rotation;
            Player.SetActive(false);
            newPlayer.SetActive(true);
        }
    }
}
