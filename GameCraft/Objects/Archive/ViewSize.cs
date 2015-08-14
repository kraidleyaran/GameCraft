using System;

namespace GameCraft.Archive
{
    [Serializable]
    public struct ViewSize
    {
        private int _width;
        private int _height;
        public ViewSize(int width, int height)
        {
            _width = width;
            _height = height;
        }
        public int Height { get { return _height; } set { _height = value; } }
        public int Width { get { return _width; } set { _width = value; } }
    }
}