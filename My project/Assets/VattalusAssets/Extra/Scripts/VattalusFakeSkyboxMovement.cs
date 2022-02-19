﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VattalusFakeSkyboxMovement : MonoBehaviour
{
    //Since actual spaceship movement is not implemented in this demo, we move the entire environment instead. We basically read the movement inputs from the spaceship and mirror the movements on the environment instead.
    //It is important that all other cameras should have the ClearFlags set to "Don't clear" or "Depth only" and they should have a Depth value greater than the env camera.

    public VattalusSceneController sceneController;
    public Transform dirLightParent;
    private Quaternion dirLightInitRot;
    private Camera envCam;
    private Rigidbody rb;

    void Start()
    {
        envCam = GetComponentInChildren<Camera>();

        //Check rigidbody, add one if not present. The rigidbody is required to simulate realistic rotations
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = 100f;
            rb.angularDrag = 0.05f;
        }
        rb.useGravity = false;

        //also add a sphere collider if missing
        if (gameObject.GetComponent<SphereCollider>() == null)
        {
            SphereCollider col = gameObject.AddComponent<SphereCollider>();
            col.radius = 0.5f;
        }

        if (envCam == null) Debug.Log("<color=#FF0000>VattalusAssets: [FakeSkyboxMovement] Missing camera component reference</color>");
        if (sceneController == null) Debug.Log("<color=#FF0000>VattalusAssets: [FakeSkyboxMovement] Missing SceneController reference</color>");


        //save the initial rotation of the light source, so it doest get reset when the scene starts
        dirLightInitRot = dirLightParent.rotation;
    }


    void LateUpdate()
    {
        if (envCam != null && rb != null)
        {
            //mirror the orientation and FOV of the main camera
            envCam.transform.rotation = rb.transform.rotation * Camera.main.transform.rotation;
            envCam.fieldOfView = Camera.main.fieldOfView;
        }

        //rotate the light source
        dirLightParent.rotation = Quaternion.Inverse(transform.rotation) * dirLightInitRot;

    }

    void FixedUpdate()
    {
        ////Read the spaceship's movement inputs, and mirror them on the environment to create fake movement
        rb.AddRelativeTorque(
            sceneController.spaceshipController.pitchInput * -sceneController.spaceshipController.pitchThrust * Time.deltaTime,
            sceneController.spaceshipController.yawInput * sceneController.spaceshipController.yawThrust * Time.deltaTime,
            sceneController.spaceshipController.rollInput * -sceneController.spaceshipController.rollThrust * Time.deltaTime
        );
    }
}
