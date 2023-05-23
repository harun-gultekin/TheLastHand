using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraneController : MonoBehaviour
{
   public float speed = 5f;
   
   [SerializeField] private Button moveUpButton;
   [SerializeField] private Button moveDownButton;
   [SerializeField] private Button moveRightButton;
   [SerializeField] private Button moveLeftButton;
   [SerializeField] private Button moveForwardButton;
   [SerializeField] private Button moveBackButton;
   
   private void OnEnable()
   {
      moveUpButton.onClick.AddListener(() =>
      { 
         MoveCrane(CraneDirection.Up);
      });
      moveDownButton.onClick.AddListener(() =>
      {
         MoveCrane(CraneDirection.Down);
      });
      moveRightButton.onClick.AddListener(() =>
      {
         MoveCrane(CraneDirection.Right);
      });
      moveLeftButton.onClick.AddListener(() =>
      {
         MoveCrane(CraneDirection.Left);
      });
      moveForwardButton.onClick.AddListener(() =>
      {
         MoveCrane(CraneDirection.Forward);
      });
      moveBackButton.onClick.AddListener(() =>
      { 
         MoveCrane(CraneDirection.Back);
      });
   }
    
   private void OnDisable()
   {
      moveUpButton.onClick.RemoveAllListeners();
      moveDownButton.onClick.RemoveAllListeners();
      moveRightButton.onClick.RemoveAllListeners();
      moveLeftButton.onClick.RemoveAllListeners();
      moveForwardButton.onClick.RemoveAllListeners();
      moveBackButton.onClick.RemoveAllListeners();
   }
   
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

   private void MoveCrane(CraneDirection direction)
   {
      if (direction == CraneDirection.Up || direction == CraneDirection.Down)
      {
         Debug.Log("up down");
      }
      else if (direction == CraneDirection.Left || direction == CraneDirection.Right)
      {
         Debug.Log("left right");
      }
      else if (direction == CraneDirection.Forward || direction == CraneDirection.Back)
      {
         Debug.Log("forward back");
      }
   }
   
   private enum CraneDirection 
   {
      Up,
      Down,
      Right,
      Left,
      Forward,
      Back
   }
}