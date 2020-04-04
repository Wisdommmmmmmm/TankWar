using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour {
    public Transform pos1, pos2;
    public int option = 1;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            option = 1;
            transform.position = pos1.position;
        }
        if(Input.GetKeyDown(KeyCode.S)) {
            option = 2;
            transform.position = pos2.position;
        }
        if(option == 1 && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Scene01");
        }
        if (option == 2 && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Scene02");
        }
    }
}
