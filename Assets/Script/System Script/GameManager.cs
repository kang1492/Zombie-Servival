using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    static public GameManager instance; // 어디서든 접속 할수 있개

    public int count;
    public GameObject resultScreen;
    [SerializeField] Text playTime;
    [SerializeField] Text kill;

    void Start()
    {
        Time.timeScale = 1; // 게임을 다시 시작하면 초기화가 됨
        instance = this;
    }

    
    void Update()
    {
        // Time.time : 게임이 실해되고 나서 부터 걸린 시간
        playTime.text = "Game Running Time : " + Time.time.ToString("N2");
        kill.text = "Zombie Kill : " + count.ToString();
    }
}
