using SQLite4Unity3d;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using Dto;

namespace Managers
{
    public class ControlManager
    {
        public static ControlManager _;
        public List<ControlEvent> RegisteredEvents { get; private set; }
        public List<ControlEvent> RunningEvents { get; private set; }

        public ControlManager()
        {
            _ = this;
        }

        public void Update()
        {

            RefreshRunningEvents();

            if (IsBlocked())
            {
                return;
            }

            // Check each registered event. If it is not running, start it
            foreach(var key in InputManager.GetCurrentInputKeys()) {
                Debug.Log("Pressed key Control: " + key);
                // コンフィグで設定されたキーとイベント名のマップを比較して、イベントを実行する
                // 現在有効なレイヤーでのみ実行されるイベントを実行する
                // TODO レイヤーマネージャを実装したら考える！
            }
        }

        public void AddEvent(ControlEvent controlEvent)
        {
            // Add new event to the list. If list has the same name event, skip it
            if (RegisteredEvents.Exists(e => e.Name == controlEvent.Name))
            {
                return;
            }

            RegisteredEvents.Add(controlEvent);
        }

        private void RefreshRunningEvents()
        {
            RunningEvents = new List<ControlEvent>();

            // Check each running event. If it is completed, remove it from the list
            foreach (var controlEvent in RunningEvents)
            {
                if (controlEvent.IsCompleted)
                {
                    RunningEvents.Remove(controlEvent);
                }
            }
        }

        private bool IsBlocked()
        {
            // Check each running event. If it is uncompleted and exclusive, return true
            foreach (var controlEvent in RunningEvents)
            {
                if (!controlEvent.IsCompleted && controlEvent.IsExclusive)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
