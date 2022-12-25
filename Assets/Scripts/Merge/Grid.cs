using UnityEngine;


namespace Merge
{
    [System.Serializable]
    public class TileData
    {
        [System.Serializable]
        public struct rowData
        {
            public Transform[] row;
        }

        public rowData[] rows;
    }

    public class Grid : MonoBehaviour
    {
        [SerializeField] private TileData _grid;
        public TileData Cells { get => _grid; }
    }
}


