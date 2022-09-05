using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    // 오디오 소스 + 오디오 클립
    AudioSource audioSource;
    [SerializeField] AudioClip[] clip;

    public static SoundSystem instance; // 전역

    private void Awake() // 이벤트 함수 9-5
    {
        // 내가 찾는 오브젝트의 타입이 SoundSystem 이라면
        // var 자료형 추론 (어떤 타입이든 다 저장이 되는 자료형입니다.)
        var soundObject = FindObjectsOfType<SoundSystem>();

        // soundObject의 갯수가 한 개 라면
        if(soundObject.Length > 1) // 비교
        {
            // 자기 자신을 파괴해서 오직 하나의 SoundSystem 오브젝트만 남길 수 있도록 합니다.
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject); // 자기 자신 넘겨주기 8-19
    }

    //                번호에 참조해서 호출하는것
    public void Sound(int number)
    {
                    // 동시 호출
        audioSource.PlayOneShot(clip[number]);
    }
   
    
}
