using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    //Pemain yang akan bertambah skornya bila bola menyentuh dinding ini
    public PlayerControl player;

    [SerializeField]
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // akan dipanggil ketika objek lain ber-collider (bola) bersentuhan dengan dinding

    private void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        //jika objek tersebut bernama "ball"

        if(anotherCollider.name == "Ball")
        {
            //tambahkan skor ke pemain
            player.IncrementScore();

            //jika skor pemain belum mencapai skor maksimal
            if (player.Score < gameManager.maxScore)
            {
                //..restart game setelah bola mengenai dinding.
                anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }

}





