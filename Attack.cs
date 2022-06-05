using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float degreesPerSecond;
    [SerializeField] float speed;
    [SerializeField] CharacterController Controller;
    [SerializeField] float TimeToAttack;
    [SerializeField] float TimeToStopAttacking;
    [SerializeField] CollectItem SkipButtom;
    public Animator PlayerAnimator;
    public Animator WolfDie;
    public bool IsDead = false;
    public ThirdPersonMovement CheckIfAlive;
    
    

    float Timer;
    void Update()
    {

        if (IsDead == false)
        {
            Vector3 dirFromeMeToTarget = player.transform.position - transform.position;
            Quaternion LookRotation = Quaternion.LookRotation(dirFromeMeToTarget);

            transform.rotation = Quaternion.Lerp(transform.rotation, LookRotation, degreesPerSecond * Time.deltaTime);

            Controller.Move(transform.forward * speed * Time.deltaTime);
            if (Timer > 0)
            {
                degreesPerSecond = 3;
            }
            else
            {
                degreesPerSecond = 1;
            }
            Timer -= Time.deltaTime;
            if (Timer <= TimeToStopAttacking)
            {
                Timer = TimeToAttack;
            }
        }
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
       if (other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<Animator>().SetTrigger("Die");
           


            WolfDie.SetTrigger("Normal");
            WolfDie.ResetTrigger("RunAttack");
        }
        if (other.gameObject.tag == "Arrow1")
        {
            IsDead = true;
            WolfDie.SetTrigger("Die");
            WolfDie.ResetTrigger("RunAttack");

        }

    }
    public void Skip()
    {
        
        SkipButtom.WolfAttack.SetActive(false);
        SkipButtom.LoseCanvas.SetActive(false);
        PlayerAnimator.SetTrigger("Alive");
        PlayerAnimator.ResetTrigger("Die");
        CheckIfAlive.isAlive = true;
        
        

    }
}
