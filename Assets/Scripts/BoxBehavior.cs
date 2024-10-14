using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    private Color defaultColor;
    public Color onButtonColor;
    public GameObject gameManager;
    private void Start()
    {
        defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    public void ChangeBoxColor(){
        if(GetComponent<SpriteRenderer>().color == defaultColor){
            GetComponent<SpriteRenderer>().color = onButtonColor;
        } else {
            GetComponent<SpriteRenderer>().color = defaultColor;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Button")){
            ChangeBoxColor();
            gameManager.GetComponent<GameManager>().AddToScore();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Button")){
            ChangeBoxColor();
            if(gameManager != null) gameManager.GetComponent<GameManager>().SubFromScore();
        }
    }
}
