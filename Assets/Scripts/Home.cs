using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour {
    public Sprite DieSprite;
    public GameObject ExplosionPrefab;
    public AudioClip DieAudio;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start() {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        
    }
    public void Die() {
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        sr.sprite = DieSprite;
        PlayerManager.Instance.isDefeated = true;
        AudioSource.PlayClipAtPoint(DieAudio, transform.position);
    }
}
