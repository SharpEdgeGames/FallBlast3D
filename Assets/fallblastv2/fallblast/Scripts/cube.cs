using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour
{


    List<GameObject> cols;
    List<GameObject> temp;


    public GameObject bombprefab;
    public GameObject bomb5prefab;
    public GameObject TNTprefab;
    public GameObject TNT10prefab;
    GameHandler grid;
    public GameObject effect;
    public MeshRenderer basemesh;
    

   public bool visited = false;


    public bool thisobject = false;
    bool valset = false;

    public float blastforce = 20;
    // Start is called before the first frame update
    void Start()
    {
        cols = new List<GameObject>();

        grid = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();

        cols.Add(gameObject.transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           
              cleanlist();
        }
    }

    public void cleanlist()
    {


        for (int i = 0; i < grid.maxcols; i++)
        {
            for (int j = 0; j < grid.maxrows; j++)
            {

                if(grid.grid[i,j]!=null)
                grid.grid[i, j].GetComponent<cube>().visited = false;
                
            }
        }

    }
    
    public void checkbomb4destruction()
    {
        int i, j;
        i = 0; j = 0;

        i = int.Parse(gameObject.name);
        j = int.Parse(gameObject.transform.GetChild(0).name);

        //i==y
        //j==x
        int y = i;
        int x = j;

        if (y + 1 < grid.maxcols && x - 1 >= 0)
            if (grid.grid[y + 1, x - 1] != null)
            {
                updateGrid(y + 1, x - 1);
            }


        if (y + 1 < grid.maxcols)
            if (grid.grid[y + 1, x] != null)
            {

                updateGrid(y + 1, x);
            }



        if (y + 1 < grid.maxcols && x + 1 < grid.maxrows)
            if (grid.grid[y + 1, x + 1] != null)
            {
                updateGrid(y + 1, x + 1);
            }

        if (x - 1 >= 0)
            if (grid.grid[y, x - 1] != null)
            {
                updateGrid(y, x - 1);
            }
        updateGrid(y, x);

        if (x + 1 < grid.maxrows)
            if (grid.grid[y, x + 1] != null)
            {
                updateGrid(y, x + 1);
            }

        if (y - 1 >= 0 && x - 1 >= 0)
            if (grid.grid[y - 1, x - 1] != null)
            {
                updateGrid(y - 1, x - 1);
            }

        if (y - 1 >= 0)
            if (grid.grid[y - 1, x] != null)
            {
                updateGrid(y - 1, x);
            }

     
        if (y - 1 >= 0 && x + 1 < grid.maxrows)
            if (grid.grid[y - 1, x + 1] != null)
            {
                updateGrid(y - 1, x + 1);
            }


    }

    public void spawnbomb5(int i,int j) 
    {
        var pos = grid.grid[i, j].transform.localPosition;
        grid.grid[i, j].GetComponent<Rigidbody>().AddForce(Vector3.up * 300*UiHandler.instance.blastupforce, ForceMode.Impulse);

        var bomb = Instantiate(bomb5prefab, transform.parent);
        bomb.transform.localPosition = pos;
       
        bomb.gameObject.tag = "bomb5";
        GameObject[,] newgrid;
        grid.maxcols++;
        newgrid = new GameObject[grid.maxcols, grid.maxcols];



        for (int x = 0; x < grid.maxcols; x++)
        {
            for (int y = 0; y < grid.maxrows; y++)
            {



                if (x >= grid.maxcols - 1)
                {
                    Debug.Log("null" + y);
                }
                else
                {
                    newgrid[x, y] = grid.grid[x, y];

                    if (newgrid[x, y] != null)
                        Debug.Log(newgrid[x, y].name + " >>> " + y);
                }
            }
        }
        grid.grid = new GameObject[grid.maxcols, grid.maxcols];
        for (int x = 0; x < grid.maxcols; x++)
        {
            for (int y = 0; y < grid.maxrows; y++)
            {




                grid.grid[x, y] = newgrid[x, y];


            }
        }

        
        for (int x = grid.maxcols -1; x > i; x--)
        {

            if (x - 1 >= 0)
            {
                grid.grid[x, j] = newgrid[x - 1, j];

                if (grid.grid[x, j] != null)
                {
                    grid.grid[x, j].gameObject.name = "" + x;
                    grid.grid[x, j].gameObject.transform.GetChild(0).name = "" + j;
                }

            }
            
            

        }
        bomb.gameObject.name = "" + i;
        bomb.gameObject.transform.GetChild(0).name = "" + j;
        grid.grid[i, j] = bomb;




   

    }

    public void spawntnt(int i, int j)
    {
        var pos = grid.grid[i, j].transform.localPosition;
        grid.grid[i, j].GetComponent<Rigidbody>().AddForce(Vector3.up * 300 * UiHandler.instance.blastupforce, ForceMode.Impulse);

        var bomb = Instantiate(TNTprefab, transform.parent);
        bomb.transform.localPosition = pos;

        bomb.gameObject.tag = "tnt9";
        GameObject[,] newgrid;
        grid.maxcols++;
        newgrid = new GameObject[grid.maxcols, grid.maxcols];



        for (int x = 0; x < grid.maxcols; x++)
        {
            for (int y = 0; y < grid.maxrows; y++)
            {



                if (x >= grid.maxcols - 1)
                {
                    Debug.Log("null" + y);
                }
                else
                {
                    newgrid[x, y] = grid.grid[x, y];

                    if (newgrid[x, y] != null)
                        Debug.Log(newgrid[x, y].name + " >>> " + y);
                }
            }
        }
        grid.grid = new GameObject[grid.maxcols, grid.maxcols];
        for (int x = 0; x < grid.maxcols; x++)
        {
            for (int y = 0; y < grid.maxrows; y++)
            {




                grid.grid[x, y] = newgrid[x, y];


            }
        }


        for (int x = grid.maxcols - 1; x > i; x--)
        {

            if (x - 1 >= 0)
            {
                grid.grid[x, j] = newgrid[x - 1, j];

                if (grid.grid[x, j] != null)
                {
                    grid.grid[x, j].gameObject.name = "" + x;
                    grid.grid[x, j].gameObject.transform.GetChild(0).name = "" + j;
                }

            }



        }
        bomb.gameObject.name = "" + i;
        bomb.gameObject.transform.GetChild(0).name = "" + j;
        grid.grid[i, j] = bomb;


    }
    public void spawntnt10(int i, int j)
    {
         
        var pos = grid.grid[i, j].transform.localPosition;
        grid.grid[i, j].GetComponent<Rigidbody>().AddForce(Vector3.up * 300 * UiHandler.instance.blastupforce, ForceMode.Impulse);

        var bomb = Instantiate(TNT10prefab, transform.parent);
        bomb.transform.localPosition = pos;

        bomb.gameObject.tag = "tnt10";
        GameObject[,] newgrid;
        grid.maxcols++;
        newgrid = new GameObject[grid.maxcols, grid.maxcols];



        for (int x = 0; x < grid.maxcols; x++)
        {
            for (int y = 0; y < grid.maxrows; y++)
            {



                if (x >= grid.maxcols - 1)
                {
                    Debug.Log("null" + y);
                }
                else
                {
                    newgrid[x, y] = grid.grid[x, y];

                    if (newgrid[x, y] != null)
                        Debug.Log(newgrid[x, y].name + " >>> " + y);
                }
            }
        }
        grid.grid = new GameObject[grid.maxcols, grid.maxcols];
        for (int x = 0; x < grid.maxcols; x++)
        {
            for (int y = 0; y < grid.maxrows; y++)
            {




                grid.grid[x, y] = newgrid[x, y];


            }
        }


        for (int x = grid.maxcols - 1; x > i; x--)
        {

            if (x - 1 >= 0)
            {
                grid.grid[x, j] = newgrid[x - 1, j];

                if (grid.grid[x, j] != null)
                {
                    grid.grid[x, j].gameObject.name = "" + x;
                    grid.grid[x, j].gameObject.transform.GetChild(0).name = "" + j;
                }

            }



        }
        bomb.gameObject.name = "" + i;
        bomb.gameObject.transform.GetChild(0).name = "" + j;
        grid.grid[i, j] = bomb;


    }

    public void  bomb5blast()
    {
        int i = int.Parse(gameObject.name);

        for (int j = 0; j <grid.maxrows; j++)
        {
            updateGrid(i, j);
        }
    }
    public void desthisobj()
    {
        int i, j;
        i = 0; j = 0;

        i = int.Parse(gameObject.name);
        j = int.Parse(gameObject.transform.GetChild(0).name);

        
        
        var obj = Instantiate(effect, null);
        obj.transform.position = grid.grid[i, j].transform.position + new Vector3(0, 0, -1f);

        var ps = obj.transform.GetChild(0).GetComponent<ParticleSystem>().main;


        ps.startColor = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
        Destroy(obj, 3);
        updateGrid(i, j);

    }

    public void destnt(int n)
    {
        Debug.Log("dest9 dest");
        int i, j;
        i = 0; j = 0;

        i = int.Parse(gameObject.name);
        j = int.Parse(gameObject.transform.GetChild(0).name);



        for (int x = i+n; x >= i; x--)
        {
            for (int y = 0; y < grid.maxrows; y++)
            {

                if(x<=grid.maxcols-1)
                if (grid.grid[x, y] != null)
                {
                   

                        updateGrid(x, y);

                    
                }


            }

        }
    }
    public void checkcollision(bool isbullet)
    {
        printGrid();


        int i, j;
        i = 0; j = 0;

        i = int.Parse(gameObject.name);
        j = int.Parse(gameObject.transform.GetChild(0).name);
        Debug.Log("Checking : col" + i + ", " + j);
        grid.collectedlist = new List<GameObject>();
        grid.collectedlist.Add(gameObject);
        //checkup();
      //  checkleft();
      //  checkdown();

        if(grid.locked==false)
        {
            thisobject = true;
            grid.locked = true;

        }
     
        
            printGrid();
        if (isbullet == true)
        {
            check(new Vector2(i, j), 1, true, true, true, true);


            Debug.Log("collected val : " + grid.collectedlist.Count);

            if (grid.collectedlist.Count == 3)
            {
                for (int x = grid.maxcols - 1; x >= 0; x--)
                {
                    for (int y = 0; y < grid.maxrows; y++)
                    {

                        if (grid.grid[x, y] != null)
                        {
                            if (doexist(x, y))
                            {
                                updateGrid(x, y);

                            }
                        }


                    }
                }

            }
            else
            if (grid.collectedlist.Count == 4)
            {

                //  updateGrid(i, j);
                var bomb = Instantiate(bombprefab, grid.grid[i, j].transform.parent);

                var obj = Instantiate(effect, null);
                obj.transform.position = grid.grid[i, j].transform.position + new Vector3(0, 0, -1f);

                var ps = obj.transform.GetChild(0).GetComponent<ParticleSystem>().main;


                ps.startColor = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;

                bomb.name = "" + i;
                bomb.transform.GetChild(0).name = "" + j;
                Destroy(obj, 3);
                bomb.transform.localPosition = grid.grid[i, j].transform.localPosition;
                Destroy(grid.grid[i, j]);

                grid.grid[i, j] = bomb;

                for (int x = grid.maxcols - 1; x >= 0; x--)
                {
                    for (int y = 0; y < grid.maxrows; y++)
                    {

                        if (x == i && y == j)
                        {

                        }
                        else
                        {
                            if (grid.grid[x, y] != null)
                            {
                                if (doexist(x, y))
                                {
                                    updateGrid(x, y);
                                }
                            }
                        }

                    }
                }
            }
            else if (grid.collectedlist.Count == 5)
            {
                for (int x = grid.maxcols - 1; x >= 0; x--)
                {
                    for (int y = 0; y < grid.maxrows; y++)
                    {

                        if (grid.grid[x, y] != null)
                        {
                            if (doexist(x, y))
                            {
                                updateGrid(x, y);

                            }
                        }


                    }
                }


                int r1 = Random.Range(1, grid.maxrows);

                
                if(grid.grid[i,r1]!=null)
                spawnbomb5(i, r1);

                int r2 = Random.Range(1, grid.maxrows);

                if (r2 == r1)
                {
                    if (r2 + 1 >= grid.maxrows)
                    {
                        r2 -= 1;
                    }
                    else if (r2 - 1 < 0)
                    {
                        r2 += 1;
                    }
                    else
                    {
                        r2 += 1;
                    }
                }

                if (grid.grid[i, r1] != null)
                    spawnbomb5(i - 1, r2);

                // spawnbomb5(i, );
            }
            else if (grid.collectedlist.Count == 6 || grid.collectedlist.Count == 7 || grid.collectedlist.Count == 8)
            {

                for (int x = grid.maxcols - 1; x >= 0; x--)
                {
                    for (int y = 0; y < grid.maxrows; y++)
                    {

                        if (grid.grid[x, y] != null)
                        {
                            if (doexist(x, y))
                            {
                                updateGrid(x, y);

                            }
                        }


                    }
                }


                droneHandler.instance.activateDrone(basemesh.material.color, i, 6, gameObject.tag);


            }
            else if (grid.collectedlist.Count == 9)
            {
                for (int x = grid.maxcols - 1; x >= 0; x--)
                {
                    for (int y = 0; y < grid.maxrows; y++)
                    {

                        if (grid.grid[x, y] != null)
                        {
                            if (doexist(x, y))
                            {
                                updateGrid(x, y);

                            }
                        }


                    }
                }
                for (int x = i; x >= 0; x--)
                {
                    if (grid.grid[x, j] != null)
                    {
                        spawntnt(x, j);
                        x = -1;
                    }
                }

            }
            else if (grid.collectedlist.Count >= 10)
            {
                for (int x = grid.maxcols - 1; x >= 0; x--)
                {
                    for (int y = 0; y < grid.maxrows; y++)
                    {

                        if (grid.grid[x, y] != null)
                        {
                            if (doexist(x, y))
                            {
                                updateGrid(x, y);

                            }
                        }


                    }
                }

                
                for (int x=i;x>=0;x--)
                {
                    if(grid.grid[x, j] != null)
                    {
                        spawntnt10(x, j);
                        x = -1;
                    }
                }

                
            }
            
        }else if(isbullet==false) // when dynamite hit
        {
          
            updateGrid(i, j);
        }
    }


    bool doexist(int x,int y)
    {
        
        foreach (var item in grid.collectedlist)
        {
            int i, j;
            i = 0; j = 0;

            if (item != null)
            {
                i = int.Parse(item.gameObject.name);
                j = int.Parse(item.gameObject.transform.GetChild(0).name);



                if (x == i && y == j)
                {
                    Debug.Log("destroying : (" + i + "," + j + ")");
                    return true;
                }
            }
        }

        return false;
    }
    public void check(Vector2 pos,int step,bool right, bool up,bool left, bool down)
    {
        Debug.Log("Step val >>>>>>>>>>>>>>>>>>>" + step);

        int n = 0;
        int i, j;
        i = (int)pos.x; j = (int)pos.y;


  

        if (i >= 0 && j >= 0 && i < grid.maxcols && j < grid.maxrows  )
        {


            if (right == true)
            {
                if (j + step < grid.maxrows)
                {
                    if (grid.grid[i, j + step] != null && grid.grid[i, j] != null)
                    {
                        if (grid.grid[i, j].gameObject.tag.Equals(grid.grid[i, j + step].gameObject.tag) && grid.grid[i, j + step].GetComponent<cube>().visited==false)
                        {
                            grid.grid[i, j+step].GetComponent<cube>().visited = true;
                            Debug.Log("right cell : " + i + "," + j + "    >>>> " + visited);

                            grid.collectedlist.Add(grid.grid[i , j+step].gameObject);


                            check(new Vector2(pos.x, pos.y+step), 1,false,true,false,true);
                            n++;
                        }
                        else
                        {
                            right = false;
                        }

                    }
                    else
                    {
                        right = false;
                    }
                }
                else
                {
                    right = false;
                }
            }
            if (up == true)
            {
                if (i + step < grid.maxcols)
                {
                    if (grid.grid[i + step, j] != null && grid.grid[i, j] != null)
                    {
                        if (grid.grid[i, j].gameObject.tag.Equals(grid.grid[i + step, j].gameObject.tag) && grid.grid[i+step, j ].GetComponent<cube>().visited == false)
                        {
                            grid.grid[i+step, j].GetComponent<cube>().visited = true;
                            Debug.Log(" up cell : " + i + "," + j + "    >>>> " + visited);

                            grid.collectedlist.Add(grid.grid[i + step, j].gameObject);
                            check(new Vector2(pos.x+step, pos.y ), 1,true,false,true,false);
                            n++;
                        }
                        else
                        {
                            up = false;
                        }

                    }
                    else
                    {
                        up = false;
                    }
                }
                else
                {
                    right = false;
                }

            }
            if (left == true)
            {
                if (j - step >= 0)
                {
                    if (grid.grid[i, j - step] != null && grid.grid[i, j] != null)
                    {
                        if (grid.grid[i, j].gameObject.tag.Equals(grid.grid[i, j - step].gameObject.tag) && grid.grid[i, j - step].GetComponent<cube>().visited ==false)
                        {
                            grid.grid[i, j - step].GetComponent<cube>().visited = true;
                            Debug.Log(" left cell : " + i + "," + j + "    >>>> " + visited);

                            grid.collectedlist.Add(grid.grid[i , j-step].gameObject);
                            check(new Vector2(pos.x, pos.y - step), 1, false, true, false, true);
                            n++;
                        }
                        else
                        {
                            left = false;
                        }

                    }
                    else
                    {
                        left = false;
                    }
                }
                else
                {
                    right = false;
                }
            }
            if (down == true)
            {
                if (i - step >= 0)
                {

                    if (grid.grid[i - step, j] != null && grid.grid[i, j] != null)
                    {
                        if (grid.grid[i, j].gameObject.tag.Equals(grid.grid[i - step, j].gameObject.tag) && grid.grid[i - step, j].GetComponent<cube>().visited ==false)
                        {
                            grid.grid[i - step, j].GetComponent<cube>().visited = true;
                            Debug.Log("down cell : " + i + "," + j + "    >>>> " + visited);

                            grid.collectedlist.Add(grid.grid[i - step, j].gameObject);
                            check(new Vector2(pos.x - step, pos.y), 1, true, false, true, false);
                            n++;
                        }
                        else
                        {
                            down = false;
                        }

                    }
                    else
                    {
                        down = false;
                    }
                }
                else
                {
                    right = false;
                }
            }


        }

        if ((step+1) < grid.maxcols && n>0)
        {
            check(pos,step + 1, right, up, left, down);

        }
    }
    


    public void printGrid()
    {
        string str = "";
        for (int i = 0; i < grid.maxcols; i++)
        {
            for (int j = 0; j < grid.maxrows; j++)
            {

                if (grid.grid[i, j] != null)
                    str += "   " + grid.grid[i, j].tag;
                else
                    str += "   NN";

            }
            str += "\n";
        }

        Debug.Log(str);
    }
  


    public void updateGrid(int i,int j)
    {

        //int i, j;
        //i = 0; j = 0;

        //i = int.Parse(gameObject.name);
        //j = int.Parse(gameObject.transform.GetChild(0).name);

        if (grid.grid[i, j] != null)
        {
            var obj = Instantiate(effect, null);

            obj.transform.position = grid.grid[i, j].transform.position + new Vector3(0, 0, -1f);

            var ps = obj.transform.GetChild(0).GetComponent<ParticleSystem>().main;


            ps.startColor = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
            Destroy(obj, 3);
        }
        Destroy(grid.grid[i, j]);

        grid.grid[i, j] = null;

        for (int x = i; x < grid.maxcols; x++)
        {


            if (x + 1 < grid.maxcols && grid.grid[x + 1, j] != null)
            {

                grid.grid[x, j] = grid.grid[x + 1, j];
              
                grid.grid[x, j].name = "" + (x);
                grid.grid[x, j].transform.GetChild(0).name = "" + (j);
            }
            else
            {
                grid.grid[x, j] = null;

                //  grid.grid[x, j].name = "" + (x);
                //   grid.grid[x, j].transform.GetChild(0).name = "" + (j);

            }




        }




       
       

        
       


    }











    private IEnumerator animate()
    {


                gameObject.transform.GetChild(0).transform.localScale *= 1.5f;
                yield return new WaitForSeconds(0.2f);

                gameObject.transform.GetChild(0).transform.localScale /= 1.5f;
    
    }

    public void checkSideCubes(Vector2 pos)
    {




    }



    
}
