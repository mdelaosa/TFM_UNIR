using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System.Linq;

public class ClientController : MonoBehaviour
{
    private GameObject mainCamera;

    private Animator animator;

    private GameObject[] chairs;
    private GameObject[] triggers;
    [SerializeField] private Chair chair;
    [SerializeField] private Table table;

    [SerializeField] private float speed;

    private bool reachedEntrance;
    private bool reachedChair;
    private bool reachedEnd;
    private bool ordered;

    [Header("Order")]
    private OrderRecipe orderRecipe;
    private Order orderedRecipe;

    private string customerSuccess;
    private float waitingTime;
    [SerializeField] private float maxWaitingTime;

    [Header("Canvas")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image imageFace;
    [SerializeField] private Image imageOrder;
    [SerializeField] private Sprite happyFace;
    [SerializeField] private Sprite neutralFace;
    [SerializeField] private Sprite madFace;

    [SerializeField] private bool receivedOrder;
    [SerializeField] private bool correctOrder;
    [SerializeField] private bool hasEaten;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        animator = GetComponent<Animator>();

        speed = 5f;

        triggers = GameObject.FindGameObjectsWithTag("Trigger");
        triggers = triggers.OrderBy(triggers => triggers.name).ToArray();

        orderRecipe = FindObjectOfType<OrderRecipe>();

        reachedEntrance = false;
        reachedChair = false;
        reachedEnd = false;
        ordered = false;

        waitingTime = 0f;
        customerSuccess = "waiting";
    }

    // Update is called once per frame
    void Update()
    {
        if (!reachedEntrance) //No ha llegado a la entrada
        {
            reachedEntrance = Move(transform.position, triggers[0].transform.position); //Se mueve hasta la entrada
        }
        
        else if((reachedEntrance) & (!reachedChair)) //Está en la entrada pero no ha elegido silla
        {
            reachedChair = ChooseChair(); //Escoge silla
        }

        else if((reachedChair) & (!ordered)) //Está sentado y todavía no ha pedido
        {
            orderedRecipe = orderRecipe.Order(this);
            table.AddOrder(orderedRecipe);
            maxWaitingTime = orderedRecipe.GetDeliveryTime();
            ordered = true;
        }

        else if ((reachedChair) & (ordered) & (customerSuccess.Equals("waiting"))) //Está sentado esperando su pedido
        {
            //Activar Canvas y Mostrar Satisfacción y la imagen del pedido que quiere
            canvas.gameObject.SetActive(true);
            canvas.transform.LookAt(mainCamera.transform);
            imageOrder.sprite = orderedRecipe.GetImageRecipe();

            //Esperar
            customerSuccess = Wait();
        }

        else if(customerSuccess.Equals("received")) //El cliente ha recibido su pedido
        {
            if (correctOrder)
            {
                Debug.Log("Estoy feliz");
                PutHappyFace();
            }
            else
            {
                PutMadFace();
            }
            transform.position = triggers[1].transform.position; //Se mueve hasta la puerta de salida
            customerSuccess = "exiting";
        }

        else if (customerSuccess.Equals("exiting")) //El cliente se va
        {
            if (!reachedEnd) //No ha llegado a irse
            {
                Exit();
            }
            else //Se ha ido
            {
                Destroy(gameObject);
            }

        }

    }

    #region Move, ChooseChair, ChairOccupied and Wait Logics
    private bool Move(Vector3 startPosition, Vector3 newPosition)
    {
        while (Vector3.Distance(startPosition, newPosition) >= 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            transform.LookAt(newPosition);
            animator.SetBool("sit", false);
            return false;
        }

        transform.position = newPosition;
        return true;

    }

    private bool ChooseChair()
    {
        StartCoroutine(Delay(1f));
        if (chairs == null)
        {
            chairs = GameObject.FindGameObjectsWithTag("Chair");
        }

        foreach (GameObject freeChair in chairs)
        {
            if (!ChairOccupied(freeChair))
            {
                Vector3 offset = new Vector3(0.7f, 0f, 0f);
                transform.position = freeChair.transform.position + freeChair.transform.rotation * offset;
                transform.rotation = freeChair.transform.rotation * Quaternion.Euler(0, -90, 0);

                animator.SetBool("sit", true);

                // Asignar el cliente a la silla
                chair = freeChair.GetComponent<Chair>();
                if (chair != null)
                {
                    chair.SetClient(this); // Registramos el cliente en la silla
                    table = chair.GetTable();
                }

                return true;
            }
        }
        return false;
    }

    private bool ChairOccupied(GameObject chair)
    {
        Collider[] colliders = Physics.OverlapSphere(chair.transform.position, 1f);
        foreach(Collider collider in colliders)
        {
            if (collider.CompareTag("Client")) 
            {
                return true; //La silla está ocupada
            }
        }
        return false; //La silla está libre
    }

    private string Wait() //Si devuelve 0 sigue esperando, si devuelve 1 le ha llegado el pedido, si devuelve 2 el tiempo de espera se ha agotado
    {
        if (waitingTime <= maxWaitingTime) //Mientras el tiempo que lleva esperando sea menor que el tiempo máximo de espera, espera
        {
            waitingTime += Time.deltaTime;
            UpdateWaitingImage(waitingTime);

            if(receivedOrder) //Si mientras está esperando llega su pedido
            {
                return "received"; //1 = recibido pedido
            }

            return "waiting"; //Sino, sigue esperando 0 = waiting
        }
        else //Cuando ha llegado al tiempo máximo de espera, se va enfadado
        {
            transform.position = triggers[1].transform.position; //Se mueve hasta la puerta de salida
            return "exiting"; // 2 = Se va
        }
    }

    private void Exit()
    {
        if (!hasEaten)
        {
            table.RemoveOrder(orderedRecipe);
        }
        canvas.transform.LookAt(mainCamera.transform);
        reachedEnd = Move(transform.position, triggers[2].transform.position);
    }

    #endregion

    #region Consume food
    private void OnOrderReceived()
    {
        if (correctOrder)
        {
            PutHappyFace();
        }
        else
        {
            PutMadFace();
        }

        // Consume el postre
        StartCoroutine(ConsumeFood());
    }

    private IEnumerator ConsumeFood()
    {
        // Tiempo de consumo de 2 segundos
        yield return new WaitForSeconds(2f);

        // Una vez consumido, el cliente se levanta y se va
        hasEaten = true;
        ClearChair();
        Exit();
    }

    private void ClearChair()
    {
        chair.ClearClient();
    }

    #endregion

    #region Update Canvas Logics
    private void UpdateWaitingImage(float waitTime)
    {
        float totalPercentageTime = 6;
        float partPercentagetime = 3; 

        if ((maxWaitingTime - waitTime) <= 10)
        {
            PutMadFace();
        }
        else if (waitTime > ((partPercentagetime * maxWaitingTime) / totalPercentageTime))
        {
            PutNeutralFace();
        }
        else
        {
            PutHappyFace();
        }
    }



    private void PutHappyFace()
    {
        imageFace.sprite = happyFace;
    }

    private void PutNeutralFace()
    {
        imageFace.sprite = neutralFace;
    }

    private void PutMadFace()
    {
        imageFace.sprite = madFace;
    }
    #endregion

    #region Delay Function
    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
    #endregion
}
