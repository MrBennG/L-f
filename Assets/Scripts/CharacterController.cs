using UnityEngine;

public class CharacterController : MonoBehaviour{

    public float inputDelay = 0.1f;
    public float forwardVel = 12;
    public float rotateVel = 100;
    private Rigidbody rBody;
    float forwardInput, turnInput;

    public Quaternion TargetRotation { get; private set; }



    void Start()
    {
        TargetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("No rigid body");

        forwardInput = turnInput = 0;

    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Turn();
    }

    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        if (Mathf.Abs(forwardInput) > inputDelay)
        {
            //move
            rBody.velocity = transform.forward * forwardInput * forwardVel;

        }
        else
            //don't move
            rBody.velocity = Vector3.zero;
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputDelay)
        {
            //move
            TargetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, Vector3.up);
            transform.rotation = TargetRotation;
        }
        else
            //don't move
            rBody.velocity = Vector3.zero;
    }
}
