using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float speed = 20f;
    private float minSpeed;
    public float topSpeed = 80f;
    public float turnForce = 60f;
    public Transform player;
    private float horizontalInput;
    private float verticalInput;
    public bool InvertedControl = false;
    private int invCntrl = -1;
    private bool thrustReady = true;
    private Vector3 playerRotation;
    public float rotSpeed;
    Rigidbody rb;
    public float flipSpeed = 1f;
    public float flipDuration = 1f;
    private bool isFlipping = false;
    public float maxRotationAngle = 45.0f;
    public float pitFactor = .5f;

    // Start is called before the first frame update
    void Start()
    {
        minSpeed = speed;
        rb = GetComponent<Rigidbody>();

        if(InvertedControl){invCntrl = 1;}
    }

        
    private void FixedUpdate() 
    {
        rb.velocity = speed * player.transform.forward * Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) && thrustReady)
        {
            StartCoroutine(Thrust());
        }

        //collect player input control
        float pitch = invCntrl * Input.GetAxis("Vertical") * turnForce;
        float yaw = Input.GetAxis("Horizontal") * turnForce;

        //rotate the correspondiding pitch and yaw
        Quaternion pitchRotation = Quaternion.AngleAxis(pitch * Time.deltaTime, Vector3.right);
        Quaternion yawRotation = Quaternion.AngleAxis(yaw * Time.deltaTime, Vector3.up);
        transform.rotation *= pitchRotation * yawRotation;


        if(Input.GetAxis("Horizontal") > -0.10 && Input.GetAxis("Horizontal") < 0.10)
        {
            Quaternion currentRotation = transform.rotation;
            float currentAngle = currentRotation.eulerAngles.z;
            float stabilizationAngle = Mathf.Clamp(-currentAngle, -5, 5);

            Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, stabilizationAngle);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotSpeed/4 * Time.deltaTime);
        }
        else
        {
            // Calculate the roll angle based on the horizontal input
            float rollAngle = -Input.GetAxis("Horizontal") * maxRotationAngle;

            // Clamp the roll angle to the desired range
            rollAngle = Mathf.Clamp(rollAngle, -maxRotationAngle, maxRotationAngle);

            // Calculate the target rotation based on the current rotation and the roll angle
            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, rollAngle);

            targetRotation *= Quaternion.AngleAxis(pitFactor * Mathf.Abs(rollAngle), Vector3.right);

            // Interpolate smoothly to the target rotation using Slerp
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotSpeed/4 * Time.deltaTime);
        }
        /*
        else
        {
            Quaternion currentRotation = transform.rotation;
            float currentAngle = currentRotation.eulerAngles.z;
            float maxRollAngle = Input.GetAxis("Horizontal") > 0 ? maxRotationAngle : -maxRotationAngle;

            float clampedAngle = Mathf.Clamp(currentAngle, -180.0f, 180.0f);
            float stabilizationAngle = Mathf.Clamp(clampedAngle, -maxRotationAngle, maxRotationAngle);

            Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, stabilizationAngle);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, yaw/4 * Time.deltaTime);
        }
        */
        /*//This rolls the ship corresponding to the left and right movement
            Quaternion rotation = transform.rotation * Quaternion.Euler(0.0f, 0.0f,yaw / 4* Time.deltaTime);
            float clampedAngle = Mathf.Clamp(player.rotation.eulerAngles.z, -maxRotationAngle, maxRotationAngle);
            Quaternion clampedRotation = Quaternion.Euler(player.rotation.eulerAngles.x,player.rotation.eulerAngles.y,clampedAngle);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotSpeed/4 * Time.deltaTime);
            */
        
        //if tip dips over pointing straight down or stright up, flip
        if (player.up.y < -0.5 && !isFlipping) {
            StartCoroutine(FlipOver());
        }
        

    }
    //Flip Over will roll the player over when flying upside down. 
    //This is to prevent gimbal lock and inverted controls in this scenario.
    IEnumerator FlipOver() {
        isFlipping = true;

        //intial is current rotation, and target in the 180 degree roll of it.
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.AngleAxis(180f, Vector3.forward);
        float elapsedTime = 0f;

        //This slowly rotates in increments of Time.deltaTime for a smooth look.
        while (elapsedTime < flipDuration) {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / flipDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isFlipping = false;
}
   
    IEnumerator Thrust()
    {
        thrustReady = false;
        //Accelerate up to 10
        for(float i= speed; i < topSpeed; i+=0.25f)
        {
            speed += 0.25f;
        }
        //wait 3 seconds
        yield return new WaitForSeconds(2.5f);
        //Decelerate
        for(float i = speed; i > minSpeed; i-=0.25f)
        {
            speed -= 0.25f;
        }
        //Wait for cooldown
        yield return new WaitForSeconds(5);
        //cooldown over
        thrustReady = true;
    }
}


/*
if(Input.GetAxis("Horizontal") > -0.10 && Input.GetAxis("Horizontal") < 0.10)
        {   
            playerRotation.z += -rotSpeed * playerRotation.z * Time.deltaTime;
        }
        else if(player.transform.localRotation.z < 0.4f && player.transform.localRotation.z > -0.4f)
        {
            playerRotation.z += -1 * (Input.GetAxis("Horizontal"));
        }
        //To avoid over turning, do not rotate when limit is reached, set by bounds above.
        else
        {
            //Do nothing to rotation
        }
        player.rotation = Quaternion.Euler( playerRotation );
*/