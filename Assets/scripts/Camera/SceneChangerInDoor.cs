using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangerInDoor : MonoBehaviour
{
    public Transform puerta;    
    public TombOpen tumba;
    public string sceneToTransit;
    private bool triggered = false;
    private bool isOpen = false;

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
        if ((Input.GetAxis("Vertical") > 0) && tumba.finished == true && triggered == true){
            if (isOpen == true){
                SceneManager.LoadScene(sceneToTransit);
            }
            else{
                Debug.Log("Pushing button");
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(-90, 200, 0));
                puerta.rotation = newRotation;
                isOpen = true;
            }
        }

    }
}