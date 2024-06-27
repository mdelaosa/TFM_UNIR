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
    private GameObject[] chairs;
    private GameObject[] triggers;

    [SerializeField] private float speed;

    private bool reachedEntrance;
    private bool reachedChair;
    private bool reachedEnd;
    private bool ordered;

    private OrderRecipe orderRecipe;
    private Order orderedRecipe;

    private int customerSuccess;
    private float waitingTime;
    [SerializeField] private float maxWaitingTime;

    [SerializeField] private Canvas canvas; 
    [SerializeField] private Image imageFace;
    [SerializeField] private Image imageOrder;
    [SerializeField] private Sprite happyFace;
    [SerializeField] private Sprite neutralFace;
    [SerializeField] private Sprite madFace;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;

        triggers = GameObject.FindGameObjectsWithTag("Trigger");
        triggers = triggers.OrderBy(triggers => triggers.name).ToArray();

        orderRecipe = FindObjectOfType<OrderRecipe>();

        reachedEntrance = false;
        reachedChair = false;
        reachedEnd = false;
        ordered = false;

        waitingTime = 0f;
        customerSuccess = 0;
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
            orderedRecipe = orderRecipe.Order();
            maxWaitingTime = orderedRecipe.GetDeliveryTime();
            ordered = true;
        }

        else if ((reachedChair) & (ordered) & (customerSuccess == 0)) //Está sentado esperando su pedido
        {
            //Activar Canvas y Mostrar Satisfacción y la imagen del pedido que quiere
            canvas.gameObject.SetActive(true);
            imageOrder.sprite = orderedRecipe.GetImageRecipe();

            //Esperar
            customerSuccess = Wait();
        }

        else if(customerSuccess == 1) //El cliente ha recibido su pedido
        {
            
        }

        else if (customerSuccess == 2) //El cliente se ha cansado de esperar y se va
        {
            if (!reachedEnd) //No ha llegado a irse
            {
                reachedEnd = Move(transform.position, triggers[2].transform.position); //Se mueve hasta la salida
            }
            else //Se ha ido
            {
                Destroy(gameObject);
            }

        }

    }


    private bool Move(Vector3 startPosition, Vector3 newPosition)
    {
        while (Vector3.Distance(startPosition, newPosition) >= 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
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

        foreach (GameObject chair in chairs)
        {
            if (!ChairOccupied(chair))
            {
                //Move(transform.position, chair.transform.position);
                transform.position = chair.transform.position;
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
            if (collider.CompareTag("Client") || collider.CompareTag("Plate")) //Si no hay una persona ni un plato
            {
                return true; //La silla está ocupada
            }
        }
        return false; //La silla está libre
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    private int Wait() //Si devuelve 0 sigue esperando, si devuelve 1 le ha llegado el pedido, si devuelve 2 el tiempo de espera se ha agotado
    {
        if (waitingTime <= maxWaitingTime) //Mientras el tiempo que lleva esperando sea menor que el tiempo máximo de espera, espera
        {
            waitingTime += Time.deltaTime;
            UpdateWaitingImage(waitingTime);
            return 0;
        }
        else //Cuando ha llegado al tiempo máximo de espera, se va enfadado
        {
            transform.position = triggers[1].transform.position; //Se mueve hasta la puerta de salida
            return 2;
        }
    }

    private void UpdateWaitingImage(float waitTime)
    {
        if (waitTime <= maxWaitingTime / 3) //Si el tiempo de espera es menor que 1/3, es que lleva 2/3 esperando
        {
            imageFace.sprite = happyFace;
        }
        else if ((waitTime > maxWaitingTime / 3) & (waitTime <= (2 * maxWaitingTime) / 3)) //Si el tiempo de espera es menor que 2/3 el tiempo máximo de espera, es que lleva 1/3 esperando
        {
            imageFace.sprite = neutralFace;
        }
        else if ((waitTime > (2 * maxWaitingTime) / 3) & (waitTime <= maxWaitingTime)) //Sino, es que lleva menos de 1/3 esperando
        {
            imageFace.sprite = madFace;
        }
    }
}
