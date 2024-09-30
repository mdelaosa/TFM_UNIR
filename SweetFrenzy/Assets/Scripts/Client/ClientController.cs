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

    void Update()
    {
        if (!reachedEntrance) 
        {
            reachedEntrance = Move(transform.position, triggers[0].transform.position); 
        }
        
        else if((reachedEntrance) & (!reachedChair)) 
        {
            reachedChair = ChooseChair(); 
        }

        else if((reachedChair) & (!ordered)) 
        {
            orderedRecipe = orderRecipe.Order(this);
            table.AddOrder(orderedRecipe);
            maxWaitingTime = orderedRecipe.GetDeliveryTime();
            ordered = true;
        }

        else if ((reachedChair) & (ordered) & (customerSuccess.Equals("waiting"))) 
        {
            canvas.gameObject.SetActive(true);
            canvas.transform.LookAt(mainCamera.transform);
            imageOrder.sprite = orderedRecipe.GetImageRecipe();

            customerSuccess = Wait();
        }

        else if(customerSuccess.Equals("received")) 
        {
            if (correctOrder)
            {
                OnOrderReceived();
                PutHappyFace();
            }
            else
            {
                PutMadFace();
            }
            transform.position = triggers[1].transform.position;
            customerSuccess = "exiting";
        }

        else if (customerSuccess.Equals("exiting")) 
        {
            if (!reachedEnd)
            {
                canvas.transform.LookAt(mainCamera.transform);
                Exit();
            }
            else 
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

                chair = freeChair.GetComponent<Chair>();
                if (chair != null)
                {
                    chair.SetClient(this); 
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
                return true;
            }
        }
        return false; 
    }

    private string Wait() 
    {
        if (waitingTime <= maxWaitingTime) 
        {
            waitingTime += Time.deltaTime;
            UpdateWaitingImage(waitingTime);

            if(receivedOrder) 
            {
                return "received"; 
            }

            return "waiting"; 
        }
        else 
        {
            transform.position = triggers[1].transform.position; 
            return "exiting"; 
        }
    }

    private void Exit()
    {
        if (!hasEaten)
        {
            table.RemoveOrder(orderedRecipe);
        }

        reachedEnd = Move(transform.position, triggers[2].transform.position);
    }

    #endregion

    #region Consume food
    public void OnOrderReceived()
    {
        StartCoroutine(ConsumeFood());
    }

    private IEnumerator ConsumeFood()
    {
        canvas.gameObject.SetActive(false);
        receivedOrder = true;
        correctOrder = true;

        yield return new WaitForSeconds(5f);

        Destroy(orderedRecipe.gameObject);

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
