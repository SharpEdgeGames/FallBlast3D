using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("c1") || other.gameObject.tag.Equals("c2") || other.gameObject.tag.Equals("c3") || other.gameObject.tag.Equals("c4"))
        {
            // Destroy(other.gameObject);
            // Debug.Log("shot at : (" + other.gameObject.name + "," + other.gameObject.transform.GetChild(0).name + ")");
            other.gameObject.GetComponentInChildren<cube>().desthisobj();
            // other.gameObject.GetComponentInChildren<cube>().checkcolleft();

            // var obj = Instantiate(tempPref, other.gameObject.transform.parent);

            //  obj.name = "-1";
            //    obj.transform.GetChild(0).name = "-1";

            Destroy(gameObject);
        }


    }
}
