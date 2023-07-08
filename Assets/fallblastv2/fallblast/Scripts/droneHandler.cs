using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneHandler : MonoBehaviour
{

    public static droneHandler instance;
    public MeshRenderer basemesh;
    public Transform homebase;
    public Transform p1; 
    public Transform p2;
    public float speed = 1;
    bool right = true;
    GameHandler grid;
    bool valset = false;


    bool canmove = false;
    bool goback = false;



    int ipos = 0;
    int descount = 1;



    public Transform spawnpoint;
    public GameObject fireball;
    public float fireballforce;

    public GameObject btn;
    public string tag = "c1";
    // Start is called before the first frame update
    void Start()
    {

        btn.SetActive(false);
        instance = this;
        grid = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
    }




    public void setvals(int i,int desc )
    {
        ipos = i;
        descount = desc;
    }


    private IEnumerator shootnow()
    {
        int tempi = ipos;


        for (int i = grid.maxcols-1; i >=0 ; i--)
        {


            for (int j = 0; j < grid.maxrows; j++)
            {
                if(grid.grid[i, j]!=null)
                if (grid.grid[i, j].tag.Equals(tag))
                {
                    Vector3 hpoint = grid.grid[i, j].gameObject.transform.position;

                    float dis = Vector3.Distance(spawnpoint.position, hpoint);

                    GameObject obj = null;

                    obj = Instantiate(fireball, null);
                    obj.tag = "bullet";
                    obj.transform.position = spawnpoint.position;
                    obj.transform.LookAt(hpoint);

                    var dir = spawnpoint.position - hpoint;

                    obj.GetComponent<Rigidbody>().AddForce(-dir * fireballforce, ForceMode.Impulse);
                    yield return new WaitForSeconds(0.1f);
                }
            }

       
        }
        canmove = false;
    }
    public void destroyrows()
    {




        StartCoroutine(shootnow());

        btn.SetActive(false);
    }
    public void activateDrone(Color clr, int i, int desc,string str)
    {
        if (canmove == false)
        {
            //   basemesh.material.color = clr;
            var m = basemesh.GetComponent<MeshRenderer>();
            m.material.color = clr;
            tag = str;
            btn.SetActive(true);
            canmove = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (canmove == true )
        {
            if (right == true)
            {

                if (Vector3.Distance(transform.position, p2.position) < 1)
                {
                    right = false;

                    if (valset == false)
                    {

                        speed = speed / 2;
                        valset = true;
                    }


                }



                transform.position = Vector3.MoveTowards(transform.position, p2.position, speed * Time.deltaTime);

            }
            else if (right == false)
            {
                if (Vector3.Distance(transform.position, p1.position) < 1)
                {
                    right = true;
                }

                transform.position = Vector3.MoveTowards(transform.position, p1.position, speed * Time.deltaTime);
            }

        }
        else
        {
           
        
            transform.position = Vector3.MoveTowards(transform.position, homebase.position, speed * Time.deltaTime);

        }
    }

   






}
