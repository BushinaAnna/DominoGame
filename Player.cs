using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Domino
{
    [Serializable]
    public class Player
    {
        public List<Tile> Tiles { get; set; } = new List<Tile>();      

    }
}
