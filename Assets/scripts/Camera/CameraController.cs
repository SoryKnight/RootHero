using UnityEngine;

using System.Collections;

public class CameraController : MonoBehaviour
    {
    public Transform camPosition;
    public Transform futurePosition;
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Set " + name + " as future position");
            futurePosition = camPosition;            
            Camera.main.transform.position = futurePosition.position;
            Camera.main.transform.rotation = futurePosition.rotation;
            }
        }
    void OnTriggerExit(Collider other) {
            Debug.Log("Exiting " + name + " so moving to new position");
            if (futurePosition != camPosition) {
                Camera.main.transform.position = futurePosition.position;
                Camera.main.transform.rotation = futurePosition.rotation;
            }
    }

}