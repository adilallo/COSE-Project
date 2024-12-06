using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public GameObject cubeAugustNormal;
    public GameObject cubeSoundWave;
    public GameObject cubeSoundSpikes;
    public Camera animationCamera;
    public Camera fpsCamera;
    public float lookSpeed = 2.0f;
    private bool isAnimationActive = false;
    private bool animationsStarted = false;
    private float elapsedTime = 0f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    // Visibility schedules
    private readonly int[] cubeAugustInvisibleTimes = { 6, 53, 67, 69, 85, 86, 100, 107, 114, 142, 143, 167, 172, 182, 192, 204, 217, 221, 232, 245, 264, 266, 267, 278, 291 };
    private readonly int[] cubeSoundWaveVisibleTimes = { 6, 67, 85, 100, 107, 142, 143, 167, 172, 192, 204, 217, 221, 232, 245, 264, 266, 267, 278 };
    private readonly int[] cubeSoundSpikesVisibleTimes = { 53, 69, 86, 114, 182, 291 };

    void Start()
    {
        // Ensure only the FPS camera is active at the start
        animationCamera.gameObject.SetActive(false);
        fpsCamera.gameObject.SetActive(true);
        Debug.Log("Game Start: FPS camera active, Animation camera inactive.");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter triggered by: {other.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger.");

            // Play animations for all cubes
            PlayCubeAnimations();

            // Activate the animation camera and deactivate the FPS camera
            fpsCamera.gameObject.SetActive(false);
            animationCamera.gameObject.SetActive(true);

            // Lock the cursor for mouse look
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Debug.Log("Trigger Entered: Switched to Animation camera.");

            // Enable the flag and reset elapsed time
            isAnimationActive = true;
            elapsedTime = 0f;
        }
    }

    void Update()
    {
        if (isAnimationActive)
        {
            elapsedTime += Time.deltaTime;

            // Check visibility for each cube
            ToggleCubeVisibility(cubeAugustNormal, !IsTimeInList(cubeAugustInvisibleTimes, Mathf.FloorToInt(elapsedTime)));
            ToggleCubeVisibility(cubeSoundWave, IsTimeInList(cubeSoundWaveVisibleTimes, Mathf.FloorToInt(elapsedTime)));
            ToggleCubeVisibility(cubeSoundSpikes, IsTimeInList(cubeSoundSpikesVisibleTimes, Mathf.FloorToInt(elapsedTime)));

            // Handle mouse look
            HandleMouseLook();

            // Exit animation on X key
            if (Input.GetKeyDown(KeyCode.X))
            {
                animationCamera.gameObject.SetActive(false);
                fpsCamera.gameObject.SetActive(true);

                // Unlock the cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Debug.Log("X Key Pressed: Switched back to FPS camera.");

                isAnimationActive = false;
            }
        }
    }

    // Play animations for all cubes
    private void PlayCubeAnimations()
    {
        if (animationsStarted) return; // Skip if already started

        Animator animatorAugust = cubeAugustNormal.GetComponent<Animator>();
        Animator animatorWave = cubeSoundWave.GetComponent<Animator>();
        Animator animatorSpikes = cubeSoundSpikes.GetComponent<Animator>();

        if (animatorAugust != null)
        {
            animatorAugust.SetTrigger("roomTrigger");
        }
        if (animatorWave != null)
        {
            animatorWave.SetTrigger("roomTrigger");
        }
        if (animatorSpikes != null)
        {
            animatorSpikes.SetTrigger("roomTrigger");
        }

        animationsStarted = true; // Mark animations as started
    }

    // Helper function to check if a time is in a given list
    private bool IsTimeInList(int[] times, int time)
    {
        foreach (int t in times)
        {
            if (t == time)
                return true;
        }
        return false;
    }

    // Helper function to toggle visibility
    private void ToggleCubeVisibility(GameObject cube, bool isVisible)
    {
        color_change colorChangeScript = cube.GetComponent<color_change>();
        if (colorChangeScript != null)
        {
            colorChangeScript.SetVisibility(isVisible);
        }
    }

    // Handle mouse look for the animation camera
    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit vertical look angle

        animationCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
