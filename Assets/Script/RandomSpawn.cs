using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] GameObject zombie;

    void Start()
    {
        //for(int i = 0; i < 10; i++) // 잘되는지 확인 10마리 리스폰
        // 위치 잡아주기.
        Instantiate(zombie, RandomPosition(), Quaternion.identity);
    }
    
    // 백터3를 반환하는 함수
    public Vector3 RandomPosition() // 새로 만들기
    {
        // 원의 방정식
        /*
        // x^2 + y^2 <= r^2
        // 원의 방정식에서 임의의 x랑 z에 해당하는 점이 반지름 r인 원 안에 존재하는 범위입니다.
        // x z 이용해서 방정식 그림. y사용안함

        // 반지름의 길이 

        // x 값으로 랜덤으로 가져올 값의 범위는 -반지름부터 +반지름의 길이까지 입니다.
        // x 0.3^2  + z^2 = 1
        // z = 루트 1^2 -0.3^2
        // z = 루트 1 - 0.09
        // z = 루트 0.91 
        // z = 0.95~~ 근사값

        // 반지름 1인 원의 값으로 (0.3, 0.95)
        */

        // 게임 오브젝트를 중심으로 기준 반지름 100인 원을 설정합니다.
        float radius = 100f;

        // 천 번째로 x값을 계산합니다.
        // 원점을 기준으로 -50, 50 사이의 난수 값을 생성합니다.
        float x = Random.Range(-radius, radius);

        // 방정식            //제공권 함수
        float z = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x, 2));
                                  //제곱근

        // 랜덤으로 0과 1사이의난수값을 생성하고 0이 나오면 음수 형태의 z를 z 변수에
        // 넣어주면 됩니다.

        if(Random.Range(0,2) == 0)
        {
            z = -z;
        }

        return new Vector3(x,0,z); // 뉴 백터 값으로 리턴
    }
  
}
