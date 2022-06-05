using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<Animator>().SetTrigger("Fall");
        }
    }
}
