using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class WorldLayer
    {
        public WorldLayer(int _layer)
        {
            Layer = _layer;
            worldLayer = Layer switch
            {
                -2 => EWorldLayer.water,
                -1 => EWorldLayer.electricity,
                _ => EWorldLayer.ground,
            };
        }

        private int Layer { get; }

        private readonly EWorldLayer worldLayer;

        private HashSet<Cell> cells = new();

        public int GetLayer()
        {
            return Layer;
        }

        public Cell GetCell(Vector2 pos) {
            foreach (Cell cell in cells) 
                if(cell.GetPos() == pos) return cell;
           
            return null;
        }

        public void AddCell(Cell cell) 
        {
            cells.Add(cell);
        }

        public void RemoveCell(Cell cell)
        {
            cells.Remove(cell);
        }

        public EWorldLayer GetWorldLayer()
        {
            return worldLayer;
        }
    }
}
