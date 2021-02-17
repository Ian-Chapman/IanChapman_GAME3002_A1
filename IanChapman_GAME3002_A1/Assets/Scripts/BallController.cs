using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float m_fChngInputVal = 0.1f;

    private BallComponent m_ball = null;

    // Start is called before the first frame update
    void Start()
    {
        m_ball = GetComponent<BallComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs(); //Checking every frame to see if the key inputs have changed. However, marker cannot be changed after the ball is kicked
    }

    //Keys to be used to move to marker to the desired location
    private void CheckInputs()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_ball.BallKicked();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_ball.MovingForward(m_fChngInputVal);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_ball.MovingBack(m_fChngInputVal);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_ball.MovingRight(m_fChngInputVal);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_ball.MovingLeft(m_fChngInputVal);
        }
    }
}
