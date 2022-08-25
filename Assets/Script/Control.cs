using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public int health = 100; //8-23 
    [SerializeField] float axisSpeed = 5.0f; // 카메라 x축과 y축의 회전 속도
    [SerializeField] GameObject eye;


    private float eulerAngleX;
    private float eulerAngleY;

    private CharacterController characterControl;
    private Vector3 moveForce;
    [SerializeField] float distance = 100.0f; //8-18 광선이 100 미터 까지 발사 된다.
    [SerializeField] float speed;
    [SerializeField] float gravity = 20.0f;
    [SerializeField] ParticleSystem effect;
    //[SerializeField] GameObject bullet; // 총알 만들기 8-18 총알 삭제
    [SerializeField] LayerMask layer; // 8-18
    

    void Start()
    {
        // 커서 비활성화
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        characterControl = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        UpdateRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if(Input.GetButtonDown("Fire1"))
        {
            effect.Play();
            SoundSystem.instance.Sound(0); // 8-12 총소리
            TwoStepRay(); //8-18, 호출하기
            //Instantiate(bullet, effect.transform.position, effect.transform.rotation); 8-18 지워도 괜찬음
            // 총구 방향
        }

        

        MoveTo
            (
                 new Vector3
                 (
                     Input.GetAxis("Horizontal"), 
                     0, 
                     Input.GetAxis("Vertical")
                  )
             );

        // 바닥과 출돌하지 않았다면
        if(characterControl.isGrounded == false)
        {
            // 중력을 받게 만듬
            moveForce.y -= gravity * Time.deltaTime;
        }
        //else 점프 하기 , 바닥에 닫지 않았다면 중력을 받게 만듬.
        //{
            //moveForce.y = 0.1f;
        //}

        characterControl.Move(moveForce * Time.deltaTime);

        Jump(); //8-19 점프
    }

    public void Jump() // 8-19
    {
        // 스페이스 누르면 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 땅바닥에 다였을때 점프하기
            if (characterControl.isGrounded)
            {
                //Debug.Log("점프"); // 확인하기
                
                // 점프를 할 수 있도록 설정합니다.
                moveForce.y = 7.5f;
                // y 축으로 7.5 이동
            }
        }
    }

    public void MoveTo(Vector3 direction)
    {
        // 카메라 회전으로 전방 방향이 변하기 때문에 회전 값을 곱해서 연산합니다.
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        // 위/ 아래를 바라보고 이동할 때 캐릭터 오브젝트가 공중으로 이동하거나 아래로 가라앉기 때문에
        // direction을 그대로 사용하지 않고, moveForce 변수에 x와 z값만 넣어서 사용합니다.
        moveForce = new Vector3(direction.x * speed, moveForce.y, direction.z * speed);
    }

    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * axisSpeed; // 마우스 좌/우 이동으로 카메라 y축 회전

        // 마우스 아래로 내리면 -로 음수인대 오브젝트의 x축이 + 방향으로 회전해야 아래를 보기 때문입니다.
        eulerAngleX -= mouseY * axisSpeed; // 마우스 위/아래 이동으로 카메라 x축 회전

        // 카메라 x축 회전의 경우 외전 범위를 설정합니다. 위로 360 회전됨. 아래 위로 각도 제한.
        eulerAngleX = ClampAngle(eulerAngleX, -80, 50);

        //transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
        transform.rotation = Quaternion.Euler(transform.rotation.x, eulerAngleY, 0); // 개별적 처리

        eye.transform.rotation = Quaternion.Euler(eulerAngleX, transform.eulerAngles.y, 0);
    }

    public float ClampAngle(float angle, float min, float max)
    {// float 반환값 필요
        return Mathf.Clamp(angle, min, max);
    }

    public void TwoStepRay() //8-18
    {   
        // 광선이 있어야 됨. 선언함
        Ray ray;

        // 붙힏힌 녀석의 정보가 필요함
        RaycastHit hit;
        Vector3 target = Vector3.zero;

        // 화면의 중앙 좌표 (Cross Hair를 기준으로 Raycast를 연산합니다.)

        // ray 필요함                        원점을 쏘기 때문에
        ray = Camera.main.ViewportPointToRay(Vector2.one * 0.5f);

        // 총알이 허공에 발사 되었을때 예외 처리 필요
        // 공격 사거리 안에 부딛히는 오브젝트가 있으면 target은 광선에 부딪힌 위치로 설정합니다.
                        // 광선 넣어주고  광선이 100미터 까지 발사 된다
        if(Physics.Raycast(ray, out hit, distance))
        {
            // hit = 물체의 위치를 알수 잇음
            target = hit.point;
            Instantiate(effect, hit.point, hit.transform.rotation); // 8-22
            //          임펙트   명중. 위치 / 장애물에도 총쏘면 임펙트 생김

        }

        // 공격 사거리 안에 부딪히는 오브젝트가 없으면 targer 포인터는 최대 사거리의 위치로 설정합니다.
        else // 허공에 쐈을때
        {
                    // 기준점 .   방향..           위치저장
            target = ray.origin + ray.direction * distance;
        }

        // 확인하기
        //Debug.Log(target); 확인후 지우기

        // 첫 번째 Raycast 연산으로 얻어진 targer의 정보를 목표지점으로 설정하고,
        // 총구 입구에서 Raycast를 발사합니다.

        //                           - 총구 위치 빼기 (파티클 위치 가져오기)
        Vector3 direction = (target - effect.transform.position).normalized;

        //         광선넣기( 광선 위치                 방향                           여기에 추가          
        if(Physics.Raycast(effect.transform.position, direction, out hit, distance, layer))
        {
            //if(hit.collider == null) // 아무대나 쏘면 에러 나는 예외 처리
            //{
            //    return;
            //}

            // 충돌한 물체에
            hit.collider.GetComponentInParent<Zombie>().health -=20;

            //hit.collider.GetComponentInParent<Zombie>().Death(); //8-25 지우기

            // 게임신 에서 확인 가능                                         색깔 지정  , 색 10초간 보이게
            //Debug.DrawLine(effect.transform.position, direction * distance, Color.red, 10); 확인후 지우기

            //Instantiate(effect, hit.transform.position, hit.transform.rotation);// 총구 임펙트 생성
            // 총 쏜 위치에다 파티클 생성됨.

        }
    }



}
