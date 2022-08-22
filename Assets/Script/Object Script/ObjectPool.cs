using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public RandomSpawn spawn;
    public static ObjectPool instance; // 객체 만들기

    // ObjectPool에 담을 수 있는 게임 오브젝트를 설정합니다.
    [SerializeField] GameObject zombie;

    // 게임 오브젝트를 담을 수 있는 자료구조 Queue를 선언합니다.
    public Queue<GameObject> queue = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // 클래스를 넣어주는것
        // static 객체를 사용하기 위해 ObjectPool를 instance 변수에 넣어줍니다.

        for(int i = 0; i < 20; i++)// 좀비 속도 느려서 20개 정도 만들기
        {
            GameObject tempPrefab = Instantiate(zombie, spawn.RandomPosition(), Quaternion.identity);
            queue.Enqueue(tempPrefab);
            tempPrefab.SetActive(false);
        }
    }

    public Vector3 ActivePostion() //8-22
    {
        return spawn.RandomPosition();
    }

    // 좀비 꺼내기 디큐
    // 캐릭터 한태 달려오기 << 인설트 큐에 다시 갔다 넣어야 됨.
    public void InsertQueue(GameObject tobj)
    {
        queue.Enqueue(tobj);
        tobj.SetActive(false);
    }

    // 큐에서 꺼내서 생성해야 되는것
    // 반환 게임오브젝트 해야 되는것
    public GameObject GetQueue()
    {
        GameObject tempZombie = queue.Dequeue();
        tempZombie.SetActive(true);

        return tempZombie;
    }
    
}
