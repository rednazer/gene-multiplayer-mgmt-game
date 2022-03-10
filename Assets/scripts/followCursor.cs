using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCursor : MonoBehaviour
{
    Vector3 lastMousePos;

    // Start is called before the first frame update
    void Start()
    {
        lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 movementVector = mousePos - lastMousePos;

        gameObject.transform.position += movementVector;
        lastMousePos = mousePos;
    }
}
