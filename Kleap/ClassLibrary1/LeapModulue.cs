using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Leap;

namespace Kleap
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class LeapPlugin : MonoBehaviour
    {
        float yaw = 0;
        float pitch = 0;
        float roll = 0;
        bool controlling = false;
        SampleListener listener;
        Leap.Controller controller;

        void Awake()
        {
            listener = new SampleListener();
            controller = new Leap.Controller();
            controller.AddListener(listener);
            print("set up the leap");
        }
        void Start()
        {
            print("started");
            
        }

        void OnDestroy()
        {
            controller.RemoveListener(listener);
            controller.Dispose();
        }

        void Update()
        {
            if (listener.active && !controlling)
            {
                FlightGlobals.ActiveVessel.OnFlyByWire += MyAutopilotFunction;
                controlling = true;
                print("leap enabled");
            }
            if (!listener.active && controlling)
            {
                FlightGlobals.ActiveVessel.OnFlyByWire -= MyAutopilotFunction;
                controlling = false;
                print("leap disabled");
            }
        }
        
       

        //[KSPEvent(active=true, guiActive = true, guiActiveEditor = true, guiName = "dispVals")]
        //public void dispVals()
        //{
        //    Events["dispVals"].guiName = "stuff";
        //}

        //void Update()
        //{
        //    pitch = listener.getp();
        //    roll = listener.getr();
        //    yaw = listener.gety();
        //    FlightGlobals.ActiveVessel.OnFlyByWire += MyAutopilotFunction;
        //    base.OnUpdate();
        //}

        void MyAutopilotFunction(FlightCtrlState s) 
        { 
            s.yaw = listener.yaw;
            s.pitch = listener.pitch*(float)1.8;
            s.roll = listener.roll*(float)0.6;
            s.mainThrottle = listener.throttle;
            s.wheelThrottle = listener.throttle;
            s.wheelSteer = listener.yaw*-1.6f;
        }
    }

    
}
