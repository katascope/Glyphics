using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlyphicsLibrary;

namespace WinGlyphics
{
    public partial class WinGlyphics : Form
    {
        GlyphicsLibrary.Language.DownSolver hc;
        private CBitmap bitmap = null;

        public WinGlyphics()
        {
            InitializeComponent();
        }

        private void WinGlyphics_Load(object sender, EventArgs e)
        {
            const string codeString = 
@"PrintableNexus,Size3D4 64 64 64;Spawn 25 5 25;PenColorD4 31 127 255 255;
PenShape 1;WallCube 1;
PenColorD4 255 255 255 255;
PenSize 1 2 1;
Rect 0 0 0 31 0 31;
Rect 0 0 32 31 0 63;
Rect 32 0 0 63 0 31;
Rect 32 0 32 63 0 63;
Rect 16 0 16 48 0 48;
PenSize 1 1 1;PenColorD4 31 127 255 255;
FillRect 17 0 17 47 0 47;FillRect 16 1 49 48 16 63;
PenColorD4 0 0 0 0;
FillRect 17 1 49 47 15 63;
Rect 0 1 0 63 63 63;
ImgEdgeX 255 255 255 255;
ImgEdgeY 255 255 255 255;
ImgEdgeZ 255 255 255 255;
PenShape 2;PenColorD3 255 255 255;FillRect 26 17 51 36 28 62;
PenColorD3 127 255 127;FillRect 2 1 2 13 12 13; 
PenColorD3 255 127 127;FillRect 2 1 18 13 12 29;
PenColorD3 127 127 255;FillRect 2 1 34 13 12 45;
PenColorD3 255 255 127;FillRect 2 1 50 13 12 61;
PenColorD3 255 127 255;FillRect 18 1 2 29 12 13;
ImgMirrorX
";
            textBoxMain.Text = codeString;

            hc = new GlyphicsLibrary.Language.DownSolver("..\\..\\..\\..\\..\\Glyph Cores\\default.gly");

            foreach (ICode code in hc.codes)
            {
                comboBoxGly.Items.Add(code.ToString());
            }
            comboBoxGly.Text = hc.codes.GetCode(0).ToString();
        }

        private void UpdateDisplay()
        {
            bitmap = new CBitmap();
            bitmap.GridToBitmap(hc.gridOblique);

            textBoxTokens.Text = (hc.tokens == null) ? null : hc.tokens.ToString();
            textBoxSerializedRects.Text = (hc.serializedRects == null) ? null : hc.serializedRects.ToString();
            textBoxSerializedRectsLimit255.Text = (hc.serializedRectsLimit255 == null) ? null : hc.serializedRectsLimit255.ToString();
            textBoxRects.Text = (hc.rects == null) ? null : hc.rects.ToString();
            textBoxQuads.Text = (hc.quads == null) ? null : hc.quads.ToString();
            textBoxTriangles.Text = (hc.triangles == null) ? null : hc.triangles.ToString();
            this.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (bitmap.GetBitmap() != null)
                e.Graphics.DrawImage(bitmap.GetBitmap(), 0, 0);
        }

        //Clipboard.SetImage(Bitmap.GetBitmap());
        private void copySerializedRectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hc.serializedRects != null)
                Clipboard.SetText(hc.serializedRects.ToString());
        }

        private void copySerializedRectsLimit255ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hc.serializedRectsLimit255 != null)
                Clipboard.SetText(hc.serializedRectsLimit255.ToString());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveObliquePNGToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxGly_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strCode = comboBoxGly.Text;
            ICode code = GlyphicsApi.CreateCode(strCode);
            hc = new GlyphicsLibrary.Language.DownSolver(code);
            UpdateDisplay();
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            string strCode = textBoxMain.Text.Replace(";;",";");
            ICode code = GlyphicsApi.CreateCode(strCode);
            hc = new GlyphicsLibrary.Language.DownSolver(code);
            UpdateDisplay();
        }

        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string resultName = FileIO.GetOpenFilename("Open a model File", "Stereolithography file (*.STL)|*.stl|OBJ file (*.OBJ)|*.obj|All files (*.*)|*.*");
            if (resultName != null)
            {
                ITriangles triangles = GlyphicsApi.StlToTriangles(resultName);
                IGrid grid = GlyphicsApi.CreateGrid(16, 16, 16, 4);
                GlyphicsApi.Renderer.RenderTrianglesToGrid(triangles, grid);
                hc = new GlyphicsLibrary.Language.DownSolver(grid);
                UpdateDisplay();
            }
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string resultName = FileIO.GetSaveAsFilename("Save a PNG Image File", "PNG Image|*.png|All files (*.*)|*.*");
            if (resultName != null)
            {
                hc = new GlyphicsLibrary.Language.DownSolver(resultName);
                UpdateDisplay();
            }
        }
    }
    class CBitmap
    {
        Bitmap bitmap;

        public Bitmap GetBitmap() { return bitmap; }

        public void Save(string fileName)
        {
            bitmap.Save(fileName);
        }

        public void Plot(IntPtr data, int stride, int x, int y, int z, byte r, byte g, byte b)
        {
            unsafe
            {
                byte* ptr = (byte*)data;
                int offset = y * stride + x * 3;
                ptr[offset + 0] = b;
                ptr[offset + 1] = g;
                ptr[offset + 2] = r;
            }
        }

        public void GridToBitmap(IGrid grid)
        {
            bitmap = new Bitmap(grid.SizeX, grid.SizeY);
            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, grid.SizeX, grid.SizeY),
                              System.Drawing.Imaging.ImageLockMode.ReadWrite,
                              System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            if (grid != null)
            {
                for (int z = 0; z < grid.SizeZ; z++)
                    for (int y = 0; y < grid.SizeY; y++)
                        for (int x = 0; x < grid.SizeX; x++)
                        {
                            ulong v = grid.GetRgba(x, y, z);
                            byte r, g, b, a;
                            GlyphicsApi.Ulong2Rgba(v, out r, out g, out b, out a);
                            Plot(data.Scan0, data.Stride, x, y, z, r, g, b);
                        }
            }

            bitmap.UnlockBits(data);
        }
    }
    public class FileIO
    {
        public static string GetSaveAsFilename(string title, string filter)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = filter;
            saveFileDialog1.Title = title;
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                return saveFileDialog1.FileName;
            }
            return null;
        }
        public static string GetOpenFilename(string title, string filter)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = ".";
            openFileDialog1.Title = title;
            openFileDialog1.Filter = filter;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            return null;
        }
    }

}
