using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    // ����� �ҽ� + ����� Ŭ��
    AudioSource audioSource;
    [SerializeField] AudioClip[] clip;

    public static SoundSystem instance; // ����

    private void Awake() // �̺�Ʈ �Լ� 9-5
    {
        // ���� ã�� ������Ʈ�� Ÿ���� SoundSystem �̶��
        // var �ڷ��� �߷� (� Ÿ���̵� �� ������ �Ǵ� �ڷ����Դϴ�.)
        var soundObject = FindObjectsOfType<SoundSystem>();

        // soundObject�� ������ �� �� ���
        if(soundObject.Length > 1) // ��
        {
            // �ڱ� �ڽ��� �ı��ؼ� ���� �ϳ��� SoundSystem ������Ʈ�� ���� �� �ֵ��� �մϴ�.
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

        DontDestroyOnLoad(gameObject); // �ڱ� �ڽ� �Ѱ��ֱ� 8-19
    }

    //                ��ȣ�� �����ؼ� ȣ���ϴ°�
    public void Sound(int number)
    {
                    // ���� ȣ��
        audioSource.PlayOneShot(clip[number]);
    }
   
    
}
