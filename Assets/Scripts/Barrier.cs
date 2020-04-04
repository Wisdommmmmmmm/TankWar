using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {
    public AudioClip HitAudio; 
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
    public void PlayAudio() {
        AudioSource.PlayClipAtPoint(HitAudio, transform.position);
    }
}
