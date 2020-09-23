using ObjLoader.Loader.Loaders;
using _3DRasterization.Geometry;
using _3DRasterization.Lights;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace _3DRasterization
{
    class Program
    {
        static void Main(string[] args)
        {
            vue settingsForm = new vue();

            // Show the settings form
            if (settingsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK){}
        }
    }
}
