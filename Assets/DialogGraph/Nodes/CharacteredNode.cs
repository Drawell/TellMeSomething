using System;

namespace DialogGraph
{
    [Serializable()]
    public class CharacteredNode : Node
    {
        public int CharacterId;

        [NonSerialized()]
        public Character CharacterInstance;

    }
}

