
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    public class CharacterMovement : MonoBehaviour
    {
        //이동 포함 모든 행동을 관리하는 스크립트

        // 캐릭터 오브젝트 관련 변수
        [SerializeField]
        private GameObject charModel;
        public CharacterProperty property;
      
        // 카메라 오브젝트 관련 변수           나중에 설정 scriptable object 하나 만들 것
        [Space(10f)]
        public Transform cameraFollow = null;
        [Range(1f, 100f)]
        public float camRotSpeed = 5f;
        public float camAngleMax = 70f;   // 위쪽 +
        public float camAngleMin = -25f;   // 아래쪽 -

        // 캐릭터 이동 관련 속성
        [Space(10f)]

        public float RunSpeedMag = 1.5f; // 달리기 속도는 걷기 속도의 배율
        public float isSpeed;  // 합계 계산 속도값
        // 점프
        [Space(5f)]
        public bool isJump = false;
        private float jumpReTime = 1f;

        public Vector2 inputValue;  // 키보드 입력값 ( 애니메이션 전환을 위한 변수 )
        public Rigidbody rigidbody;

        private void Start()
        {
            

        }
        private void FixedUpdate()
        {
            LookAround();
            MoveControl(gameObject, charModel);
        }






        
        public void MoveControl(GameObject moveObj, GameObject charModel)
        {   // 캐릭터 이동 함수
            
#if UNITY_EDITOR_WIN || UNITY_EDITOR

            rigidbody = moveObj.GetComponent<Rigidbody>();
            inputValue = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            

            // 달리기  ( 키는 LeftShift )
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSpeed = property.Speed * RunSpeedMag;
            }
            else isSpeed = property.Speed;

            bool isMove = inputValue.magnitude != 0;

            if( isMove )    // LookAround 함수로 옮겨야 하는지??
            {
               Vector3 lookForward = new Vector3(cameraFollow.forward.x, 0f, cameraFollow.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraFollow.right.x, 0f, cameraFollow.right.z).normalized;

                charModel.transform.forward = lookForward;
                Vector3 LookDir = lookForward * inputValue.y + lookRight * inputValue.x;
                LookDir *= isSpeed;
                Vector3 moveDir = new Vector3(LookDir.x, rigidbody.velocity.y, LookDir.z);  // y값을 0으로 고정하면 점프 AddForce의 물리 값이 0으로 초기화됨
                                                                                           // 그래서 y값을 velocity.y의 현재 값을 할당하면 됨
                
                rigidbody.velocity = moveDir;
            }



            Jump();
#endif        
        }
        public void Jump()
        {
            if (isJump) return;
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

            if (Input.GetKey(KeyCode.Space) && rigidbody)
            {
                rigidbody.AddForce(Vector3.up * property.jumpPower, ForceMode.Impulse);
                //rigidbody.velocity =  Vector3.up * jumpPower;
                this.isJump = true;
                property.HP--;
                print(property.HP);
            }
            
        }
        public void LookAround()
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * camRotSpeed;
            Vector3 CamAngle = cameraFollow.rotation.eulerAngles;

            // ------------------------------------각도 제한 ----------------------------------------------
            float limitx = CamAngle.x - mouseDelta.y;
            if (limitx < 180f)
            {
                limitx = Mathf.Clamp(limitx, -1f, camAngleMax);
            }
            else
            {
                limitx = Mathf.Clamp(limitx, camAngleMin + 360, 361f);
            }

            cameraFollow.rotation = Quaternion.Euler(limitx, CamAngle.y + mouseDelta.x, CamAngle.z);
        }
        public void OnCollisionEnter(Collision collision)
        {
            string tagname = collision.transform.tag.ToString();
            if (tagname == "Ground") this.isJump = false;

        }

    }

}