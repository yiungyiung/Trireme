using UnityEngine;
 
 public class Camerafollow : MonoBehaviour
 {public float interpVelocity;
     public Transform playerTransform;
     public int depth = 20;
     public float minDistance;
    public float followDistance;
    public Vector3 offset;
    Vector3 targetPos;
    GameObject playerObject;
    public int iv;
     // Update is called once per frame
     void FixedUpdate()
     {
         if(playerTransform != null)
         {
             playerObject = GameObject.FindWithTag("Player");
             //Debug.Log("Rotation: " + playerTransform.rotation.z); //this is the value of the object's rotation

             if(Mathf.Abs(playerTransform.rotation.z) >= 0.9  ){
                 offset.x = -10f;
                 offset.y = 0f;             }
             else if(Mathf.Abs(playerTransform.rotation.z) <= 0.35){

                 offset.x = 10f;
                 offset.y = 0f;            }
             else{
                 if(playerTransform.rotation.z > 0){

                     offset.x = 0f;
                     offset.y = 3.5f;
                 }
                 else if(playerTransform.rotation.z < 0){
                     offset.x = 0f;
                     offset.y = -3.5f;
                 }
             }
             Vector3 posNoZ = transform.position;
            posNoZ.z = playerTransform.transform.position.z;

            Vector3 targetDirection = (playerTransform.position + offset - posNoZ);

            interpVelocity = targetDirection.magnitude * 8f;
            iv = (int)interpVelocity;
            
            

            targetPos = transform.position + (targetDirection.normalized* interpVelocity * Time.deltaTime); 

            transform.position = Vector3.Lerp( transform.position, targetPos , 0.25f);
         }
     }
 
     public void setTarget(Transform target)
     {
         playerTransform = target;
     }
 }