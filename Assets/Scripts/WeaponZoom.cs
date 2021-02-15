using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] float defaultFOV = 60.0f;
    [SerializeField] float defaultSensitivity = 2.0f;
    [SerializeField] float zoomFOV = 30.0f;
    [SerializeField] float zoomSensitivity = 0.5f;
    private Camera cam;
    private RigidbodyFirstPersonController fpsController;



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();
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

    public void ZoomIn() {
        cam.fieldOfView = zoomFOV;
        fpsController.mouseLook.XSensitivity = zoomSensitivity;
        fpsController.mouseLook.YSensitivity = zoomSensitivity;
    }
    public void ZoomOut() {
        cam.fieldOfView = defaultFOV;
        fpsController.mouseLook.XSensitivity = defaultSensitivity;
        fpsController.mouseLook.YSensitivity = defaultSensitivity;
    }   
}
