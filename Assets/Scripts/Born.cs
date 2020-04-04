using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject[] PlayerPrefabList;
    public GameObject[] EnemyPrefabList;
    public int isPlayer = 0;
    void Start() {
        Invoke("BornPlayer", 1);
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update() {
        
    }
    public void BornPlayer() {
        if (isPlayer > 0) {
            GameObject Player = Instantiate(PlayerPrefabList[isPlayer-1], transform.position, Quaternion.identity);
            Player.GetComponent<Player>().id = isPlayer;
        }
        else {
            int x = Random.Range(0, 2);
            Instantiate(EnemyPrefabList[x], transform.position, Quaternion.identity);
        }
    }
}
