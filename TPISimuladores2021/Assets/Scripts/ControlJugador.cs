using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour
{


    private Rigidbody rb;
    private CapsuleCollider col;
    public Transform cam;

    public float camVelocidad = 5f;
    public float camMinimoY = -60f;
    public float camMaximoY = 75f;
    public float camSmooth = 10f;

    private Vector3 posInicial;

    public float velocidad;
    public float vida;

    public bool corriendo;
    public bool pisando;
    public bool ocupado;       // para cuando estas hablando con un NPC o usando el inventario y no deberias poder moverte


    private float rotX;
    private float rotYCam;

    Vector3 dirIntX;
    Vector3 dicIntY;



    //private bool puedoDobleSaltar;

    // Start is called before the first frame update
    void Start()
    {

        Inicializar();

    }



    // Update is called once per frame
    void Update()
    {

        MovimientoCamara();     // probando si funciona mejor en fixedupdate o en update

    }


    void FixedUpdate()
    {




        MoverJugador();


    }




    public void Inicializar()
    {
        rb = GetComponent<Rigidbody>();

        col = GetComponent<CapsuleCollider>();

        posInicial = new Vector3(0, 1, 0);

        CargarJugador();

        Debug.Log("Se inicializaron los componentes del Jugador");

    }



    public void MoverJugador()
    {

        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        // Movimiento Izquierda
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || movHorizontal == -1)
        {
            transform.Translate(Vector3.left * velocidad * Time.deltaTime);
        }
        // Movimiento Derecha
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || movHorizontal == 1)
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }
        // Movimiento Arriba
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || movVertical == 1)
        {
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        }
        // Movimiento Abajo
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || movVertical == -1)
        {
            transform.Translate(Vector3.back * velocidad * Time.deltaTime);
        }


    }

    public void MovimientoCamara()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rotX += Input.GetAxis("Mouse X") * camVelocidad;
        rotYCam += Input.GetAxis("Mouse Y") * camVelocidad;

        rotYCam = Mathf.Clamp(rotYCam, camMinimoY, camMaximoY);

        Quaternion rotTarget = Quaternion.Euler(0, rotX, 0);
        Quaternion rotTargetCam = Quaternion.Euler(-rotYCam, 0, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotTarget, Time.deltaTime * camSmooth);
        cam.localRotation = Quaternion.Lerp(cam.localRotation, rotTargetCam, Time.deltaTime * camSmooth);
        
    }


    public void Morir()
    {
        // me muero, tengo que guardar mi memoria y volver a la posicion inicial



        CargarJugador();
    }



    public void CargarJugador()
    {

        // aca se cargaria la lista de memoria, son varios bools que despuen se chequean durante el juego

        rb.transform.position = posInicial;

    }


    public void GuardarJugador()
    {
        // aca tengo que guardar la lista de memoria

    }


}
