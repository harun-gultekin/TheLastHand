using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
   public float speed = 5f;
   
   //TODO boundry ekle ve up-down/left-right ayir
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
      
      Vector3 movement = new Vector3(horizontalInput, verticalSpeed, verticalInput);
      movement.Normalize();  

      transform.Translate(movement * speed * Time.deltaTime);
   }
}