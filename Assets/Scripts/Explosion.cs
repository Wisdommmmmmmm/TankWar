using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public AudioClip ExplosionAudio;
    //0:一条命 
    public GameObject[] BonusPrefab;
    public bool isEnemy;

    private float p;
    // Start is called before the first frame update
    void Start() {
        AudioSource.PlayClipAtPoint(ExplosionAudio, transform.position);
        Destroy(gameObject, 0.167f);
        if(isEnemy) Invoke("CreateBonus", 0.1f);
    }

    // Update is called once per frame
    void Update() {
        
    }
    private void CreateBonus() {
        //每种奖励的生成概率都是0.25
        p = Random.Range(0.0f, 1.0f);
        if(p <= 0.25f) Instantiate(BonusPrefab[0], transform.position, Quaternion.identity);
        else if(p <= 0.5f) Instantiate(BonusPrefab[1], transform.position, Quaternion.identity);
        else if(p <= 0.75f) Instantiate(BonusPrefab[2], transform.position, Quaternion.identity);
        else Instantiate(BonusPrefab[3], transform.position, Quaternion.identity);
    }
}
