using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    private Camera cam;
    private float startingFOV, startingXSensitivity, startingYSensitivity;
    private RigidbodyFirstPersonController fpsController;
    [SerializeField] float zoomFOV = 30.0f;
    [SerializeField] float zoomXSensitivity = 0.5f;
    [SerializeField] float zoomYSensitivity = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();

        startingFOV = cam.fieldOfView;
        startingXSensitivity = fpsController.mouseLook.XSensitivity;
        startingYSensitivity = fpsController.mouseLook.YSensitivity;      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire2") == 1) {
            ZoomIn();
        }

        if (Input.GetAxis("Fire2") == 0) {
            ZoomOut();
        }
    }

    private void ZoomIn() {
        cam.fieldOfView = zoomFOV;
        fpsController.mouseLook.XSensitivity = zoomXSensitivity;
        fpsController.mouseLook.YSensitivity = zoomYSensitivity;
    }
    private void ZoomOut() {
        cam.fieldOfView = startingFOV;
        fpsController.mouseLook.XSensitivity = startingXSensitivity;
        fpsController.mouseLook.YSensitivity = startingYSensitivity;
    }   
}
