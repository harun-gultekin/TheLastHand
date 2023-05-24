using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
   [SerializeField] private GameObject verticalMoveObject;
   [SerializeField] private GameObject horizontalMoveObject;
   [SerializeField] private GameObject leftRightMoveObject;

   [SerializeField] private float speed = 5f;
   private float _verticalSpeed;

   void Update()
   {
      //float horizontalInput = Input.GetAxis("Horizontal");
      //float verticalInput = Input.GetAxis("Vertical");

      //Vector3 movement = new Vector3(horizontalInput, _verticalSpeed, verticalInput);
      //movement.Normalize();  
      //transform.Translate(movement * speed * Time.deltaTime);
   }

   public void MoveUp()
   {
      _verticalSpeed = 1f;
      Vector3 movementDown = new Vector3(transform.position.x, _verticalSpeed, transform.position.z);
      movementDown.Normalize();
      transform.Translate(movementDown * speed * Time.deltaTime);
   }

   public void MoveDown()
   {
      _verticalSpeed = -1f;
      Vector3 movementDown = new Vector3(transform.position.x, _verticalSpeed, transform.position.z);
      movementDown.Normalize();
      transform.Translate(movementDown * speed * Time.deltaTime);
   }
   
   public void MoveRight()
   {
      float horizontalInput = Input.GetAxis("Horizontal");
      float verticalInput = Input.GetAxis("Vertical");

      Vector3 movement = new Vector3(horizontalInput, transform.position.y, verticalInput);
      movement.Normalize();  
      transform.Translate(movement * speed * Time.deltaTime);
   }
   
   public void MoveLeft()
   {
     
   }
   
   public void MoveForward()
   {
     
   }
   
   public void MoveBack()
   {
     
   }
}