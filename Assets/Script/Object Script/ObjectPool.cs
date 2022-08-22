using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public RandomSpawn spawn;
    public static ObjectPool instance; // ��ü �����

    // ObjectPool�� ���� �� �ִ� ���� ������Ʈ�� �����մϴ�.
    [SerializeField] GameObject zombie;

    // ���� ������Ʈ�� ���� �� �ִ� �ڷᱸ�� Queue�� �����մϴ�.
    public Queue<GameObject> queue = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Ŭ������ �־��ִ°�
        // static ��ü�� ����ϱ� ���� ObjectPool�� instance ������ �־��ݴϴ�.

        for(int i = 0; i < 20; i++)// ���� �ӵ� ������ 20�� ���� �����
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

    // ���� ������ ��ť
    // ĳ���� ���� �޷����� << �μ�Ʈ ť�� �ٽ� ���� �־�� ��.
    public void InsertQueue(GameObject tobj)
    {
        queue.Enqueue(tobj);
        tobj.SetActive(false);
    }

    // ť���� ������ �����ؾ� �Ǵ°�
    // ��ȯ ���ӿ�����Ʈ �ؾ� �Ǵ°�
    public GameObject GetQueue()
    {
        GameObject tempZombie = queue.Dequeue();
        tempZombie.SetActive(true);

        return tempZombie;
    }
    
}
