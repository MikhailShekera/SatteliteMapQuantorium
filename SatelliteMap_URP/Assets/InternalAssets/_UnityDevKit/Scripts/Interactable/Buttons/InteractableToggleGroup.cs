﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.Interactables.Buttons
{
    public class InteractableToggleGroup : MonoBehaviour
    {
        [SerializeField] private ButtonConnector[] buttons;

        [SerializeField] private UnityEvent onToggle;
        
        private void Start()
        {
            if (buttons.Length < 2)
            {
                throw new IndexOutOfRangeException("There's less then two buttons in toggle group");
            }
            
            SubscribeAll();
        }

        private void SubscribeAll()
        {
            for (var i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];
                button.AddListenerOnButtonDown(onToggle.Invoke);
                for (var k = 0; k < buttons.Length; k++)
                {
                    if (i == k)
                    {
                        continue;
                    }
                    
                    button.AddListenerOnButtonDown(buttons[k].OnButtonUpTo);
                }
            }
        }
    }
}