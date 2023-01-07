using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    public float normalSpeed;

    public float fastSpeed;

    public float movementSpeed;
    public float movementTime;

    public Vector3 newPosition;

    public Vector3 zoomAmount;
    private Camera _camera;
    private float zoom_speed = 1f;

    public float rotationAmount;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        _camera = transform.GetChild(0).GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        HandleMovementInput();
    }

    void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (_camera.orthographicSize > 30f && Input.mouseScrollDelta.y > 0)
                _camera.orthographicSize -= Input.mouseScrollDelta.y * zoom_speed;
            else if (_camera.orthographicSize < 60f && Input.mouseScrollDelta.y < 0)
            {
                _camera.orthographicSize -= Input.mouseScrollDelta.y * zoom_speed;
            }
        }

        Vector2 mousePos = Input.mousePosition;
        Debug.Log(mousePos);
        // Calculate screen size, ratio

        if(mousePos.x / Screen.width <= 0.05f )
        {
            if (transform.position.x > -70f && transform.position.z > 300f)
            {
                newPosition += (transform.right + transform.forward).normalized * -movementSpeed;
            }
        }
        else if(mousePos.x / Screen.width >= 0.95f)
        {
            if (transform.position.x < 90f && transform.position.z < 400f)
            {
                newPosition += (transform.right + transform.forward).normalized * movementSpeed;
            }
        }
        if(mousePos.y / Screen.height >= 0.95f)
        {
            if (transform.position.x > -70f && transform.position.z < 400f)
            {
                newPosition += (transform.forward + -transform.right).normalized * movementSpeed;
            }
        }
        else if (mousePos.y / Screen.height <= 0.05f)
        {
            if (transform.position.x < 90f && transform.position.z > 300f)
            {
                newPosition += (transform.forward + -transform.right).normalized * -movementSpeed;
            }
        }
        /*
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }*/
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //x=70 -90 z = 300 400
            if (transform.position.x > -70f && transform.position.z < 400f)
            {
                newPosition += (transform.forward + -transform.right).normalized * movementSpeed;
            }
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.x < 90f && transform.position.z > 300f)
            {
                newPosition += (transform.forward + -transform.right).normalized * -movementSpeed;
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > -70f && transform.position.z > 300f)
            {
                newPosition += (transform.right + transform.forward).normalized * -movementSpeed;
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < 90f && transform.position.z < 400f)
            {
                newPosition += (transform.right + transform.forward).normalized * movementSpeed;
            }
        }

        // if (Input.GetKey(KeyCode.Q))
        // {
        //     newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        // }
        // if (Input.GetKey(KeyCode.E))
        // {
        //     newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        // }

        // if(Input.GetKey(KeyCode.R))
        // {
        //     newZoom+=zoomAmount;
        // }
        // if(Input.GetKey(KeyCode.F))
        // {
        //     newZoom-=zoomAmount;
        // }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        //transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}