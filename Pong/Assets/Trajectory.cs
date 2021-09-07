using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    //Skrip, Collider, dan rigidbody bola

    public BallControll ball;
    CircleCollider2D ballCollider;
    Rigidbody2D ballRigidbody;

    //bola bayangan yang akan ditampilkan dititik tumbukan
    public GameObject ballAtCollision;
    // Start is called before the first frame update
    //inisialisasi rigidbody dan collider

   
    void Start()
    {
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //inisiasi status pantulan lintasan, yang hanya akan ditampilkan jika lintasan bertumbukan dengan objek tertentu.
        bool drawBallAtCollision = false;

        //Titik tumbukan yang digeser, untuk menggambar ballAtCollision
        Vector2 offsetHitPoint = new Vector2();

        //tentukan titik tumbukan dengan deteksi pergerakan lingkaran
        RaycastHit2D[] circleCastHit2DArray = 
            Physics2D.CircleCastAll(ballRigidbody.position,
            ballCollider.radius, ballRigidbody.velocity.normalized);

        //untuk setiap titik tumbukan...
        foreach(RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            //jika terjadi tumbukan dan tumbukan tersebut tidak dengan bola
            //karena garis lintasan digambar dari titik tengah bola...
            if(circleCastHit2D.collider !=null && circleCastHit2D.collider.GetComponent<BallControll>() == null)
            {
                //garis lintasan akan digambar dari titik tengah bola saat ini ke tiitk tengah bola pada saat tumbukan,
                // yaitu sebuah tiitk yang di offset dari titik tumbukan berdasar vektor normal titik
                //tersebut sebesar 
                //jari-jari bola

                //tentukan titik tumbukan
                Vector2 hitPoint = circleCastHit2D.point;

                //tentukan normal di titik tumbukan
                Vector2 hitNormal = circleCastHit2D.normal;

                //tentukan offsetHitPoint, yaitu titik tengah bola pada saat bertumbukan
                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;

                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                //kalau bukan sidewall, gambar pantulannya
                if (circleCastHit2D.collider.GetComponent<SideWall>() == null)
                {
                    //hitung vektor datang
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;

                    //hitung vektor keluar
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                    //hitung dot product dari outVector dan hitNormal. Digunakan supaya garis lintasan
                    //ketika terjadi tumbukan tidak digambar
                    float outDot = Vector2.Dot(outVector, hitNormal);
                    if (outDot > -1.0F && outDot < 1.0f)
                    {
                        //gambar lintasan pantulannya
                        DottedLine.DottedLine.Instance.DrawDottedLine(offsetHitPoint,
                            offsetHitPoint + outVector * 10.0f);

                        //untuk menggambar bola bayangan di prediksi titik tumbukan
                        drawBallAtCollision = true;
                    }
                }
                //hanya gambar lintasan untuk satu titik tumbukan, jadi keluar dari loop
                break;
            }

            //jika true, ...
            if (drawBallAtCollision)
            {
                //gambar bola bayangan dititik tumbukan
                ballAtCollision.transform.position = offsetHitPoint;
                ballAtCollision.SetActive(true);

            }
            else
            {
                //sembunyikan bola bayangan
                ballAtCollision.SetActive(false);
            }
        }   

    }
}
