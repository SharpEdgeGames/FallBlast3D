using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winset : MonoBehaviour
{

    public GameObject glassbox;
    public GameObject effect;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void diableglassbox()
    {
        var ps = Instantiate(effect, null);
        ps.transform.position = transform.position + new Vector3(0, 0, -1);
        Destroy(ps, 3);
        glassbox.SetActive(false);

        anim.SetTrigger("play");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("ground"))
        {
            UiHandler.instance.reachedcount++ ;
            diableglassbox();
        }
    }
}
