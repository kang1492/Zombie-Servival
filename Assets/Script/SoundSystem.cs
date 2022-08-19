using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    // 오디오 소스 + 오디오 클립
    AudioSource audioSource;
    [SerializeField] AudioClip[] clip;

    public static SoundSystem instance; // 전역

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
