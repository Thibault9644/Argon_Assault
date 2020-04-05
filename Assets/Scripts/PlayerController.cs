﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 40f;
    [Tooltip("In m")] [SerializeField] float xRange = 14f;

    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 40f;
    [Tooltip("In m")] [SerializeField] float yRange = 8f;
    [SerializeField] float pitchShift = -5;
    [Header("Screen-position based")]
    [SerializeField] float positionPitchFactor = -2.5f;
    [SerializeField] float positionYawFactor = 1.3f;
    [Header("Control-Throw based")]
    
    
    [SerializeField] float rollControlFactor = -30f;
    [SerializeField] float controlPitchFactor = -20;


    float xThrow, yThrow;
    bool controlsAreEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsAreEnabled == true)
        {
            ProcessTranslation();
            ProcessRotation();
        }

    }

   
    void OnPlayerDeath() // called by string reference ( do not rename ) 
    {
        print("Control");
        controlsAreEnabled = false;
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
