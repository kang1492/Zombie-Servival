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

#elif UNITY_WEBGL // 웹질에서 이렇개 종료해야 됨. //8-17
         Application.OpenURL("http://google.com");// 사이트 주소 남기기 8-17

//#else
         // Application.Quit(); // 웹진 종료 적용 안됨.pc에서는 종료 됨
        // <- exe, Android, 됨. WebPlayer(안먹힘) 
        // 다른 방식이 필요
#endif

    }

}
