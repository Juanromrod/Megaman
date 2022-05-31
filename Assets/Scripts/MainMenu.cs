using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    Button startBtn;

    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        startBtn = root.Q<Button>("start-btn");
        startBtn.clicked += StartGame;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
