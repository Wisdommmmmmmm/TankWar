using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour { 
    public float speed = 3;
    public Sprite[] tank;
    public GameObject BulletPrefab;
    public GameObject ExplosionPrefab;

    private float CdTimeCount = 0;
    private float TurnTimeCount = 0;
    private float v = -1, h = 0;
    private float p;
    private SpriteRenderer sr;
    private Vector3 BulletEulerAngle, position;
    private GameObject Explosion;
    // Start is called before the first frame update
    void Start() {
        sr = gameObject.GetComponent<SpriteRenderer>();
        position = transform.position;
    }

    // Update is called once per frame
    void Update() {

    }
    private void FixedUpdate() {
        if (PlayerManager.Instance.AllEnemyDie) Destroy(gameObject);
        Move();
        //子弹攻击时间间隔，为3s
        if (CdTimeCount >= 3) Attack();
        else CdTimeCount += Time.fixedDeltaTime;

    }
    //攻击
    private void Attack() {
        Instantiate(BulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + BulletEulerAngle));
        CdTimeCount = 0;
    }
    //移动
    private void Move() {
        if(TurnTimeCount >= 4) {
            int x = Random.Range(0, 8);
            if(x >= 5) {
                v = -1;
                h = 0;
            }
            else if(x == 0) {
                v = 1;
                h = 0;
            }
            else if(1 <= x && x <= 2) {
                h = -1;
                v = 0;
            }
            else if(3 <= x && x <= 4) {
                h = 1;
                v = 0;
            }
            TurnTimeCount = 0;
        }
        else {
            TurnTimeCount += Time.fixedDeltaTime;
        }
        transform.Translate(Vector3.right * h * speed * Time.fixedDeltaTime, Space.World);
        if (h < 0) {
            sr.sprite = tank[3];
            BulletEulerAngle = new Vector3(0, 0, 90);
        }
        else if (h > 0) {
            sr.sprite = tank[1];
            BulletEulerAngle = new Vector3(0, 0, -90);
        }
        else {
            transform.Translate(Vector3.up * v * speed * Time.fixedDeltaTime, Space.World);
            if (v < 0) {
                sr.sprite = tank[2];
                BulletEulerAngle = new Vector3(0, 0, 180);
            }
            else if (v > 0) {
                sr.sprite = tank[0];
                BulletEulerAngle = new Vector3(0, 0, 0);
            }
        }
    }
    //死亡
    private void Die() {
        //爆炸特效
        Explosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Explosion.GetComponent<Explosion>().isEnemy = true;
        //死亡
        Destroy(gameObject);
        PlayerManager.Instance.Score++;
    }
    //碰撞转向
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            h = -h;
            v = -v;
        }
    }
}

