using System;
using System.Windows.Forms;
using UnityEngine;

namespace WorldObjects
{
    public class Cycle
    {
        public float duration;
        public float delay;
        public float amount;

        private Timer timer1;

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = (int)(duration * 1000); // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Debug.Log("seconds");
        }
    }
}
