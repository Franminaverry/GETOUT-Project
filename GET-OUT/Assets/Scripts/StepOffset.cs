using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepOffset : MonoBehaviour
{
    [SerializeField] private float stepHeight = 0.5f;
    private Vector3 prevVel;
    private bool allowStepOffset;
    private FirstPersonController player;

    private void Start() {
        player = FirstPersonController.Instance;
    }
    private void OnCollisionEnter(Collision collision) {
        if (player.IsWalking()) {
            if (collision.gameObject.layer == 3) {
                allowStepOffset = true;
            }
        }
    }
    private void OnCollisionStay(Collision collision) {
       if (allowStepOffset) {
           player.transform.position += new Vector3(0f,stepHeight,0f);
           allowStepOffset = false;

       }
    }
   
        
    
}
