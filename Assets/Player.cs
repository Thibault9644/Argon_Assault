using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 40f;
    [Tooltip("In m")] [SerializeField] float xRange = 14f;

    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 40f;
    [Tooltip("In m")] [SerializeField] float yRange = 8f;

    [SerializeField] float positionPitchFactor = -2.5f;
    [SerializeField] float controlPitchFactor = -20;
    [SerializeField] float pitchShift = -5;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float rollControlFactor = -30f;


    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();

    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor +pitchShift;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * rollControlFactor;
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    private void ProcessTranslation()
    {
        moveX();
        moveY();
    }

    private void moveX()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
    }
    private void moveY()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
    }
}
