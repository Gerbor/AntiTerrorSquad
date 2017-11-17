using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharController : MonoBehaviour
{

    [Serializable]
    public class Speed
    {
        public float normalSpeed;
        public float sprintSpeed;
        public float cameraSpeed;
        public float cameraSens;
        public bool isSprinting;
    }

    [Serializable]
    public class Restraints
    {
        public float viewRange;
        public float rotX;
        public Quaternion originalRotation;
    }

    [Serializable]
    public class Settings
    {
        public bool sprintToggle;
    }

    public Speed speed = new Speed();
    public Restraints restraints = new Restraints();
    public Settings settings = new Settings();

    public GameObject mainCam;
    public bool paused;
    public static CharController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Vector3 rot = mainCam.transform.localRotation.eulerAngles;
        restraints.rotX = rot.x;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (!paused)
        {
            Movement();
            BodyRotation();
            HeadRotation();
        }
        CheckSprint();
        ButtonInput();
    }

    private void ButtonInput()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
            {
                paused = true;
                MouseLock.instance.CheckLock(false);
            }
            else
            {
                paused = false;
                MouseLock.instance.CheckLock(true);
            }
        }
    }

    private void CheckSprint()
    {
        if (!settings.sprintToggle)
        {
            if (Input.GetButton("Sprint"))
            {
                speed.isSprinting = true;
            }
            else
            {
                speed.isSprinting = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Sprint"))
            {
                speed.isSprinting = true;
            }
            if (Input.GetButtonUp("Sprint"))
            {
                speed.isSprinting = false;
            }
        }
    }

    private void Movement()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (speed.isSprinting)
        {
            moveDirection *= speed.sprintSpeed;
        }
        else
        {
            moveDirection *= speed.normalSpeed;
        }
        transform.Translate(moveDirection * Time.deltaTime);
    }

    private void BodyRotation()
    {
        Vector3 rotDir = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        rotDir *= speed.cameraSpeed;
        rotDir *= speed.cameraSens;
        transform.Rotate(rotDir * Time.deltaTime);
    }

    private void HeadRotation()
    {
        float mouseY = -Input.GetAxis("Mouse Y");
        restraints.rotX += mouseY * speed.cameraSpeed * Time.deltaTime;
        restraints.rotX = Mathf.Clamp(restraints.rotX, -restraints.viewRange, restraints.viewRange);
        Quaternion localRotation = Quaternion.Euler(restraints.rotX, transform.localEulerAngles.y, 0.0f);
        mainCam.transform.rotation = localRotation;
    }
}