using System;

namespace DialogGraph
{
    [Serializable()]
    public class CharacteredNode : Node
    {
        public string CharacterId;

        [NonSerialized()]
        public Character CharacterInstance;

    }
}

