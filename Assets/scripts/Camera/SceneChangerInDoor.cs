using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangerInDoor : MonoBehaviour
{
    public Transform puerta;
    // Start is called before the first frame update
    void  OnTriggerStay(Collider other) {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.Log("Pushing button");
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(-90, 200, 0));
            puerta.rotation = newRotation;
            //SceneManager.LoadScene("TombRoom");
        }
    }
}