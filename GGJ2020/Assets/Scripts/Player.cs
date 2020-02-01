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
    #endregion

    void Start()
    {
        gameObject.tag = mTag;

        //Set attributes
        mRigidBody = GetComponent<Rigidbody>();
        mRigidBody.constraints = RigidbodyConstraints.FreezePositionY;
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
        var target = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(target);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.point - transform.position;
        }

        target.y = 0;

        //print("Mouse to world postion: " + target.ToString());
        Quaternion rotation = Quaternion.LookRotation(target);
        mRigidBody.MoveRotation(rotation);

    }
    #endregion
}

