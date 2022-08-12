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
    }

    public void Sound(int number)
    {
                    // 동시 호출
        audioSource.PlayOneShot(clip[number]);
    }
   
   
}
