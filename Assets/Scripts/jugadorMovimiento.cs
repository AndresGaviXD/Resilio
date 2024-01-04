using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jugadorMovimiento : MonoBehaviour
{

    public float Speed;
    public float JumpForce;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    //Variable para confirmar si el jugador está tocando el suelo
    private bool Grounded;


    void Start()
    {
        //Para enlazar el componetente de fisicas al movimiento del jugador
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Para capturar el movimiento horizontal del personaje
        Horizontal = Input.GetAxisRaw("Horizontal");

        //Girar al personaje al presionarse a o d
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //Variable para animar movimiento horizontal
        Animator.SetBool("Running", Horizontal != 0.0f);

        //Confirmar si el jugador está tocando el suelo.
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;
        
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();

        }
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {

        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);

    }
}
