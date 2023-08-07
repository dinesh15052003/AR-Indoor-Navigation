using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 5f;
    private GameObject obj;

    public float rotationSpeed = 50f;

    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (obj == null)
        {
            obj = GameObject.FindGameObjectWithTag("Environment");
        }
        if (isRotatingLeft)
        {
            RotateLeft();
        }
        if (isRotatingRight)
        {
            RotateRight();
        }
    }

    public void MoveLeft()
    {
        // Move the object to the left
        obj.transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }

    public void MoveRight()
    {
        // Move the object to the right
        obj.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }

    public void MoveUp()
    {
        // Move the object up
        obj.transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
    }

    public void MoveDown()
    {
        // Move the object down
        obj.transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
    }

    public void MoveFront()
    {
        // Move the object forward
        obj.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    public void MoveBack()
    {
        // Move the object backward
        obj.transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
    }

   

    public void StartRotatingLeft()
    {
        isRotatingLeft = true;
    }

    public void StopRotatingLeft()
    {
        isRotatingLeft = false;
    }

    public void StartRotatingRight()
    {
        isRotatingRight = true;
    }

    public void StopRotatingRight()
    {
        isRotatingRight = false;
    }

    private void RotateLeft()
    {
        float deltaRotation = rotationSpeed * Time.deltaTime;
        obj.transform.Rotate(Vector3.up, -deltaRotation);
    }

    private void RotateRight()
    {
        float deltaRotation = rotationSpeed * Time.deltaTime;
        obj.transform.Rotate(Vector3.up, deltaRotation);
    }
}
