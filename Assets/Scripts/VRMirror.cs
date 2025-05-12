using UnityEngine;
using System;

public class VRMirror : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject CameraToTrack;
    public GameObject Mirror;

    private static float DISTANCE_FROM_MIRROR = 2f;
    private Vector3 TRACKING_OFFSET = new Vector3(0f, 0f, DISTANCE_FROM_MIRROR);

    // public GameObject Mirror;

    private bool tracking = false; // initially false, set to true first time B is pressed
    private bool mirroring = false; //toggle when B is pressed
    
    void Start()
    {
        
    }

    void HandleMirrorUpdate()
    {
        // Update position
        if (!mirroring)
        {
            transform.position = CameraToTrack.transform.position + TRACKING_OFFSET;
        }
        else
        {
            Vector3 mirroredPosition = CameraToTrack.transform.position;
            mirroredPosition.z = Mirror.transform.position.z + (Mirror.transform.position.z - CameraToTrack.transform.position.z); //update z to be where the camera is from mirror, but on other side
            transform.position = mirroredPosition;
        }

        // Update rotation
        if (!mirroring)
        {
            // Old Behavior
            // transform.rotation = CameraToTrack.transform.rotation;

            // New Behavior
            Vector3 currentAngles = CameraToTrack.transform.rotation.eulerAngles;
            currentAngles.y += 180f;
            currentAngles.x *= -1;

            Quaternion newRotation = Quaternion.Euler(currentAngles);
            transform.rotation = newRotation;
        }
        else
        {
            Vector3 currentAngles = CameraToTrack.transform.rotation.eulerAngles;
            currentAngles.y *= -1;
            currentAngles.x *= -1;
            Quaternion newRotation = Quaternion.Euler(currentAngles);
            transform.rotation = newRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            // mirroring = true ^ mirroring;
            // Debug.Log("Mirroring: " + mirroring);
            if (tracking == false)
            {
                tracking = true;
            }
            else
            {
                mirroring = !mirroring;
                Debug.Log("Mirroring");
            }
        }

        if (tracking)
        {
            HandleMirrorUpdate();
        }
    }
}
