using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    int rows = 4;
    [SerializeField]
    int cols = 4;
    [SerializeField]
    float tileSize = 50f;
    [SerializeField]
    GameObject gameButton,panelToAttachButtons;
    [SerializeField]
    public InputField rowsAndCols;
    Dictionary<int,ButtonHandler> TileList;
    GameManager gameManager;
    
    
    
   
    // Start is called before the first frame update
    void Start()
    {
        gameManager = new GameManager(gameManager);
        TileList = new Dictionary<int, ButtonHandler>();
        GenerateGrid(rows,cols);
        
        
    }
    

    public void GenerateGrid(int row,int col)
    {
        //Board'un uzunluğu
        float gridW = panelToAttachButtons.GetComponent<RectTransform>().rect.width * panelToAttachButtons.transform.localScale.x;
        print(gridW);
        //Board'un yüksekliği
        float gridH = panelToAttachButtons.GetComponent<RectTransform>().rect.height * panelToAttachButtons.transform.localScale.y;
        print(gridH);
        //Butonları board'a göre scale etme işlemi
        Vector2 buttonScale = new Vector2(gridW/cols,gridH/rows);
        panelToAttachButtons.GetComponent<GridLayoutGroup>().cellSize = buttonScale;
        panelToAttachButtons.GetComponent<GridLayoutGroup>().constraintCount = cols;
        //Butonun id'si
        int id = 0;

        GameObject referenceObject = Instantiate(gameButton) ;
        for ( col = 0; col < cols; col++)
        {
            for ( row = 0; row < rows; row++)
            {
                GameObject Tile = Instantiate(referenceObject,panelToAttachButtons.transform);
                //Komşulukları daha rahat bulabilmek için sanal bir pozisyon ataması yaptım.
                Tile.GetComponent<ButtonHandler>().GridPosition = new Point(row,col);
                //Hierarchy ekranında hangi buton nerede daha kolay anlamak için isiminin yanına sanal pozisyonunu ekledim.
                Tile.name = "Cell " + $"({row},{col})";
                Tile.GetComponent<ButtonHandler>().ID = id;
                //Debug.Log(Tile.GetComponent<ButtonHandler>().GridPosition.X+" "+ Tile.GetComponent<ButtonHandler>().GridPosition.Y);   
                TileList.Add(Tile.GetComponent<ButtonHandler>().ID,Tile.GetComponent<ButtonHandler>());
                id++;
            }
        }
        Dictionary<int,ButtonHandler> denemeList = GetTileList();
    }
   
    public Dictionary<int,ButtonHandler> GetTileList()
    {
        return TileList;
    }
    
   public void GenerateNewGrid()
    {
        //Inputfield boş mu?
        if (!string.IsNullOrEmpty(rowsAndCols.text))
        {
            foreach (Transform item in panelToAttachButtons.transform)
            {
                Destroy(item.gameObject);
            }
            rows = int.Parse(rowsAndCols.text);
            cols = int.Parse(rowsAndCols.text);
            //Daha önceki butonların kayıtlarını silmek gerekiyor yoksa dictionary'e eklenmiyor. Bu durumda yeni butonlar da oluşmuyor.
            TileList = new Dictionary<int, ButtonHandler>();
            GenerateGrid(rows, cols);
        }
        
    }
}
