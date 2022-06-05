using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCode : MonoBehaviour
{
    public GameObject Grab;
    public GameObject AimUi;
    public bool AnimIsPlaying = false;
    [SerializeField] CollectItem CollectScript2;
  
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            
            Grab.GetComponent<Animator>().Play("Grabbing");
        }
        
        
       
       
            if (Input.GetMouseButtonDown(0))
            {


                AnimIsPlaying = true;
                AimUi.SetActive(true);
                Grab.GetComponent<Animator>().Play("BowArrow");
                if (Input.GetMouseButtonUp(0))
                {

                    GetComponent<Animator>().SetBool("ArrowStop", true);
                    AnimIsPlaying = false;
                    AimUi.SetActive(false);
                }
            }
      
       
      
       
    }
    
}
