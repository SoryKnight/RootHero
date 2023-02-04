using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void  OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            SceneManager.LoadScene("TombRoom");
        }
    }
}