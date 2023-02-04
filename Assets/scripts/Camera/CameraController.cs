using UnityEngine;

using System.Collections;

public class CameraController : MonoBehaviour
    {
    public Transform camPosition;
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Set " + name + " as future position");          
            Camera.main.transform.position = camPosition.position;
            Camera.main.transform.rotation = camPosition.rotation;
            }
        }
    void OnTriggerExit(Collider other) {
            Debug.Log("Exiting " + name + " so moving to new position");
    }

}