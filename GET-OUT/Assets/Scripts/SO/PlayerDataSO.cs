using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    // Camera 
    [Header("Camera Movement")]
    public float fov = 60f;
    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;


    // Zoom 
    [Header("Camera Zoom")]
    public bool enableZoom = true;
    public bool holdToZoom = false;
    public float zoomFOV = 30f;
    public float zoomStepTime = 5f;

    // Movement 
    [Header("Movement")]
    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;

    // Sprint 
    [Header("Sprint")]
    public bool enableSprint = true;
    public bool unlimitedSprint = false;
    public float sprintSpeed = 7f;
    public float sprintDuration = 5f;
    public float sprintCooldown = 0.5f;
    public float sprintFOV = 80f;
    public float sprintFOVStepTime = 10f;

    // Jump 
    [Header("Jump")]
    public bool enableJump = true;
    public float jumpPower = 5f;

    // Crouch 
    [Header("Crouch")]
    public bool enableCrouch = true;
    public bool holdToCrouch = true;
    public float crouchHeight = 0.75f;
    public float speedReduction = 0.5f;

    // Head Bob 
    [Header("Head Bob")]
    public bool enableHeadBob = true;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(0.15f, 0.05f, 0f);
}
