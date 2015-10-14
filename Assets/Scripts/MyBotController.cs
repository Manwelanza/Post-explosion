using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class MyBotController : MonoBehaviour {
    public float animSpeed = 1.5f;                  // Velocidad de la animación
    private Animator anim;                          // Animator del personaje
    private AnimatorStateInfo currentBaseState;         // a reference to the current state of the animator, used for base layer
    private AnimatorStateInfo layer2CurrentState;       // a reference to the current state of the animator, used for layer 2
    private CapsuleCollider col;					// Collider muy simple del personaje
    private bool Grounded;                        // Variable para saber si el personaje estará en el suelo
    private bool jump;                              // Variable para saber si nuestro personaje esta saltando

    // Use this for initialization                                  
    void Start () {
        // Iniciamos variables
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.speed = animSpeed;                             // Establecemos la velocidad de las animaciones.
        float horizontal = Input.GetAxis("Horizontal");              // Cogemos si el jugador ha pulsado para moverse en el eje horizontal
        float vertical = Input.GetAxis("Vertical");                // Cogemos si el jugador ha pulsado para moverse en el eje vertical
        anim.SetFloat("Speed", vertical);                          // Pasamos al animador lo que pasa en el eje vertical		
        anim.SetFloat("Direction", horizontal); 						// Pasamos al animador lo que pasa en el eje horizontal
        float jump = Input.GetAxis("Jump");
        anim.SetFloat("Jump", jump);
        

        if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Base Layer.Jump"))
        {
            // Ahora comprobamos si esta en el suelo
            Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo))
            {
                // Si la distancia al suelo es mayor de 1.75 (para que pueda subir rampas
                if (hitInfo.distance > 1.75f)
                {
                    anim.SetBool("Grounded", false);
                    anim.SetBool("JumpDown", true);
                }
                else
                {
                    anim.SetBool("Grounded", true);
                    anim.SetBool("JumpDown", false);
                }
            }
        }
    }
}
