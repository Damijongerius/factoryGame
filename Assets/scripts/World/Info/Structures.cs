using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WorldObjects
{
    public abstract class Structures
    {
        private GameObject _structure;
        public GameObject structure { get => _structure; set => Tests(value) ; }

        public float upkeepcost;

        public int efficiency;

        public bool LandStruct;

        public void OnCalculate() { }

        public void ResetCycles() { }

        public void GetCycles() { }

        public void AddPath(List<Tile> list) { }



        //make function overload of world.onDelete with Tile you want to Delete
        public void Tests(GameObject _value)
        {
            if (_value == null)
            {
                //delete Tile
                return;
            }
            _structure = _value;
            return;
        }
    }
}
