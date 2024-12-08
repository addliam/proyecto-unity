using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1; //0:left, 1:middle, 2:right
    public float laneDistance = 2.5f; //The distance between lanes

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float jumpForce;
    public float Gravity;
    
    private bool isColliding = false; 

    // manager
    private PlayerManager playerManager = new PlayerManager();
    void Start()
    {
        isGrounded = true;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        direction.z = forwardSpeed;

        // chequear si toco suelo
        isGrounded = CheckIfGrounded();

        // SALTO
        if (isGrounded)
        {
            if (direction.y < 0)
                direction.y = -2;   
            animator.SetBool("IsJumping", false);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
                // jumping 
                animator.SetBool("IsJumping", true);
            }
        }
        else{
            direction.y += Gravity * Time.deltaTime;
        }
        // DERECHA
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        // IZQUIERDA
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        //Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;


        //transform.position = targetPosition;
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.magnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

    }
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.transform.tag);
        if(hit.transform.tag == "Obstacle" && !isColliding)
        {
            isColliding = true;
            // Cuando colisione con un obstaculo
            HealthManager.health--;
            PlayerManager.LoseLife();
            StartCoroutine(AllowPassThrough(2, hit.collider));
        }
    }

    IEnumerator AllowPassThrough(float duration, Collider obstacleCollider)
    {
        Debug.Log("Ignoring collision with obstacle");
        if (obstacleCollider != null)
        {
            obstacleCollider.enabled = false;
        }
        yield return new WaitForSeconds(duration);
        if (obstacleCollider != null)
        {
            obstacleCollider.enabled = true;
        }
        isColliding = false;
    }

    private bool CheckIfGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
    }
}