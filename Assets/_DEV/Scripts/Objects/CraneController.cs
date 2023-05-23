using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
   public float speed = 5f;

   [SerializeField] private GameObject verticalMoveObject;
   [SerializeField] private GameObject horizontalMoveObject;
   
   //TODO separate movement !!
   void Update()
   {
      float horizontalInput = Input.GetAxis("Horizontal");
      float verticalInput = Input.GetAxis("Vertical");
      
      float verticalSpeed = 0f;

      if (Input.GetKey(KeyCode.LeftShift))
      {
         verticalSpeed = -1f;  
      }
        
      else if (Input.GetKey(KeyCode.LeftControl))
      {
         verticalSpeed = 1f;   
      }

      Vector3 movementVertical = new Vector3(horizontalInput, verticalSpeed, verticalInput);
      Vector3 movementHorizontal = new Vector3(horizontalInput, horizontalMoveObject.transform.position.y, horizontalMoveObject.transform.position.z);

      movementVertical.Normalize();  
      movementHorizontal.Normalize();  

      verticalMoveObject.transform.Translate(movementVertical * speed * Time.deltaTime);
      horizontalMoveObject.transform.Translate(movementHorizontal * speed * Time.deltaTime);
   }
}