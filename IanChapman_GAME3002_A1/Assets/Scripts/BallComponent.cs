using UnityEngine;
using UnityEngine.Assertions;


public class BallComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vOrigVelocity = Vector3.zero; // A velocity of nothing. The ball has not been kicked
    private Rigidbody m_ridgidBody = null;
    private GameObject m_marker = null;

    private bool m_bOnGround;

    // Start is called before the first frame update
    void Start()
    {
        m_ridgidBody = GetComponent<Rigidbody>();
        //Assert.IsNotNull(m_ridgidBody, "Error: RidgidBody is not attached.");   //Just for debugging purposes
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositionLanded(); 
    }

    private Vector3 GetPositionLanded() // Where the ball has hit the ground again
    {
        float time = 2 * ( 0 - m_vOrigVelocity.y / Physics.gravity.y); //Equation needed for time passed

        //The balls velocity at its landing position (the moment it first touches the ground again) needs to be set equally with the 
        //velocity that the ball had started with, which is 0.

        Vector3 v1 = m_vOrigVelocity;
        v1.y = 0;
        v1 *=  time;

        //Finally, if we are able to predetermine where the ball is going to land on the ground, and the time it takes to get to the marker
        //we are now able to determine what the balls velocity was when it was first kicked.
        return transform.position + v1;
    }

    private void RenderMarker() // Setting up all the properties of the ball marker
    {
        m_marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder); //We can rely on the ball component to always reach a cylinder marker
        m_marker.transform.position = Vector3.zero; //The position of the marker relative to where the ball is located before it is kicked
        m_marker.transform.localScale = new Vector3(1f, 0.1f, 1f); //The size of the marker x, y and z
        m_marker.GetComponent<Renderer>().material.color = Color.red; //The colour of the marker is set as red
        m_marker.GetComponent<Collider>().enabled = false; //The marker has no collision. It does not "physically" exist in the game.
    }

    private void UpdatePositionLanded()
    {
        if (m_marker != null && m_bOnGround) // If the ball is still on the ground and the marker exists then the markers position can be changed to influence where
                                             //the ball is going to make contact with the ground again.
        {
            m_marker.transform.position = GetPositionLanded();
        }
    }



    public void BallKicked()
    {
        if (!m_bOnGround) //If the ball is in the air...
        {
            return; //...then skip this whole function
        }

        m_marker.transform.position = GetPositionLanded(); //Start calculating the where the ball will make contact with the ground
        m_bOnGround = false; //The ball is now in the air

        transform.LookAt(m_marker.transform.position, Vector3.up); //Forward vector is changed to "look at" the marker at its position from  when the ball was kicked

        m_ridgidBody.velocity = m_vOrigVelocity; //Velocity of the ridgid body component is instantly set back to 0;
    }


// Axis mapping that will be used with the players input to control the marker
    public void MovingForward(float fDelta)
    {
        m_vOrigVelocity.z += fDelta;
    }

    public void MovingBack(float fDelta)
    {
        m_vOrigVelocity.z -= fDelta;
    }

    public void MovingRight(float fDelta)
    {
        m_vOrigVelocity.x += fDelta;
    }

    public void MovingLeft(float fDelta)
    {
        m_vOrigVelocity.x -= fDelta;
    }
}
