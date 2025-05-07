using UnityEngine;

public class VRMirror : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject CameraToTrack;
    public GameObject MainCameraParent;

    private bool mirroring = false; //toggle when B is pressed
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch)){
            mirroring = true^mirroring;
            Debug.Log("Mirroring: " + mirroring);
        }

        if (mirroring){
            transform.position = CameraToTrack.transform.position + (CameraToTrack.transform.position - transform.position);
            
        }
    }
}
