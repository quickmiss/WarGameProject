
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

        // 카메라 오브젝트 관련 변수
        
        public Transform cameraFollow = null;

        // 캐릭터 이동 관련 속성
        public float Speed = 1; // 걷기 속도
        public float RunSpeedMag = 1.5f; // 달리기 속도는 걷기 속도의 배율
        private float isSpeed;  // 합계 계산 속도값


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

            Rigidbody rigidbody = moveObj.GetComponent<Rigidbody>();
            Vector2 inputValue = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // 달리기  ( 키는 LeftShift )
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSpeed = Speed * RunSpeedMag;
            }
            else isSpeed = Speed;

            bool isMove = inputValue.magnitude != 0;

            if( isMove )    // LookAround 함수로 옮겨야 하는지??
            {
               Vector3 lookForward = new Vector3(cameraFollow.forward.x, 0f, cameraFollow.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraFollow.right.x, 0f, cameraFollow.right.z).normalized;

                charModel.transform.forward = lookForward;
                Vector3 moveDir = lookForward * inputValue.y + lookRight * inputValue.x;

                rigidbody.velocity = moveDir * isSpeed;
            }

            

            
#endif        
        }

        public void LookAround()
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector3 CamAngle = cameraFollow.rotation.eulerAngles;

            float x = CamAngle.x - mouseDelta.y;
            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
            }
            else
            {
                x = Mathf.Clamp(x, 335f, 361f);
            }
            cameraFollow.rotation = Quaternion.Euler(CamAngle.x - mouseDelta.y, CamAngle.y + mouseDelta.x, CamAngle.z);
        }
        
    }

}