     using UnityEngine;
     using UnityEngine.SceneManagement;
     using System.Collections;
     
     public class Restart : MonoBehaviour {
     
        public void RestartGame() {
            Debug.Log("Clicking button");
             SceneManager.LoadScene("Nivel1");
         }

     }