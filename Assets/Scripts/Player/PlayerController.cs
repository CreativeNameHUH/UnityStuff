using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float   playerSpeed  = 5f;
    public float   playerSprint = 10f;
    public float   playerJump   = 10f;
    
    public bool    isGrounded   = true;
    
    public Vector3 jump;

    private Rigidbody _rigidbody;

    private void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void OnCollisionExit()
    {
        isGrounded = false;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        jump = new Vector3(0f, 150f, 0f);
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        const float yAxis = 0;

        Vector3 tVector3 = new Vector3(xAxis, yAxis, zAxis);

        if (Input.GetKey("left shift"))
        {
            tVector3 *= playerSprint * Time.deltaTime;
        }
        else
        {
            tVector3 *= playerSpeed * Time.deltaTime;
        }

        transform.Translate(tVector3);

        if (Input.GetKeyDown("space") && isGrounded)
        {
            _rigidbody.AddForce(jump * playerJump * Time.deltaTime, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}