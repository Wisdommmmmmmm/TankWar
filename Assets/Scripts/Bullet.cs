using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 10;
    public bool isPlayer;
    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject, 2.2f);
    }
    // Update is called once per frame
    void Update() {
        transform.Translate(speed*Vector3.up*Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.tag) {
            case "Player":
                if (!isPlayer) {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }    
                break;
            case "Enemy":
                if (isPlayer) {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Home":
                if(!isPlayer) collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                if (isPlayer) collision.SendMessage("PlayAudio");
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
