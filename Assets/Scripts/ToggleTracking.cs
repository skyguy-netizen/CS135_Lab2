using UnityEngine;

public class ToggleTracking : MonoBehaviour
{
    private bool posTrackingOn = true;
    private bool rotTrackingOn = true;
    private Vector3 targetPosition;

    private Vector3 originalPosition = new(0f, 0.675f, 0f); // Original position of the camera rig
    private Quaternion targetRotation;
    public GameObject cameraTransform;
    public Transform parentTransform; // Reference to the parent transform (MainCameraParent)

    void Start()
    {
        // targetPosition = cameraTransform.transform.position;
        // targetRotation = cameraTransform.transform.rotation;
    }


    void HandleDisabledPos(Vector3 cameraLocalPosition)
    {
        if (!posTrackingOn)
        {

            // Vector3 rotatedLocalPosition = cameraTransform.transform.rotation * cameraLocalPosition;
            Debug.Log("[CS135 Lab2] cameraLocalPosition: " + cameraLocalPosition);
            Debug.Log("[CS135 Lab2] cameraTransform: " + cameraTransform.transform.position);
            Debug.Log("[CS135 Lab2] targetPosition: " + targetPosition);
            parentTransform.position += (targetPosition - cameraTransform.transform.position);
            // parentTransform.localScale = new Vector3(0f, 0f, 0f);
        }
    }

    void HandleDisabledRot(Quaternion cameraLocalRotation)
    {
        if (!rotTrackingOn)
        {
            Debug.Log("[CS135 Lab2] cameraLocalRotation: " + cameraLocalRotation);
            Debug.Log("[CS135 Lab2] targetRotation: " + targetRotation);
            Debug.Log("[CS135 Lab2] parentTransform: " + parentTransform.rotation);
            parentTransform.localRotation = targetRotation * Quaternion.Inverse(cameraLocalRotation);
            Debug.Log("[CS135 Lab2] cameraTransform: " + cameraTransform.transform.rotation);
        }
        //Do nothing if rotation tracking is enabled, just track the camera
    }

    void Update()
    {
        // Toggle position tracking with 'P' key
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            posTrackingOn = true ^ posTrackingOn;
            if (!posTrackingOn)
            {
                // Store the current world position when disabling position tracking
                targetPosition = cameraTransform.transform.position;
                Debug.Log("[CS135 Lab2] Storing original position: " + targetPosition);
            }
            else
            {
                Debug.Log("[CS135 Lab2] Position before: " + cameraTransform.transform.position);
                Debug.Log("[CS135 Lab2] Parent Position (before): " + parentTransform.position);
                Debug.Log("[CS135 Lab2] Target Position: " + targetPosition);
                parentTransform.position = originalPosition;
                parentTransform.position = parentTransform.position - cameraTransform.transform.position + targetPosition;
                // parentTransform.localScale = new Vector3(1f, 1f, 1f);
                // parentTransform.position = targetPosition;
                Debug.Log("[CS135 Lab2] Position after: " + cameraTransform.transform.position);
                Debug.Log("[CS135 Lab2] Parent Position (after): " + parentTransform.position);
                Debug.Log("[CS135 Lab2] Target Position: " + targetPosition);
            }
        }

        // Toggle rotation tracking with 'R' key
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            rotTrackingOn = true ^ rotTrackingOn;
            if (!rotTrackingOn)
            {
                // Store the current world rotation when disabling rotation tracking
                targetRotation = cameraTransform.transform.rotation;
            }
            else
            {
                Debug.Log("[CS135 Lab2] Rotation before: " + cameraTransform.transform.rotation);
                Debug.Log("[CS135 Lab2] Parent Position (before): " + parentTransform.rotation);
                Debug.Log("[CS135 Lab2] Target Position: " + targetRotation);
                parentTransform.localRotation = Quaternion.identity; //need to fix this in lab, this resets the orientation of the axes, will need to see how it works with the headset
            }
        }

        // Update parent's transform to maintain constant world position/rotation
        if (!rotTrackingOn)
        {
            // Get the inverse of the camera's local transform (T_parent,camera)
            Quaternion cameraLocalRotation = cameraTransform.transform.localRotation;
            HandleDisabledRot(cameraLocalRotation);
        }
        else if (!posTrackingOn)
        {
            Vector3 cameraLocalPosition = cameraTransform.transform.localPosition;
            HandleDisabledPos(cameraLocalPosition);
        }
    }
}
