using UnityEngine;

public class VersatileButton : MonoBehaviour
{
    public void Scene(string name)
    {
        Loading.LoadScene(name);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#elif UNITY_WEBGL // �������� �̷��� �����ؾ� ��. //8-17
         Application.OpenURL("http://google.com");// ����Ʈ �ּ� ����� 8-17

//#else
         // Application.Quit(); // ���� ���� ���� �ȵ�.pc������ ���� ��
        // <- exe, Android, ��. WebPlayer(�ȸ���) 
        // �ٸ� ����� �ʿ�
#endif

    }

}
