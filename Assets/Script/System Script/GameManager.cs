using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    static public GameManager instance; // ��𼭵� ���� �Ҽ� �ְ�

    public int count;
    public GameObject resultScreen;
    [SerializeField] Text playTime;
    [SerializeField] Text kill;

    void Start()
    {
        Time.timeScale = 1; // ������ �ٽ� �����ϸ� �ʱ�ȭ�� ��
        instance = this;
    }

    
    void Update()
    {
        // Time.time : ������ ���صǰ� ���� ���� �ɸ� �ð�
        playTime.text = "Game Running Time : " + Time.time.ToString("N2");
        kill.text = "Zombie Kill : " + count.ToString();
    }
}
