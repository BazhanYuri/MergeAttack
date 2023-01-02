using UnityEngine;


namespace Merge
{
    [System.Serializable]
    public class TileData
    {
        [System.Serializable]
        public struct rowData
        {
            public Cell[] row;
        }

        public rowData[] rows;
    }

    
        
}


