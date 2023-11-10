using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pause_Manual : MonoBehaviour
{
    public Slider bgm_slider;
    public Slider sfx_slider;
    public Button note_controller_manual;
    public float sound_diminish;
    public GameObject manual_note;
    public bool note_on = false;

    public int current_ui = 0;
    GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.buttonBPress)
        {
            switch (current_ui)
            {
                case 0:
                    note_controller_manual.animator.SetTrigger("Normal");
                    if(bgm_slider.value < 0)
                    {
                        bgm_slider.value = 0;
                    }
                    else if(bgm_slider.value > 1)
                    {
                        bgm_slider.value = 1;
                    }
                    else
                    {
                        bgm_slider.value += sound_diminish;
                    }
                    sound_diminish = 0;
                    break;
                case 1:
                    note_controller_manual.animator.SetTrigger("Normal");
                    if (sfx_slider.value < 0)
                    {
                        sfx_slider.value = 0;
                    }
                    else if (sfx_slider.value > 1)
                    {
                        sfx_slider.value = 1;
                    }
                    else
                    {
                        sfx_slider.value += sound_diminish;
                    }
                    sound_diminish = 0;
                    break;
                case 2:
                    note_controller_manual.animator.SetTrigger("Highlighted");
                    break;
                default:
                    break;
            }
        }
        
    }
    public void Button_Select(InputAction.CallbackContext input)
    {
        if (GameManager.buttonBPress && input.started && !note_on)
        {
            if(input.control.name == "up")
            {
                if(current_ui == 0)
                {
                    current_ui = 2;
                }
                else
                {
                    current_ui -= 1;
                }
            }
            else if(input.control.name == "down")
            {
                if (current_ui == 2)
                {
                    current_ui = 0;
                }
                else
                {
                    current_ui += 1;
                }
            }
        }
    }
    public void Button_Press(InputAction.CallbackContext input)
    {
        if(GameManager.buttonBPress && input.started && current_ui == 2)
        {
            if (note_on)
            {
                note_on = false;
                manual_note.SetActive(note_on);
            }
            else
            {
                note_on = true;
                manual_note.SetActive(note_on);
            }
            
        }
    }
    public void Sound_Slider(InputAction.CallbackContext input)
    {
        if (GameManager.buttonBPress && input.started && !note_on)
        {
            if (input.control.name == "left")
            {
                sound_diminish = -0.2f;
            }
            else if (input.control.name == "right")
            {
                sound_diminish = 0.2f;
            }
        }
    }
    private void OnDisable()
    {
        current_ui = 0;
    }
}
