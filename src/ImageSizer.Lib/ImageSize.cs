namespace ImageSizer.Lib
{
    /// <summary>
    /// Size Property. 
    ///     Width.
    ///     Height.
    /// </summary>
    public struct ImageSize
    {
        public ImageSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}