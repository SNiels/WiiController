﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WiiLib;

namespace WiiService
{
    public partial class WiiService : ServiceBase
    {
        private WiiController controller;
        public WiiService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            #if DEBUG
                        System.Diagnostics.Debugger.Launch();
            #endif
                        SetCursorPos(180, 180);
            controller = WiiController.GetOldWiiRemote();
            controller.InfraredChanged += controller_InfraredChanged;
            controller.ButtonPressed += controller_ButtonPressed;
        }

        void controller_ButtonPressed(object sender, WiiController.Button e)
        {
            throw new NotImplementedException();
        }

        void controller_InfraredChanged(object sender, List<System.Windows.Point> e)
        {
            Point point = (Point)controller.GetAveragePoint();
            SetCursorPos((int)point.X * 1280, (int)point.Y * 1920);
        }

        protected override void OnStop()
        {
            controller.Dispose();
        }

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);
    }
}
