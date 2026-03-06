using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;
    public Animator playerAnimator;
    Rigidbody playerRigidBody;
    [SerializeField] Transform camera;

    public bool isGameStarted;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidBody = this.GetComponent<Rigidbody>();
        playerAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameStarted)
            Movement();
    }

    void Movement()
    {
        float horizMove = Input.GetAxis("Horizontal");
        float vertMove = Input.GetAxis("Vertical");

        Vector3 camForward = camera.forward;
        Vector3 camRight = camera.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward* vertMove + camRight * horizMove;

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(move);
        }

        if (Input.GetKey(KeyCode.LeftShift))
            move *= 5f;

        playerAnimator.SetFloat("Velocity", move.magnitude);

        playerRigidBody.linearVelocity = move;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            collision.gameObject.GetComponent<Animator>().enabled = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            GameManager.Instance.LoadLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Barrier"))
        {
            GameManager.Instance.ReloadScene();
        }
    }
}
