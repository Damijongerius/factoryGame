using System;
using UnityEngine;
using System.Threading;

namespace WorldObjects
{
    public class Cycle
    {
        public float duration;
        public float delay;
        public float amount;

        private SaveFile sf = SaveFile.GetInstance();

        public void InitTimer()
        {
            Debug.Log("startT");
            var timer = new Timer(AfterDelay, null, 0, Mathf.RoundToInt(1f / Time.deltaTime));
        }

        private void AfterDelay(object sender)
        {
            sf.profile.Statistics.Money += 1;
        }
    }
}
