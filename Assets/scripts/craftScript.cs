using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftScript : MonoBehaviour
{
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void click() {
        gameManager.GetComponent<gamemanager>().craft();
    }
}
