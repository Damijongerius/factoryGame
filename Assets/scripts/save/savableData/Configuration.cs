using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace configuration
{
    public class Configuration
    {
        //camera
        public float Fov;
        public KeyCode CenterCam;

        //movement
        public KeyCode Up;
        public KeyCode Down;
        public KeyCode Left;
        public KeyCode Right;

        public KeyCode Speed;

        //move rotations
        public KeyCode RLeft;
        public KeyCode RRight;

        //shortcuts modes
        public KeyCode Inspect;
        public KeyCode Sell;
        public KeyCode place;
        public KeyCode Upgrade;
    }

    class StandardConfig 
    {
        readonly float  Fov = 90f;
        readonly KeyCode CenterCam = KeyCode.V;

        //movement
        readonly KeyCode Up = KeyCode.W;
        readonly KeyCode Down = KeyCode.S;
        readonly KeyCode Left = KeyCode.A;
        readonly KeyCode Right = KeyCode.D;

        readonly KeyCode Speed = KeyCode.LeftShift;

        //move rotations
        readonly KeyCode RLeft = KeyCode.Q;
        readonly KeyCode RRight = KeyCode.E;

        //shortcuts modes
        readonly KeyCode Inspect = KeyCode.X;
        readonly KeyCode Sell = KeyCode.C;
        readonly KeyCode place = KeyCode.B;
        readonly KeyCode Upgrade = KeyCode.F;
    }

}
