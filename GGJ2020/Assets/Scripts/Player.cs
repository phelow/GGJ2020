using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    #region  Private Members
    private Rigidbody mRigidBody;
    private string mTag = "Player";
    public double mCurrentJuice;
    private bool mMoving;
    #endregion

    #region  Public Members
    public float thrust = 25;
    [Range(1, 100)]
    public float turn;
    [Range(0, 5)]
    public float friction = 2;
    [Range(1, 1000)]
    public float juice = 500; //the amount of booster juice the player has on a fully charged jetpack
    [Range(1, 500)]
    public float drainRate = 100; //How fast the jetpact uses juices when moving
    [Range(1, 500)]
    public float chargeRate = 200; //How fast the jetpack refuels when idle

    [Tooltip("normal of the 2d plane that the game will be played on can only be 1 of the 3 axises due to rigidbody limitations")]
    public RigidbodyConstraints normalOf2dPlane = RigidbodyConstraints.FreezePositionZ;
    #endregion

    void Start()
    {
        gameObject.tag = mTag;

        //Set attributes
        mRigidBody = GetComponent<Rigidbody>();
        mRigidBody.constraints = normalOf2dPlane;
        mCurrentJuice = juice;
    }

    void Update()
    {
        mMoving = Input.GetMouseButton(0);
        bool grabbing = Input.GetMouseButtonDown(1);
        mRigidBody.drag = friction;

        faceDirection();

        //Grab
        if (grabbing)
            grab();

        //IDLE LOGIC
        //Recharge jet pack
        if (!mMoving)
        {
            if (mCurrentJuice < juice)
                mCurrentJuice += chargeRate * Time.deltaTime;
        }
    }

    //Good for updats involving physics
    private void FixedUpdate()
    {
        //PHYSICS LOGIC
        if (mMoving)
            move();
        else
            mRigidBody.angularVelocity = Vector3.zero;
    }

    #region Primary Mechanics
    //Accelerates the player
    private void move()
    {
        if (mCurrentJuice > 0)
        {
            //drain jet pack
            mCurrentJuice -= drainRate * Time.deltaTime;
            mRigidBody.AddForce(transform.forward * thrust, ForceMode.Force);
        }
    }

    //Grabs an object if there is an object presents
    private void grab()
    {

    }

    #endregion

    #region Helper Classes
    //Faces the player to the direction that the mouse is pointing at
    private void faceDirection()
    {
        // determine players mouse cursor position in world
        var mouseInWorld = Input.mousePosition;
        mouseInWorld.z = Camera.main.transform.position.z;
        mouseInWorld = Camera.main.ScreenToWorldPoint(mouseInWorld);

        // determine what plane is in the 3d space
        Vector3 planeNormal;
        switch (normalOf2dPlane)
        {
            case RigidbodyConstraints.FreezePositionX:
                planeNormal = Vector3.right;
                break;
            case RigidbodyConstraints.FreezePositionY:
                planeNormal = Vector3.up;
                break;
            case RigidbodyConstraints.FreezePositionZ:
                planeNormal = Vector3.forward;
                break;
            default:
                throw new System.NotImplementedException("Not sure what to do with that restraint, do a position one instead");
        }

        // use that plane for 2d
        Vector3 directionToMouse = Vector3.ProjectOnPlane(mouseInWorld - this.transform.position, planeNormal);
        Quaternion rotation = Quaternion.LookRotation(directionToMouse, planeNormal);
        mRigidBody.MoveRotation(rotation);
    }
    #endregion
}

