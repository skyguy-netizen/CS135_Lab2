using UnityEngine;
using UnityEngine.XR;
using System;

public class GenerateStimuli : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject redSphere;
    public GameObject smallSphere;
    public GameObject largeSphere;

    public GameObject cameraTransform;

    private bool changePerception = false;
    private bool objectCurrentState = false;
    private float waittime = 2.0f;
    private float timer = 0.0f;


    private float redSphereRadius = 0.5f;
    void Start()
    {
        redSphere.SetActive(false);
        smallSphere.SetActive(false);
        largeSphere.SetActive(false);
    }


    void setState(bool state)
    {
        // redSphere.SetActive(state);
        
    }

    float getDistance(GameObject camera, GameObject point) {
        float distance = Vector3.Distance(camera.transform.position, point.transform.position);
        Debug.Log("Distance is: " + distance);
        return distance;
    }

    void HandleScaling()
    {

        // Get Positions
        Vector3 currPos = cameraTransform.transform.position;
        Vector3 redPos = redSphere.transform.position;
        Vector3 smallPos = smallSphere.transform.position;
        Vector3 largePos = largeSphere.transform.position;

        // Calculate distance for each
        float redSphereDistance = getDistance(cameraTransform, redSphere);
        float smallSphereDistance = getDistance(cameraTransform, smallSphere);
        float largeSphereDistance = getDistance(cameraTransform, largeSphere);


        // Scales based on radius
        float scaleSmall = (redSphereRadius * smallSphereDistance) / redSphereDistance;
        float scaleLarge = (redSphereRadius * largeSphereDistance) / redSphereDistance;


        Debug.Log("Red sphere scale: " + redSphere.transform.localScale);

        Debug.Log("[CS 135 Lab2] Small scale: " + scaleSmall);
        Debug.Log("[CS 135 Lab2] Large scale: " + scaleLarge);

        Debug.Log("[CS 135 Lab2] Small new radius: " + new Vector3(redSphereRadius, redSphereRadius, redSphereRadius) * scaleSmall);
        Debug.Log("[CS 135 Lab2] Large new radius: " + new Vector3(redSphereRadius, redSphereRadius, redSphereRadius) * scaleSmall);

        // // Now just scale, each spheres starting radius is 0.5, so we can just apply the scale
        smallSphere.transform.localScale = redSphere.transform.localScale * (2f * scaleSmall); //for some reason, scale needs to 2 times, because otherwise the spheres looked like half the size, maybe cuz of the radius and diameter

        // did some research: localScale represents diameter, but the scale is the radius scale, so diameter scale would be twice the scale
        largeSphere.transform.localScale = redSphere.transform.localScale * (2f * scaleLarge);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            changePerception = true;
            objectCurrentState = !objectCurrentState;
            timer = 0f; //reset timer to wait for two seconds
            redSphere.SetActive(objectCurrentState);
            Debug.Log("Reset timer");
        }

        timer += Time.deltaTime;
        if (timer >= waittime)
        {
            timer = 0f;
            Debug.Log("Waited 2 seconds");


            if (changePerception)
            {   
                smallSphere.SetActive(objectCurrentState);
                largeSphere.SetActive(objectCurrentState);
                if (objectCurrentState)
                {
                    HandleScaling();
                    changePerception = false;
                }
               
            }
        }
    }
}
