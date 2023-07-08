using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootHandler : MonoBehaviour
{
    public static ShootHandler instance;

    public GameObject bullet;

    public GameObject bulletDynamite;
    public float bulletforce;
    public Transform spawnpoint;
    Animator anim;
    [HideInInspector] public int blocks;

    public Animator shooteffect;

    public bool canshoot=false;


    public int maxAllowed = 10;
   [HideInInspector]public int count = 0;
    [HideInInspector] public int dcount = 0;

    [Header("UiElement")]
    public TextMeshProUGUI ballCount;
    public TextMeshProUGUI dynamitecount;
    public GameObject hand;

    public bool isDynamite = false;



    public GameObject canonbalsanim; 
    public GameObject dynamiteanim;
    public AudioSource gunshotsfx;
    // Start is called before the first frame update
    void Start()
    {


        canonbalsanim.SetActive(true);
        dynamiteanim.SetActive(false);
        instance = this;

        count = maxAllowed;
        dcount = maxAllowed;

        count +=PlayerPrefs.GetInt("bomb_levelcount", 1);
        dcount+= PlayerPrefs.GetInt("arrow_levelcount", 1);

        ballCount.text = "X" + count;
        dynamitecount.text = "X" + dcount;
        anim = GetComponent<Animator>();
    }
    public void setbullet()
    {
        UiHandler.instance.btnclick.PlayOneShot(UiHandler.instance.btnclick.clip);
        isDynamite = false;
        canonbalsanim.SetActive(true);
        dynamiteanim.SetActive(false);
    }
    public void setDynamite()
    {
        UiHandler.instance.btnclick.PlayOneShot(UiHandler.instance.btnclick.clip);
        isDynamite = true;
        canonbalsanim.SetActive(false);
        dynamiteanim.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {


        if(count<=0 && canshoot==true)
        {
            canshoot = false;

            UiHandler.instance.Gamelost();
        }
        if (canshoot == true)
        {
            if (Input.GetMouseButtonDown(0) && count>0)
            {
                RaycastHit hit;

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 500))
                {

                    if ( hit.collider.gameObject.tag.Equals("c1") || hit.collider.gameObject.tag.Equals("c2") || hit.collider.gameObject.tag.Equals("c3") || hit.collider.gameObject.tag.Equals("c4"))
                    {
                        if( isDynamite==true &&dcount<=0)
                        {
                            return;
                        }

                        if(hand.activeSelf)
                        {
                            hand.SetActive(false);
                        }
                        Vector3 hpoint = hit.collider.gameObject.transform.position;

                        float dis = Vector3.Distance(spawnpoint.position, hpoint);

                        GameObject obj = null;
                        gunshotsfx.PlayOneShot(gunshotsfx.clip);
                        if(isDynamite==false)
                        {
                            obj = Instantiate(bullet, null);
                            obj.tag = "bullet";
                        }else if(isDynamite==true)
                        {
                            obj = Instantiate(bulletDynamite, null);
                            obj.tag = "dynamite";
                        }

                        obj.transform.position = spawnpoint.position;
                        obj.transform.LookAt(hpoint);

                        var dir = spawnpoint.position - hpoint;


                        shooteffect.SetTrigger("play");
                        obj.GetComponent<Rigidbody>().AddForce(-dir * bulletforce, ForceMode.Impulse);

                        anim.SetTrigger("play");
                        if(isDynamite==false)
                        {
                            count--;

                        }
                        else if(isDynamite==true && dcount>0)
                        {
                            dcount--;

                        }
                        
                        ballCount.text = "X" + count;
                        dynamitecount.text = "X" + dcount;
                        //Destroy(hit.collider.gameObject);
                    }else if(hit.collider.gameObject.tag.Equals("bomb4"))
                    {
                        hit.collider.gameObject.GetComponentInChildren<cube>().checkbomb4destruction();
                    }
                    else if (hit.collider.gameObject.tag.Equals("bomb5"))
                    {
                        hit.collider.gameObject.GetComponentInChildren<cube>().bomb5blast();
                    }
                    else if (hit.collider.gameObject.tag.Equals("drone"))
                    {
                        hit.collider.gameObject.GetComponent<droneHandler>().destroyrows();
                    }
                    else if (hit.collider.gameObject.tag.Equals("tnt9"))
                    {
                        hit.collider.gameObject.GetComponent<cube>().destnt(9);
                    }
                    else if (hit.collider.gameObject.tag.Equals("tnt10"))
                    {
                        hit.collider.gameObject.GetComponent<cube>().destnt(10);
                    }

                }
            }
        }
    }
}
