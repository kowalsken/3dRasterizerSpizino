using System.Drawing;
using System.Drawing.Imaging;

namespace _3DRasterization
{
    //Буфер - размер, цвет, глубина.
    public class Buffer
    {
        public Bitmap colorBuffer;
        public float[,] depthBuffer;

        //Создание нового образа с указанными размера и цветом фона
        public Buffer(int size_X, int size_Y, Color col)
        {
            colorBuffer = new Bitmap(size_X, size_Y);
            FillColor(col);
            depthBuffer = new float[size_X, size_Y];
            FillDepth();
        }

        //Зарполнить фон
        private void FillColor(Color col)
        {
            for (int i = 0; i < colorBuffer.Width; i++)
                for (int j = 0; j < colorBuffer.Height; j++)
                {
                    colorBuffer.SetPixel(i, j, col);
                }
        }

        //Заполнить буфер глубины
        private void FillDepth()
        {
            for (int i = 0; i < depthBuffer.GetLength(0); i++)
                for (int j = 0; j < depthBuffer.GetLength(1); j++)
                {
                    depthBuffer[i, j] = 1;
                }
        }

        //Сохранить файл
        public void SaveImage()
        {
            colorBuffer.Save("finalRender.png", ImageFormat.Png);
        }
    }
}
