﻿namespace GameCraft
{
    public struct PlayCount
    {
        public int Count;
        public bool Loop;
        public PlayCount(int count, bool loop)
        {
            Count = count;
            Loop = loop;
        }
    }
}