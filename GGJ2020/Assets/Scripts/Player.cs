using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    internal static Player instance;

    [SerializeField]
    private ProgressBarPro juiceBar;

    #region  Private Members
    private Rigidbody2D mRigidBody;
    private string mTag = "Player";
    public float mCurrentJuice;
    private bool mMoving;
    #endregion

    #region  Public Members
    public Jetpack jetpack;
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

    private double GetJuice()
    {
        return mCurrentJuice;
    }

    internal bool TrySendClick()
    {
        mMoving = true;
        return true;
    }

    public void AddJuice(float addedJuice)
    {
        mCurrentJuice += addedJuice;
        mCurrentJuice = Mathf.Min(mCurrentJuice, juice);
        juiceBar.SetValue(mCurrentJuice / juice);
    }

    public void SubtractJuice(float subtractedJuice)
    {
        mCurrentJuice -= subtractedJuice;
        mCurrentJuice = Mathf.Min(subtractedJuice, juice);
        juiceBar.SetValue(mCurrentJuice / juice);
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameObject.tag = mTag;

        //Set attributes
        mRigidBody = GetComponent<Rigidbody>();
        mRigidBody.constraints = normalOf2dPlane;
        mCurrentJuice = juice;

        //start logic
        StartCoroutine(controlLogic());
    }

    private void FixedUpdate()
        mRigidBody = GetComponent<Rigidbody2D>();
        AddJuice(juice);
    }

    void Update()
    {
        mRigidBody.drag = friction;

        faceDirection();
    }

    private void LateUpdate()
    {
        //PHYSICS LOGIC
        if (mMoving && mCurrentJuice > drainRate * 1.0f)
        {
            move();
            mMoving = false;
            return;
        }


        mRigidBody.angularVelocity = 0;

        AddJuice(chargeRate * Time.deltaTime);

        mMoving = false;
    }

    #region Primary Mechanics
    //Accelerates the player
    private void move()
    {
        if (mCurrentJuice > 0)
        {
            //drain jet pack
            SubtractJuice(drainRate * Time.deltaTime);
            mRigidBody.AddForce(transform.up * -1.0f * thrust);
        }
    }

    #endregion

    #region Helper Classes
    /// <summary> Face this player towards the mouse cursor </summary>
    private void faceDirection()
    {
        // determine players mouse cursor position in world
        var mouseInWorld = Input.mousePosition;

        // determine what plane is in the 3d space
        Vector3 planeNormal;
        mouseInWorld.z = Camera.main.transform.position.z;
        planeNormal = Vector3.forward;
        mouseInWorld = Camera.main.ScreenToWorldPoint(mouseInWorld);

        // use that plane for 2d
        Vector3 directionToMouse = Vector3.ProjectOnPlane(mouseInWorld - this.transform.position, planeNormal);
        Quaternion rotation = Quaternion.LookRotation(directionToMouse, planeNormal);
        mRigidBody.MoveRotation(rotation);
    }

    private IEnumerator controlLogic()
    {
        while(true)
        {
            mMoving = Input.GetMouseButton(0);
            mRigidBody.drag = friction;

            faceDirection();

            //IDLE LOGIC
            //Recharge jet pack
            if (!mMoving)
            {
                if (mCurrentJuice < juice)
                    mCurrentJuice += chargeRate * Time.deltaTime;
            }

            if (jetpack != null)
                jetpack.setState(mMoving);

            yield return null;
        }
    }
    #endregion

    void OnCollisionEnter(Collision collision)
    {
        
        CameraShake.Instance.ShakeByFactor(1.5f);
    }
}
