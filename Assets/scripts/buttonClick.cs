using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonClick : MonoBehaviour
{
    // The gameManager gamObj
    public GameObject gameManager;

    // The prefab for a cursor follower item
    public GameObject cursorFollowerPrefab;

    // The item held (for purpose of inventory)
    public GameObject heldItem;

    // Update is called once per frame
    void Update()
    {
        
    }

    // Checks if an item is following thecursor. If none, creates one
    // If there is one, does nothing (game manager will decide if it will delete)
    public void selectButton() {
        if(gameManager.GetComponent<gamemanager>().selectedItem == null) {
            if (heldItem != null) {
                GameObject cursorFollower = Instantiate(cursorFollowerPrefab, gameObject.transform.position, cursorFollowerPrefab.transform.rotation);
                cursorFollower.GetComponent<SpriteRenderer>().sprite = gameObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite;
                
                gameManager.GetComponent<gamemanager>().buttonTrigger(gameObject, cursorFollower);
            }
        } else {
            
            gameManager.GetComponent<gamemanager>().buttonTrigger(gameObject, null);


        }
    }

    // Swaps item locations
    public void setItem(GameObject obj) {
        heldItem = obj;
        if (obj == null) {
            Transform i = gameObject.transform.GetChild(0);
            i.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            i.gameObject.GetComponent<Image>().sprite = null;
        } else {
            Transform i = gameObject.transform.GetChild(0);
            i.gameObject.GetComponent<Image>().sprite = obj.GetComponent<item>().sprite;
            i.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }

    // This no work
    public void dim() {
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 20);
    }
}
