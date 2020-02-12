using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance { get; set; }
    private static GameManager Instance;

    Dictionary<GameObject, bool> buttons;
    // Start is called before the first frame update
    public GameManager(GameManager gameManager)
    {
        Instance = gameManager;
        buttons = new Dictionary<GameObject, bool>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddButtonsFromBoard(GameObject button)
    {
        this.buttons.Add(button, button.GetComponent<ButtonHandler>().isMarked);
    }
    public void Printdict()
    {
        print(buttons.Count);
        
    }
    public void UpdateAButton(GameObject button)
    {
        buttons[button] = button.GetComponent<ButtonHandler>().isMarked;
        print(buttons[button]);
    }
    public Dictionary<GameObject, bool> GetButtonList()
    {
        return buttons;
    }
}
