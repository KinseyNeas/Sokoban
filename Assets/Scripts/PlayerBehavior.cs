using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class PlayerBehavior : MonoBehaviour
{
    private Vector2 movement;
    public float speed;
    public Transform targetPosition;
    public LayerMask WallLayer;
    public LayerMask BoxLayer;
    private Animator animator;
    private void Awake() {
        targetPosition.position = transform.position;
        animator = gameObject.GetComponent<Animator>();
    }
    private void Update() {
        UpdatePlayerPosition();
    }
    private void UpdatePlayerPosition(){
        if(PlayerIsMoving() && !ThereIsWall()) {
            if(ThereIsBox()) {
                if(!ThereIsWallNextToBox()) { // Move the player only if there is no wall next to box
                    targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
                }
            } else { // Move the player if there is no box or wall
                targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
    }
    public void OnMove(InputValue value){
        movement = value.Get<Vector2>();
         if(movement != Vector2.zero) {
             if(!animator.GetBool("Walk")) animator.SetBool("Walk", true);
             transform.up = movement;
         } else animator.SetBool("Walk", false);
    }
    private bool PlayerIsMoving(){
        return Vector3.Distance(transform.position, targetPosition.position) <= .01f;
    }
    private bool ThereIsWall(){
        return Physics2D.OverlapCircle(targetPosition.position + new Vector3(movement.x, movement.y, 0f), .01f, WallLayer);
    }
    private bool ThereIsBox(){
        return Physics2D.OverlapCircle(targetPosition.position + new Vector3(movement.x, movement.y, 0f), .01f, BoxLayer);
    }
    private bool ThereIsWallNextToBox(){
        return Physics2D.OverlapCircle(targetPosition.position + new Vector3(movement.x*2, movement.y*2, 0f), .01f, WallLayer);
    }
}
