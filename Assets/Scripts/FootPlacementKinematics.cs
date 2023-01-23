using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPlacementKinematics : MonoBehaviour
{
    private Animator animator;

    public LayerMask layerMask;

    [Range(0,1f)]
    public float distanceFromFloor;
    
    //[SerializeField]
    //private float rightWeight, leftWeight = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        distanceFromFloor = 0.13f;
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    //Funzione richiamata ogni frame se l'animator ha la funzionalità IK attiva
    private void OnAnimatorIK(int layerIndex){
        if(animator)
        {
            //Feet Position
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);

            //FeetRotation
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            RaycastHit hit;
            //Left foot
            Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if(Physics.Raycast(ray, out hit, distanceFromFloor + 1f, layerMask)){
                if(hit.transform.tag == "Walkable"){
                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceFromFloor;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
            
            //right foot
            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if(Physics.Raycast(ray, out hit, distanceFromFloor + 1f, layerMask)){
                if(hit.transform.tag == "Walkable"){
                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceFromFloor;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }

        }  
    }
}
