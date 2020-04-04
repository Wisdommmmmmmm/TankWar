using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    //属性值
    public int LifeValue = 3;
    public int Score = 0;
    public bool isDead = false;
    public bool isDefeated = false;
    public bool AllEnemyDie = false;
    public int NewPlayerId;

    public GameObject Born;
    public GameObject GameOverUI;
    public Text Text_Score;
    public Text Text_LifeValue;
    //单例模式,ctrl+r+e快捷健
    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) Screen.fullScreen = false;
        //游戏失败，返回主界面
        if (isDefeated) {
            GameOverUI.SetActive(true);
            Invoke("ReturnMenu", 3);
            return;
        }
        if(isDead) {
            Recovery();
        }
        Text_LifeValue.text = LifeValue.ToString();
        Text_Score.text = Score.ToString();
    }
    private void ReturnMenu() {
        SceneManager.LoadScene("Scene00");
    }
    private void Recovery() {
        if(LifeValue == 0) {
            isDefeated = true;
        }
        else {
            LifeValue--;
            GameObject PlayerBorn;
            if (NewPlayerId == 1) PlayerBorn = Instantiate(Born, new Vector3(-2, -8, 0), Quaternion.identity);
            else PlayerBorn = Instantiate(Born, new Vector3(2, -8, 0), Quaternion.identity);
            PlayerBorn.GetComponent<Born>().isPlayer = NewPlayerId;
            isDead = false;
        }
    }
    
}
