using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Pemain 1
    public PlayerControl player1; //skrip
    private Rigidbody2D player1RigidBody;

    //Pemain 2
    public PlayerControl player2; //skrip
    private Rigidbody2D player2RigidBody;

    //Bola
    public BallControll ball;
    private Rigidbody2D ballRigidBody;
    private CircleCollider2D ballCollider;

    //skor maksimal
    public int maxScore;

    //apakah debug window ditampilkan?
    private bool isDebugWindowShown = false;



    // Objek untuk menggambar prediksi lintasan bola
    public Trajectory trajectory;





    // Start is called before the first frame update
    void Start()
    {
        player1RigidBody = player1.GetComponent<Rigidbody2D>();
        player2RigidBody = player2.GetComponent<Rigidbody2D>();
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //untuk menampilkan GUI

    private void OnGUI()
    {
        //simpan nilai warna lama GUI
        Color oldColor = GUI.backgroundColor;
        //Tampilkan skor pemain 1 di kiri atas dan pemain 2 di kanan atas
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);

        //Tampilkan skor pemain 1 di kiri atas dan pemain 2 di kanan atas
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        //Tombol Restart untuk memuali game dari awal
        if(GUI.Button(new Rect(Screen.width / 2 -60, 35, 120, 53), "RESTART"))
        {
            //ketika tombol restart ditekan, reset skor kedua pemain..
            player1.ResetScore();
            player2.ResetScore();

            //..dan restart game
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        //jika pemain 1 menang (mencapai skor maksimal), ...
        if (player1.Score == maxScore)
        {
            //... tampilkan teks "PLAYER ONE WINS" dibagian kiri layar...
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");

            //...dan kembalikan bola ke tengah.
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }


        //sebaliknya jika pemain 2 menang (mencapai skor maksimal),...
        else if (player2.Score == maxScore)
        {
            //tampilkan teks "PLAYER TWO WINS" di bagian kanan layar...
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");

            //... dan kembalikan bola ke tengah.
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        //jika isDebugWindowShown == true, tampilkan text area untuk debug window.

        if (isDebugWindowShown)
        {
            
            GUI.backgroundColor = Color.red;
            //Simpan variabel-variabel fisika yang akan ditampilkan.
            float ballMass = ballRigidBody.mass;
            Vector2 ballVelocity = ballRigidBody.velocity;
            float ballSpeed = ballRigidBody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocty = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            //tampilkan debug window
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            GUI.backgroundColor = oldColor;
        }

        if (GUI.Button(new Rect(Screen.width/2 -60,Screen.height -73,120,53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }
    }
}
