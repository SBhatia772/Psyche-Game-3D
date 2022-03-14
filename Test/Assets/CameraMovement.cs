using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform ship;
    [SerializeField] private float distanceToShip = 10;
    
    private Vector3 lastPosition;


    private void Start()
    {
        camera.transform.position = new Vector3(0, 0, -distanceToShip);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            camera.transform.position = new Vector3(0, 0, -distanceToShip);

            Vector3 currentPosition = camera.ScreenToViewportPoint(Input.mousePosition);

            Vector3 direction = lastPosition - currentPosition;

            float rotationAroundYAxis = -direction.x * 180;

            float rotationAroundXAxis = direction.y * 180;

            camera.transform.position = ship.position;

            camera.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);

            camera.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

            camera.transform.Translate(new Vector3(0, 0, -distanceToShip));

            lastPosition = currentPosition;
        }

        else
        {
            camera.transform.position = ship.position;
            camera.transform.Translate(new Vector3(0, 0, -distanceToShip));
        }
    }
}
