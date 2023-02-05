using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObjectMoving : MonoBehaviour
{

    public float positionUp;
    public float positionDown;

    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y > positionDown){
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if(this.transform.position.y < positionUp){
            this.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
    }
}
