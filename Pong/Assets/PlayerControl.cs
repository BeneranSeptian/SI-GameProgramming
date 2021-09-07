using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //tombol untuk gerak ke atas
    public KeyCode upButton = KeyCode.W;

    //tombol untuk gerak ke bawah
    public KeyCode downButton = KeyCode.S;

    //kecepatan gerak
    public float speed = 10.0f;

    //Batas atas dan bawah game scene (batas bawah menggunakan minus(-))
    public float yBoundary = 9.0f;

    //Rigid Body raket ini
    private Rigidbody2D rigidbody2D;

    //Skor Pemain
    private int score;

    //titik tumbukan terakhir dengan bola, untuk menampilkan variabel-variabel fisika terkait tumbukan tersebut
    private ContactPoint2D lastContactPoint;

    //untuk mengakses informasi titik kontak dari kelas lain
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //dapatkan kecepatan raket sekarang

        Vector2 velocity = rigidbody2D.velocity;

        //jika pemain menekan tombol keatas, beri kecepatan positif ke komponen y (atas)
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }

        //jika pemain menekan tombol kebawah, beri kecepatan negatif ke komponen y (bawah)
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }

        //jika pemain tidak menekan tombol apa-apa, kecepatannya nol.
        else
        {
            velocity.y = 0.0f;
        }

        //masukkan kembali kecepatannya ke rigidBody2D
        rigidbody2D.velocity = velocity;

        //dapatkan posisi raket sekarang.
        Vector3 position = transform.position;

        //jika posisi raket melewati batas atas(yBoundary), kembalikan ke batas atas tersebut
        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }

        //jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas bawah tersebut
        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        //masukan kemabli posisinya ke transform
        transform.position = position;

    }

    //ketika terjadi tumbukan dengan bola, rekam titik kontaknya
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

    //menaikkan skor sebanyak 1 poin
    public void IncrementScore()
    {
        score++;
    }

    //mengembalikan skor menjadi 0
    public void ResetScore()
    {
        score = 0;
    }

    //mendapatkan nilai skor
    public int Score
    {
        get { return score; }
    }

}
