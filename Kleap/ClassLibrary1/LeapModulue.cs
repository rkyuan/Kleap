using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Kleap
{
    public class LeapModulue : PartModule
    {
        SampleListener listener = new SampleListener();
        Leap.Controller controller = new Leap.Controller();

        public override void OnAwake()
        {
            controller.AddListener(listener);
            base.OnAwake();
        }

        public override void OnStart(StartState state)
        {
            print("mod loaded!");
            base.OnStart(state);
        }

        float yaw = 0;
        float pitch = 0;
        float roll = 0;

        //[KSPEvent(active=true, guiActive = true, guiActiveEditor = true, guiName = "dispVals")]
        //public void dispVals()
        //{
        //    Events["dispVals"].guiName = "stuff";
        //}

        public override void OnUpdate()
        {
            pitch = listener.getp();
            roll = listener.getr();
            yaw = listener.gety();
            vessel.OnFlyByWire += MyAutopilotFunction;
            base.OnUpdate();
        }
        void MyAutopilotFunction(FlightCtrlState s) 
        { 
            s.yaw = yaw/3;
            s.pitch = pitch/3;
            s.roll = roll/3;
        }
    }

    
}
