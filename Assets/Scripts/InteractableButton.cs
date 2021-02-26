using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractableButton : MonoBehaviour
{
    public Button myButton;
    public Text myText;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        counter += 1;
        if (counter > 5)
        {
            Text txt = myText.GetComponent<Text>();
            txt.text = "hello";
        }
        Debug.Log("Button pressed " + counter + " times");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
