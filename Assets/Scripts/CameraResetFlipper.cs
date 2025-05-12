using System;
using Oculus.Interaction;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject CameraRig;
    void Start()
    {
        
    }

    // Update is called once per frame

    // Update should be fast enough hopefully
    void Update()
    {
        // Debug.Log("[CS135 Lab2] Current pos: " + CameraRig.eulerAngles);
        // Debug.Log("[CS135 Lab2] Current pos (Parent): " + transform.position);
        // Debug.Log("[CS135 Lab2] Current pos (Rig): " + CameraRig.transform.position);
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)){
            Debug.Log("[CS135 Lab2] Position before: " + CameraRig.transform.position);
            // Debug.Log("[CS135 Lab2] Rotation before: " + CameraRig.eulerAngles);
            transform.position = transform.position - CameraRig.transform.position + (new Vector3(0f, 0.675f, 0f));
            // CameraRig.transform.position = new Vector3(0f, 0f, 0f);
            // CameraRig.eulerAngles = new Vector3(0f, 0f, 0f);
            Debug.Log("[CS135 Lab2] Position after: " + CameraRig.transform.position);
            // Debug.Log("[CS135 Lab2] Rotation after: " + CameraRig.eulerAngles);
        }

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)){
            Debug.Log("[CS135 Lab2] camera rotation before is: " + transform.eulerAngles);
            transform.RotateAround(transform.position, Vector3.up, 180); // Rotate along y-axis, to rotate perspective, x axis would up down i think, and z would be useless, just upside down?

            // Transform currentTransform = this.transform;
            // currentTransform.rotation = Quaternion.Euler(currentTransform.rotation.eulerAngles.x, currentTransform.rotation.eulerAngles.y + 180, currentTransform.rotation.eulerAngles.z);

            // Transform currentTransform = CameraRig.transform;
            // currentTransform.rotation = Quaternion.Euler(currentTransform.rotation.eulerAngles.x, currentTransform.rotation.eulerAngles.y + 180, currentTransform.rotation.eulerAngles.z);
            
            Debug.Log("[CS135 Lab2] Rotating 180 deg??");
            Debug.Log("[CS135 Lab2] camera rotation before is: " + transform.eulerAngles);
        }       
    }

}
