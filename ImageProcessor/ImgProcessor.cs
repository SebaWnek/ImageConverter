using System;

namespace ImageProcessor
{
    public class ImgProcessor
    {
        private byte[,,] baseImage;

        public ImgProcessor()
        {
        }

        public bool LoadImage(byte[,,] data)
        {
            baseImage = data;
            return true;
        }
    }
}
