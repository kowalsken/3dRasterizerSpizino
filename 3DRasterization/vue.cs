using _3DRasterization.Geometry;
using _3DRasterization.Lights;
using ObjLoader.Loader.Loaders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3DRasterization
{
    public partial class vue : Form
    {
        public vue()
        {
            InitializeComponent();
        }

        private void vue_Load(object sender, EventArgs e)
        {
            textBox10.Text = "7test.obj";
        }

        private List<Vector3> getTextBox()
        {
            List<Vector3> vectors = new List<Vector3>();

            float x, y, z;
            try {  x = float.Parse(textBox1.Text); }
            catch { x = .2f; textBox1.Text = "0.2"; }
            try { y = float.Parse(textBox2.Text); }
            catch { y = 0f; textBox2.Text = "0"; }
            try {  z = float.Parse(textBox3.Text); }
            catch {  z = 2f; textBox3.Text = "2"; }
            vectors.Add(new Vector3(x, y, z));
            try { x = float.Parse(textBox4.Text); }
            catch { x = 0f; textBox4.Text = "0"; }
            try { y = float.Parse(textBox5.Text); }
            catch { y = 0f; textBox5.Text = "0"; }
            try { z = float.Parse(textBox6.Text); }
            catch { z = 0f; textBox6.Text = "0"; }
            vectors.Add(new Vector3(x, y, z));
            try { x = float.Parse(textBox7.Text); }
            catch { x = 0f; textBox7.Text = "0"; }
            try { y = float.Parse(textBox8.Text); }
            catch { y = 1f; textBox8.Text = "1"; }
            try { z = float.Parse(textBox9.Text); }
            catch { z = 0f; textBox9.Text = "0"; }
            vectors.Add(new Vector3(x, y, z));
            try { x = float.Parse(textBox11.Text); }
            catch { x = .5f; textBox11.Text = "0.5"; }
            try { y = float.Parse(textBox12.Text); }
            catch { y = .5f; textBox12.Text = "0.5"; }
            try { z = float.Parse(textBox13.Text); }
            catch { z = .5f; textBox13.Text = "0.5"; }
            vectors.Add(new Vector3(x, y, z));
            return vectors;
        }
        List<Vector4> getVector4()
        {
            List<Vector4> vectors = new List<Vector4>();

            float x, y, z, w;
            try { x = float.Parse(textBox14.Text); }
            catch { x = 100f; textBox14.Text = "100"; }
            try { y = float.Parse(textBox15.Text); }
            catch { y = 1f; textBox15.Text = "1"; }
            try { z = float.Parse(textBox16.Text); }
            catch { z = 1f; textBox16.Text = "1"; }
            try { w = float.Parse(textBox17.Text); }
            catch { w = 10000f; textBox17.Text = "10000"; }
            vectors.Add(new Vector4(x, y, z, w));
            try { x = float.Parse(textBox18.Text);  }
            catch { x = 0f; textBox18.Text = "0"; }
            try { y = float.Parse(textBox19.Text); }
            catch { y = 0f; textBox19.Text = "0"; }
            try { z = float.Parse(textBox20.Text); }
            catch { z = 0f; textBox20.Text = "0"; }
            try { w = float.Parse(textBox21.Text); }
            catch { w = 0f; textBox21.Text = "0"; }
            vectors.Add(new Vector4(x, y, z, w));
            return vectors;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label8.Text = "Start render...";
            this.Update();
            #region Create collections for rasterize
            Buffer buff = new Buffer(1500, 1500, Color.Aqua);
            List<Mesh> meshList = new List<Mesh>();
            List<Light> lightList = new List<Light>();
            Rasterization render = new Rasterization(buff);
            VertexProcessor vertex = new VertexProcessor(getTextBox(), getVector4());
            this.Update();
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            Vector3 p0 = new Vector3(2f, 5f, 5f);
            DirectionalLight light = new DirectionalLight(p0);
            #endregion


            #region Dodanie obiektów do sceny
            ObjMesh obj = new ObjMesh();

            //Console.WriteLine("OBJ File Name:");
            string nameObj = textBox10.Text; // Console.ReadLine();
            var fileStream = new FileStream(nameObj, FileMode.Open);
            var result = objLoader.Load(fileStream);
            fileStream.Close();
            //ladowanie pozycji wierzcholkow
            foreach (ObjLoader.Loader.Data.VertexData.Vertex l in result.Vertices)
            {
                Vector3 pos = new Vector3(l.X, l.Y, l.Z);
                Vertex newVert = new Vertex(pos);
                obj.vertexes.Add(newVert);
            }

            List<int> indexes = new List<int>();

            foreach (ObjLoader.Loader.Data.Elements.Group n in result.Groups)
            {
                foreach (ObjLoader.Loader.Data.Elements.Face f in n.Faces)
                {
                    for (int i = 0; i < f._vertices.Count; i++)
                    {
                        indexes.Add(f._vertices[i].VertexIndex - 1);
                    }
                }
            }

            lightList.Add(light);
            obj.indexes = indexes;
            meshList.Add(obj);
            Scene scene = new Scene(meshList, lightList, render, vertex);

            #endregion
            pictureBox1.Image = buff.colorBuffer;
            label8.Text = "Render was end.";

            buff.SaveImage();

            // Console.WriteLine("Rasterization Complete.");
            // Console.ReadKey();
        }
    }
}
