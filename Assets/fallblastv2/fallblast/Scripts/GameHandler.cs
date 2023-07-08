using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{

    public float time = 0.5f;
    public GameObject[] cubes;
   
    public GameObject[,] grid;
    public List<GameObject> collectedlist;
    public Transform cubesparent;

    public Transform peopleparent;
    public int maxcols;
    public int maxrows;

  public  bool locked = false;
    public GameObject people;


    public Image tutorialhand;
    // Start is called before the first frame update
    void Start()
    {
      //  PlayerPrefs.SetInt("level", 1);
      //  PlayerPrefs.Save();
        maxcols += PlayerPrefs.GetInt("level", 1);
        StartCoroutine(GenerateCubes());


        collectedlist = new List<GameObject>();
        grid = new GameObject[maxcols, maxrows];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showHandTutorial()
    {
        bool found = false;
        int n = 0;
        for (int x = 0; x <maxcols; x++)
        {

            for (int y = 0; y < maxrows; y++)
            {

                if (found == false && y+1<maxrows)
                    if(grid[x, y]!=null && grid[x, y+1]!=null)
                if (grid[x, y].gameObject.tag.Equals(grid[x, y+1].gameObject.tag)  )
                {
                    if (grid[x, y ] != null)
                    {
                    //    tutorialhand.rectTransform.position = Camera.main.WorldToScreenPoint(obj.transform.position);
                            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, grid[x,y].transform.position);

                            // convert the screen position to the local anchored position

                            Vector2 anchoredPosition = transform.InverseTransformPoint(screenPoint);

                            tutorialhand.rectTransform.position = screenPoint+new Vector2(25,-35);

                                n++;
                                if (n == 3)
                                {
                                    found = true;
                                    break;
                                }
                    }
                }


            }
        }


    }
    public void doanim()
    {
        StartCoroutine(animate());
    }
    private IEnumerator animate()
    {


        for (int i = 0; i < maxcols; i++)
        {
            for (int j = 0; j < maxrows; j++)
            {
                grid[i, j].transform.GetChild(0).transform.localScale *= 1.5f;
                yield return new WaitForSeconds(time*2);

                grid[i, j].transform.GetChild(0).transform.localScale /= 1.5f;

            }
        }

    }


    public void cleanlist()
    {


        for (int i = 0; i < maxcols; i++)
        {
            for (int j = 0; j < maxrows; j++)
            {

                if (grid[i, j] != null)
                    grid[i, j].GetComponent<cube>().visited = false;

            }
        }

    }


    public IEnumerator GenerateCubes()
    {

        Vector3 lastpos = Vector3.zero;

        for (int i = 0; i < maxcols; i++)
        {
            for (int j = 0; j < maxrows; j++)
            {
                yield return new WaitForEndOfFrame();

                var obj = Instantiate(cubes[Random.Range(0, cubes.Length)], cubesparent);
                obj.transform.localPosition = new Vector3(j - 2, i+1, 0);
                obj.name=""+i;
                obj.transform.GetChild(0).name = "" + j;

                lastpos= new Vector3(j - 3, i + 1, 0); ;
                grid[i, j] = obj;
            }
        }



            var obj1 = Instantiate(people, peopleparent);
            obj1.transform.localPosition =new Vector3(-1.35f, lastpos.y+3, 0);
            var obj2 = Instantiate(people, peopleparent);
            obj2.transform.localPosition = new Vector3(0.35f, lastpos.y + 3, 0);
            var obj3 = Instantiate(people, peopleparent);
            obj3.transform.localPosition = new Vector3(2f, lastpos.y + 3, 0);



        cleanlist();

    }
}
