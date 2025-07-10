using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 movedir;
    private float movespeed = 7f;
    private float rotatespeed = 10f;
    public bool isWalking;
    private Vector3 lastInterecdir;
    [SerializeField] private Transform cheese_clearcounter;
    [SerializeField] private Transform knife_clearcounter;
    [SerializeField] private Transform PAN_clearcounter;
    [SerializeField] private GameObject chesse;
    //[SerializeField] private GameObject cheese;
    [SerializeField] private Transform holdObject;
    private Transform holdingObject;
    private bool holding = false;
    private int score;
    private CharacterController controller;

    public bool canMove = true;
    
    public void EnableControl(bool enable)
    {
        canMove = enable;
    }

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) inputVector.y = +1;
        if (Input.GetKey(KeyCode.S)) inputVector.y = -1;
        if (Input.GetKey(KeyCode.A)) inputVector.x = -1;
        if (Input.GetKey(KeyCode.D)) inputVector.x = +1;

        inputVector = inputVector.normalized;
        Vector3 movedir = new Vector3(inputVector.x, 0f, inputVector.y);

        controller.Move(movedir * movespeed * Time.deltaTime);

        if (movedir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, movedir, Time.deltaTime * rotatespeed);
            isWalking = true;
            lastInterecdir = movedir;
        }
        else
        {
            isWalking = false;
        }
        if (Physics.Raycast(transform.position, lastInterecdir, out RaycastHit raycasthit, 1f))
        {
            if (raycasthit.transform.tag == "tp1")

            {
           
                transform.position = new Vector3(225.19f, 1.743f, 358.6f);
            }

            //Debug.Log(raycasthit.transform);
            //Debug.Log(clearcounter);

            //การเก็บของเข้ากล่อง
            if (raycasthit.transform == cheese_clearcounter)

            {
                //เช็คว่าถือและกด E ด้วยป่าว
                if (Input.GetKey(KeyCode.E) && holding)
                {
                    Destroy(holdingObject.gameObject);
                    holding = false;
                    CreatePrefeb();
                    movespeed = 7f;
                }
            }
            else if (raycasthit.transform.tag == "cheese" && !holding)

            {
                Debug.Log("cheese");
         
                if (!holding)
                {
                    raycasthit.transform.SetParent(holdObject);
                    //ตำแหน่งobject ที่ถือให้อยู่ตำแหน่ง 0 
                    raycasthit.transform.localPosition = Vector3.zero;
                    holding = true;
                    holdingObject = raycasthit.transform;
                

                }
            }
            if (raycasthit.transform.tag == "knife" && !holding)

            {
                Debug.Log("knife");
       
                if (!holding)
                {
                    raycasthit.transform.SetParent(holdObject);
                    raycasthit.transform.localPosition = Vector3.zero;
                    holding = true;
                    holdingObject = raycasthit.transform;
                    movespeed = 5f;
                   
                }
            }
            if (raycasthit.transform.tag == "PAN" && !holding)

            {
                Debug.Log("PAN");
               
                if (!holding)
                {
                    raycasthit.transform.SetParent(holdObject);
                    raycasthit.transform.localPosition = Vector3.zero;
                    holding = true;
                    holdingObject = raycasthit.transform;
                    movespeed = 5f;
               
                }
            }
        }

        //Debug.Log(hit);

      
    }

    public void CreatePrefeb()
    {
        // สร้างลิสต์ของแท็กที่ต้องการใช้
        string[] tags = { "cheese", "knife", "PAN" };

        GameObject prefabToSpawn = null;

        // ลองค้นหาวัตถุที่มีแท็กตามลำดับจนกว่าจะพบ
        foreach (string tag in tags)
        {
            prefabToSpawn = GameObject.FindWithTag(tag);
            if (prefabToSpawn != null)
            {
                break; // ถ้าพบวัตถุที่มีแท็กให้หยุดการค้นหา
            }
        }

        if (prefabToSpawn != null)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-5, 5),
                0,
                Random.Range(-5, 5)
            );
            int randomPrefab = Random.Range(0, 2);
            if (randomPrefab == 1)
            {
                prefabToSpawn = chesse;
            }
            // สร้าง instance ของ prefab
            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No object with specified tags found in the scene.");
        }
    } 
    

    public bool Iswalking()
    {
        return isWalking;
    }
}