using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStart : MonoBehaviour
{

    public GameObject image;
    public GameObject button;
    public UnityEngine.Video.VideoPlayer video;
    public float gapTime;
    public double time;
    public double currentTime;

    void Awake()
    {
        button.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        time = video.clip.length;
        video.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(image){
                Destroy(image);
                video.Play();
            }else{
                return;
            }

        }
        if(video){
            currentTime = video.time;
            if(currentTime >= time - gapTime){
                button.SetActive(true);
                Destroy(video);
            }
        }else{
            return;
        }
        
    }
}
