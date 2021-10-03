using System;

namespace DialogGraph
{
    [Serializable()]
    public class SwitchByItem : Node
    {
        public string ItemId;
        public int MaximalThreshold;
        public int MinimalThreshold;
    }
}

