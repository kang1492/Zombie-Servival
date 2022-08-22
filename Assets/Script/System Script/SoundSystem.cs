using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    // ����� �ҽ� + ����� Ŭ��
    AudioSource audioSource;
    [SerializeField] AudioClip[] clip;

    public static SoundSystem instance; // ����

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject); // �ڱ� �ڽ� �Ѱ��ֱ� 8-19
    }

    //                ��ȣ�� �����ؼ� ȣ���ϴ°�
    public void Sound(int number)
    {
                    // ���� ȣ��
        audioSource.PlayOneShot(clip[number]);
    }
   
    
}
