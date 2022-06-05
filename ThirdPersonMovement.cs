using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController Controller;


    public float Speed = 6f;
    public float RunSpeed;
    public float WalkSpeed;
    public float Acceleration;
    public float StopAcceleration;
    public Transform Cam;

    public float TurnSmoothTime = 0.1f;
    float TurnSmoothVelocity;
    public float Gravity = -9.81f;
    public float Stamina = 10000;
    public float RunTimer;
    bool WaitBeforeRun;

    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    public float JumpHeight = 3f;
    public Vector3 direction;

    bool isGrouded;
    public bool isAlive = true;


    void Update()
    {
        if (isAlive == true)
        {
            isGrouded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
            if (isGrouded && velocity.y < 0)
            {

            }
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (Input.GetButtonDown("Jump") && isGrouded)
            {
                velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

            velocity.y += Gravity * Time.deltaTime;
            Controller.Move(velocity * Time.deltaTime);

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                Controller.Move(moveDir.normalized * Speed * Time.deltaTime);



                if (Input.GetKey(KeyCode.LeftShift) && Stamina > 0 && RunTimer == 0)
                {




                    Stamina -= Time.deltaTime * 25;
                    Speed = Mathf.Lerp(Speed, RunSpeed, Acceleration * Time.deltaTime);


                }
                else
                {
                    Debug.Log("0");
                    Speed = Mathf.Lerp(Speed, WalkSpeed, Acceleration * Time.deltaTime);
                    if (Stamina < 100)
                        Stamina += Time.deltaTime * 15;
                }
            }






            else
            {
                Speed = Mathf.Lerp(Speed, 0, StopAcceleration * Time.deltaTime);
                if (Stamina < 100)
                    Stamina += Time.deltaTime * 15;
            }
                if (Stamina <= 0)
                {
                    WaitBeforeRun = true;

                }
                if (RunTimer >= 5)
                {
                    WaitBeforeRun = false;
                    RunTimer = 0;

                }
                if (WaitBeforeRun)
                    RunTimer += Time.deltaTime;


            
        }
    }
}

