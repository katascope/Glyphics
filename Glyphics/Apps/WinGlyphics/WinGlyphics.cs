using System;
using System.Drawing;
using System.Windows.Forms;
using GlyphicsLibrary;

namespace WinGlyphics
{
    public partial class WinGlyphics : Form
    {
        GlyphicsLibrary.Language.DownSolver _hc;
        private CBitmap _bitmap;

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

            _hc = new GlyphicsLibrary.Language.DownSolver("..\\..\\..\\..\\..\\Glyph Cores\\default.gly");

            foreach (ICode code in _hc.Codes)
            {
                comboBoxGly.Items.Add(code.ToString());
            }
            comboBoxGly.Text = _hc.Codes.GetCode(0).ToString();
        }

        private void UpdateDisplay()
        {
            _bitmap = new CBitmap();

            if (_hc.GridOblique != null)
                _bitmap.GridToBitmap(_hc.GridOblique);

            textBoxTokens.Text = (_hc.Tokens == null) ? null : _hc.Tokens.ToString();
            textBoxSerializedRects.Text = (_hc.SerializedRects == null) ? null : _hc.SerializedRects.ToString();
            textBoxSerializedRectsLimit255.Text = (_hc.SerializedRectsLimit255 == null) ? null : _hc.SerializedRectsLimit255.ToString();
            textBoxRects.Text = (_hc.Rects == null) ? null : _hc.Rects.ToString();
            textBoxQuads.Text = (_hc.Quads == null) ? null : _hc.Quads.ToString();
            textBoxTriangles.Text = (_hc.Triangles == null) ? null : _hc.Triangles.ToString();
            textBoxIndexedTriangles.Text = (_hc.Triangles == null) ? null : GlyphicsApi.TrianglesToWebGl(_hc.Triangles,"Name");
            Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (_bitmap.GetBitmap() != null)
                e.Graphics.DrawImage(_bitmap.GetBitmap(), 0, 0);
        }

        //Clipboard.SetImage(Bitmap.GetBitmap());
        private void copySerializedRectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_hc.SerializedRects != null)
                Clipboard.SetText(_hc.SerializedRects.ToString());
        }

        private void copySerializedRectsLimit255ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_hc.SerializedRectsLimit255 != null)
                Clipboard.SetText(_hc.SerializedRectsLimit255.ToString());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxGly_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strCode = comboBoxGly.Text;
            ICode code = GlyphicsApi.CreateCode(strCode);
            _hc = new GlyphicsLibrary.Language.DownSolver(code);
            UpdateDisplay();
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            string strCode = textBoxMain.Text.Replace(";;",";");
            ICode code = GlyphicsApi.CreateCode(strCode);
            _hc = new GlyphicsLibrary.Language.DownSolver(code);
            UpdateDisplay();
        }

        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string resultName = FileIo.GetOpenFilename("Open a model File", "Stereolithography file (*.STL)|*.stl|OBJ file (*.OBJ)|*.obj|All files (*.*)|*.*");
            if (resultName != null)
            {
                ITriangles triangles = GlyphicsApi.StlToTriangles(resultName);
                IGrid grid = GlyphicsApi.CreateGrid(16, 16, 16, 4);
                GlyphicsApi.Renderer.RenderTrianglesToGrid(triangles, grid);
                _hc = new GlyphicsLibrary.Language.DownSolver(grid);
                UpdateDisplay();
            }
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string resultName = FileIo.GetSaveAsFilename("Save a PNG Image File", "PNG Image|*.png|All files (*.*)|*.*");
            if (resultName != null)
            {
                _hc = new GlyphicsLibrary.Language.DownSolver(resultName);
                UpdateDisplay();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _hc = new GlyphicsLibrary.Language.DownSolver();
            UpdateDisplay();
        }

        private void copyWebGLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_hc.Triangles != null)
            {
                string webglTriangles = GlyphicsApi.TrianglesToWebGl(_hc.Triangles, "Tester");
                Clipboard.SetText(webglTriangles);
            }
        }
    }
    class CBitmap
    {
        Bitmap _bitmap;

        public Bitmap GetBitmap() { return _bitmap; }

        public void Save(string fileName)
        {
            _bitmap.Save(fileName);
        }

        public void Plot(IntPtr data, int stride, int x, int y, int z, byte r, byte g, byte b)
        {
            unsafe
            {
                var ptr = (byte*)data;
                int offset = y * stride + x * 3;
                ptr[offset + 0] = b;
                ptr[offset + 1] = g;
                ptr[offset + 2] = r;
            }
        }

        public void GridToBitmap(IGrid grid)
        {
            if (grid != null)
            {
                _bitmap = new Bitmap(grid.SizeX, grid.SizeY);
                System.Drawing.Imaging.BitmapData data = _bitmap.LockBits(new Rectangle(0, 0, grid.SizeX, grid.SizeY),
                              System.Drawing.Imaging.ImageLockMode.ReadWrite,
                              System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                for (int z = 0; z < grid.SizeZ; z++)
                    for (int y = 0; y < grid.SizeY; y++)
                        for (int x = 0; x < grid.SizeX; x++)
                        {
                            ulong v = grid.GetRgba(x, y, z);
                            byte r, g, b, a;
                            GlyphicsApi.Ulong2Rgba(v, out r, out g, out b, out a);
                            Plot(data.Scan0, data.Stride, x, y, z, r, g, b);
                        }
                _bitmap.UnlockBits(data);
            }
        }
    }
    public class FileIo
    {
        public static string GetSaveAsFilename(string title, string filter)
        {
            var saveFileDialog1 = new SaveFileDialog();
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
            var openFileDialog1 = new OpenFileDialog();

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
