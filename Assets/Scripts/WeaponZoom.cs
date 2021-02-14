using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    private Camera cam;
    private float startingFOV;
    [SerializeField] float zoomFOV = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        startingFOV = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) {
            ZoomIn();
        };

        if (Input.GetButtonUp("Fire2")) {
            ZoomOut();
        };
    }

    private void ZoomIn() {
        cam.fieldOfView = zoomFOV;
    }
    private void ZoomOut() {
        cam.fieldOfView = startingFOV;
    }   
}
