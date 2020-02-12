using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public int ID { get; set; }
    public Button button;
    public Text buttonText;
    string mark = "X";
    public bool isMarked = false;
    public Point GridPosition { get; set; }
    List<Point> neighborList;
    
    public Dictionary<int,ButtonHandler> buttonList;
    GameManager gameManager;
    Stack<GameObject> travelledRoad;
    GameObject board;
    private void Start()
    {
        board = GameObject.Find("BoardManager");
        gameManager = new GameManager(gameManager);
        neighborList = new List<Point>();
        travelledRoad = new Stack<GameObject>();
        GetNeighbors();
    }

    public void markButton()
    {
        if (!isMarked)
        {
            isMarked = true;
           // print(GridPosition.X+","+GridPosition.Y);
            buttonText.text = mark;
            FindMarkedButton(gameObject);
            
        }
        else if (isMarked)
        {
            isMarked = false;
            //print(GridPosition.X + "," + GridPosition.Y);
            buttonText.text = "";
        }
    }
    //Sanal pozisyonlara göre komşulukları listeye atıyoruz. Her buton kendi komşusunun sanal pozisyonunu bilecek.
    public void GetNeighbors()
    {
        for (int x = GridPosition.X-1; x <= GridPosition.X + 1; x++)
        {
            for (int y = GridPosition.Y -1; y <= GridPosition.Y+ 1; y++)
            {
                Point neighborPosition = new Point(x, y);
                if (neighborPosition == GridPosition)
                {
                    continue;
                }
                neighborList.Add(neighborPosition);
            }
        }
    }
    
    void FindMarkedButton(GameObject obj)
    {
        //Bulunduğum hücreyi gezdiğim yollara ekledim.
        travelledRoad.Push(gameObject);
        //Tüm butonları aldım.
        buttonList = board.GetComponent<BoardManager>().GetTileList();
        //Kaç hücre gezdim?
        int count = 0;
        //Board'daki butonların içinde dönüyorum
        foreach(var button in buttonList)
        {
           //Komşularımı kontrol ediyorum
            foreach (Point neighborLocation in neighborList)
            {
                
                
                //Foreach loop'undan gelen item benim komşum mu?
                if (button.Value.GridPosition == neighborLocation)
                {
                 //Komşum ise gir   
                    var komsuHucre = button.Value;
                    //Komşumda mark'lı hücre var mı ve oraya gittim mi?
                    if (komsuHucre.isMarked && !travelledRoad.Contains(komsuHucre.gameObject))
                    {
                        //Burada bir sıkıntı yok
                        print("Ha burada X Var: " + button.Value.isMarked + "," + button.Value.gameObject.name);
                        count++;
                        //Buraya gelindiği anda oyunum donuyor. Yeterince optimize edemedim.
                        FindMarkedButton(komsuHucre.gameObject);

                    }
                    else
                    {
                        //Komşularımda mark yok
                        print("Bulamadım");
                    }
                    if (count == 3)
                    {
                        Debug.Log("Win");
                        return;
                        
                    }
                    
                }
            }
        }
        
    }
   

}
