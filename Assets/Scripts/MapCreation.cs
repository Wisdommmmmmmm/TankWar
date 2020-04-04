using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {
    //预制体列表
    //0：家 1：墙 2：障碍 3：河流 4：草 5：空气墙 6：出生特效
    public GameObject[] item;

    private HashSet<Vector3> PositionSet = new HashSet<Vector3>();
    private void Awake() {
        //实例化家
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);

        //用墙围家
        CreateItem(item[1], new Vector3(0, -7, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(-1, -7, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -7, 0), Quaternion.identity);

        //实例化空气墙
        for (int i = -10; i <= 10; ++i) {
            CreateItem(item[5], new Vector3(i, 9, 0), Quaternion.identity);
            CreateItem(item[5], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i <= 8; ++i) {
            CreateItem(item[5], new Vector3(11, i, 0), Quaternion.identity);
            CreateItem(item[5], new Vector3(-11, i, 0), Quaternion.identity);
        }

        //实例化玩家和敌人的出生特效
        //玩家
        GameObject PlayerBorn = Instantiate(item[6], new Vector3(-2, -8, 0), Quaternion.identity);
        PositionSet.Add(new Vector3(-2, -8, 0));
        PlayerBorn.GetComponent<Born>().isPlayer = 1;
        //双人模式，两个玩家
        if (tag == "Scene02") {
            PlayerBorn = Instantiate(item[6], new Vector3(2, -8, 0), Quaternion.identity);
            PositionSet.Add(new Vector3(2, -8, 0));
            PlayerBorn.GetComponent<Born>().isPlayer = 2;
        }
        //敌人
        CreateItem(item[6], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[6], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[6], new Vector3(10, 8, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy", 10, 10);

        //实例化墙、障碍、河流、草,墙多一点
        for (int i = 0; i < 50; ++i) {
            CreateItem(item[1], CreatePosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; ++i) {
            CreateItem(item[2], CreatePosition(), Quaternion.identity);
            CreateItem(item[3], CreatePosition(), Quaternion.identity);
            CreateItem(item[4], CreatePosition(), Quaternion.identity);
        }
    }
    private void CreateItem(GameObject Prefab, Vector3 Position, Quaternion Rotation) {
        GameObject NewItem = Instantiate(Prefab, Position, Rotation);
        NewItem.transform.parent = gameObject.transform;
        PositionSet.Add(Position);
    }
    //随机生成位置
    private Vector3 CreatePosition() {
        while(true) {
            Vector3 NewPosition = new Vector3(Random.Range(-9, 10), Random.Range(-9, 10), 0);
            if (PositionSet.Contains(NewPosition)) continue;
            return NewPosition;
        }
    }
    //从三个位置中随机生成一个敌人
    private void CreateEnemy() {
        int x = Random.Range(0, 3);
        if(x == 0) CreateItem(item[6], new Vector3(-10, 8, 0), Quaternion.identity);
        else if(x == 1) CreateItem(item[6], new Vector3(0, 8, 0), Quaternion.identity);
        else CreateItem(item[6], new Vector3(10, 8, 0), Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
