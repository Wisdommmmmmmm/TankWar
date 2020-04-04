using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour {
    public float speed = 10;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) Screen.fullScreen = false;
        if (Vector3.Distance(transform.position, new Vector3(0, 5, 0)) < 0.01f) return;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 5, 0), speed*Time.deltaTime);
    }
}
