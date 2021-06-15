using System;

namespace DialogGraph
{
    [Serializable()]
    public class CharacterAppearance : Node
    {
        public int CharacterId;
        public CharacterPosition Position;
    }
}
