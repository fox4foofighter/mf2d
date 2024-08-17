using SQLite4Unity3d;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class InputManager {
        public static InputManager _;
        public List<KeyCode> CurrentInputKeys { get; private set; }

        public InputManager() {
            _ = this;
        }

        public void Update() {
            UpdateCurrentInputKeys();
        }

        private void UpdateCurrentInputKeys()
        {
            List<KeyCode> keys = new List<KeyCode>();
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    Debug.Log("Pressed key: " + kcode);
                    keys.Add(kcode);
                }
            }
            CurrentInputKeys = keys;
        }


        public static bool IsKeyPressed(KeyCode key)
        {
            return _.CurrentInputKeys.Contains(key);
        }

        public static List<KeyCode> GetCurrentInputKeys()
        {
            return _.CurrentInputKeys;
        }
    }
}
