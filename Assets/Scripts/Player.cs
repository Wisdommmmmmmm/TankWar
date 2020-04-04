using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3;
    public int id;
    public Sprite[] tank;
    public GameObject BulletPrefab;
    public GameObject ExplosionPrefab;
    public GameObject ShieldPrefab;
    public AudioSource MoveAudio;
    public AudioClip[] AudioResource;

    private float CdTimeCount = 0;
    private float DefendTimeCount = 0;
    private bool isDefended = true;
    private SpriteRenderer sr;
    private Vector3 BulletEulerAngle;
    // Start is called before the first frame update
    void Start() {
        sr = gameObject.GetComponent<SpriteRenderer>();
        ShieldPrefab.SetActive(true);
    }

    // Update is called once per frame
    void Update() {

    }
    private void FixedUpdate() {
        if (PlayerManager.Instance.isDefeated) return;
        Move();
        //玩家受保护时间，3s
        if(isDefended) {
            DefendTimeCount += Time.fixedDeltaTime;
            if (DefendTimeCount >= 3) {
                isDefended = false;
                ShieldPrefab.SetActive(false);
            }
        }
        //子弹攻击cd，为0.4s
        if(CdTimeCount >= 0.4) Attack();
        else CdTimeCount += Time.fixedDeltaTime;
        
    }
    //攻击
    private void Attack() {
        if (id == 1) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Instantiate(BulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + BulletEulerAngle));
                CdTimeCount = 0;
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Keypad0)) {
                Instantiate(BulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + BulletEulerAngle));
                CdTimeCount = 0;
            }
        }
    }
    //移动
    private void Move() {
        if (id == 1) {
            float h = Input.GetAxis("Player0_Horizontal");
            transform.Translate(Vector3.right * h * speed * Time.fixedDeltaTime, Space.World);

            if (Mathf.Abs(h) > 0.01f) {
                MoveAudio.clip = AudioResource[1];
                if (!MoveAudio.isPlaying) MoveAudio.Play();
            }
            if (h < 0) {
                sr.sprite = tank[3];
                BulletEulerAngle = new Vector3(0, 0, 90);
            }
            else if (h > 0) {
                sr.sprite = tank[1];
                BulletEulerAngle = new Vector3(0, 0, -90);
            }
            else {
                float v = Input.GetAxis("Player0_Vertical");
                transform.Translate(Vector3.up * v * speed * Time.fixedDeltaTime, Space.World);
                if (v < 0) {
                    sr.sprite = tank[2];
                    BulletEulerAngle = new Vector3(0, 0, 180);
                }
                else if (v > 0) {
                    sr.sprite = tank[0];
                    BulletEulerAngle = new Vector3(0, 0, 0);
                }
                if (Mathf.Abs(v) > 0.01f) {
                    MoveAudio.clip = AudioResource[1];
                    if (!MoveAudio.isPlaying) MoveAudio.Play();
                }
                else {
                    MoveAudio.clip = AudioResource[0];
                    if (!MoveAudio.isPlaying) MoveAudio.Play();
                }
            }
        }
        else {
            float h = Input.GetAxis("Player1_Horizontal");
            transform.Translate(Vector3.right * h * speed * Time.fixedDeltaTime, Space.World);

            if (Mathf.Abs(h) > 0.01f) {
                MoveAudio.clip = AudioResource[1];
                if (!MoveAudio.isPlaying) MoveAudio.Play();
            }
            if (h < 0) {
                sr.sprite = tank[3];
                BulletEulerAngle = new Vector3(0, 0, 90);
            }
            else if (h > 0) {
                sr.sprite = tank[1];
                BulletEulerAngle = new Vector3(0, 0, -90);
            }
            else {
                float v = Input.GetAxis("Player1_Vertical");
                transform.Translate(Vector3.up * v * speed * Time.fixedDeltaTime, Space.World);
                if (v < 0) {
                    sr.sprite = tank[2];
                    BulletEulerAngle = new Vector3(0, 0, 180);
                }
                else if (v > 0) {
                    sr.sprite = tank[0];
                    BulletEulerAngle = new Vector3(0, 0, 0);
                }
                if (Mathf.Abs(v) > 0.01f) {
                    MoveAudio.clip = AudioResource[1];
                    if (!MoveAudio.isPlaying) MoveAudio.Play();
                }
                else {
                    MoveAudio.clip = AudioResource[0];
                    if (!MoveAudio.isPlaying) MoveAudio.Play();
                }
            }
        }
    }
    //死亡
    private void Die() {
        if (isDefended) return;
        //爆炸特效
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
        PlayerManager.Instance.isDead = true;
        PlayerManager.Instance.NewPlayerId = id;
    }
    //开启保护
    private void StartProtect() {
        isDefended = true;
        ShieldPrefab.SetActive(true);
        DefendTimeCount = 0;
    }
    //让所有敌人死亡
    private void setAllEnemyDie() {
        PlayerManager.Instance.AllEnemyDie = true;
        Invoke("Rcovery", 0.1f);
    }
    private void Rcovery() {
        PlayerManager.Instance.AllEnemyDie = false;
    }
}
