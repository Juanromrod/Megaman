using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class Win : MonoBehaviour
{
    Button menuBtn;

    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        menuBtn = root.Q<Button>("menu-btn");
        menuBtn.clicked += Menu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
