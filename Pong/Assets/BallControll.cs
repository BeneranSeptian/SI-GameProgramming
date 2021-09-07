using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControll : MonoBehaviour
{

    // rigidBody2D Bola
    private Rigidbody2D rigidbody2D;

    //besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    private Vector2 trajectoryOrigin;

    //untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        //mulai game
        RestartGame();

        trajectoryOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }
    void ResetBall()
    {
        //reset posisi menjadi (0.0)
        transform.position = Vector2.zero;

        //reset kecepatan menjadi (0,0)
        rigidbody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        //tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitial Force
        //float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        //tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
        float randomDirection = Random.Range(0, 2);

        //jika nilainya dibawah 1, bola berkerak ke kiri.
        //jika tidak, bola bergerak ke kanan

        if(randomDirection < 1.0f)
        {
            rigidbody2D.AddForce(new Vector2(-xInitialForce, yInitialForce));

        }
        else
        {
            rigidbody2D.AddForce(new Vector2(xInitialForce, yInitialForce));
        }

    }

    void RestartGame()
    {
        //kembalikan bole ke posisi semula
        ResetBall();

        //setelah 2 detik berikan gaya ke bola
        Invoke("PushBall", 2);
    }

}
