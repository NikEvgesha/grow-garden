// ThirdPersonControllerWithAnimator.cs
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    public static ThirdPersonController Instance { get; private set; }

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float turnSmoothTime = 0.1f;
    public float jumpForce = 5f;

    [Header("Gravity")]
    public float gravity = -9.81f;
    public float terminalVelocity = -20f;

    [Header("References")]
    public Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;
    private float turnSmoothVelocity;
    private float verticalVelocity;
    private string _animationStateName = "Animation_int";

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        bool isMoving = inputDir.magnitude >= 0.1f;
        bool isGrounded = controller.isGrounded;

        // Animator parameters
        animator.SetFloat("Speed_f", inputDir.magnitude * moveSpeed);
        animator.SetBool("Static_b", !isMoving && isGrounded);

        // Jump
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
                animator.SetBool("Jump_b", true);
            }
            else
            {
                // small downward force to keep grounded
                verticalVelocity = -1f;
                animator.SetBool("Jump_b", false);
            }
        }
        else
        {
            animator.SetBool("Jump_b", false);
        }

        // Movement and rotation
        if (isMoving)
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg
                                + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothVelocity,
                turnSmoothTime
            );
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;
        verticalVelocity = Mathf.Max(verticalVelocity, terminalVelocity);
        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }
    public void AnimationState(int state)
    {
        animator.SetInteger(_animationStateName, state);
    }

}