#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlyphicsLibrary.ByteGrid
{
    internal class FileDaeWrite
    {
        internal class ColladaScene
        {
            public int Id;
            public List<int> Geometry = new List<int>();
        }
        internal class ColladaGeometry
        {
            public int Id;
            public int Materialid;
            public int PositionId;
            public int NormalId;
            public int PositionId2;
            public int NormalId2;
            public double X1, Y1, Z1;
            public double X2, Y2, Z2;
            public int VertexId;
        }

        internal class ColladaMaterial
        {
            public int Id;
            public int Url;
        }
        internal class ColladaEffect
        {
            public int Id;
            public ulong Rgba;
            public byte R, G, B, A;
        }

        internal class Collada
        {
            public static string Header = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\" ?>\r\n<COLLADA xmlns=\"http://www.collada.org/2005/11/COLLADASchema\" version=\"1.4.1\">";
            public static string Assets = "    <asset>\r\n" +
                                            "        <contributor>\r\n" +
                                            "            <authoring_tool>Glyphics</authoring_tool>\r\n" +
                                            "        </contributor>\r\n" +
                                            "        <created>1867-04-01T20:13:48Z</created>\r\n" +
                                            "        <modified>1867-04-01T20:13:48Z</modified>\r\n" +
                                            "        <unit meter=\"0.0254\" name=\"inch\"/>\r\n" +
                                            "        <up_axis>Z_UP</up_axis>\r\n" +
                                            "    </asset>";
            public static string Footer = "</COLLADA>\r\n";

            public List<ColladaEffect> Effects = new List<ColladaEffect>();
            public List<ColladaMaterial> Materials = new List<ColladaMaterial>();

            public List<ColladaScene> Scenes = new List<ColladaScene>();
            public List<ColladaGeometry> Geometries = new List<ColladaGeometry>();

            public int FindMaterial(ulong rgba)
            {
                foreach (ColladaMaterial material in Materials)
                {
                    ColladaMaterial materialReadOnly = material;
                    if (Effects.Any(effect => effect.Id == materialReadOnly.Url && effect.Rgba == rgba))
                    {
                        return material.Id;
                    }
                }
                return 0;
            }

            public string StrScenes()
            {
                string str = "    <scene>\r\n";
                foreach (ColladaScene cscene in Scenes)
                {
                    str += "        <instance_visual_scene url=\"#ID" + (cscene.Id) + "\" />\r\n";
                }

                str += "    </scene>";
                return str;
            }
            public void WriteMesh(string filename)
            {
                string str =
                    Header + "\r\n" +
                    Assets + "\r\n" +
                    StrVisuals() + "\r\n" +
                    StrGeometries() + "\r\n" +
                    StrMaterials() + "\r\n" +
                    StrEffects() + "\r\n" +
                    StrScenes() + "\r\n" +
                    Footer + "\r\n";
                //Console.WriteLine(str);

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
                {
                    file.WriteLine(str);
                }
            }

            private ColladaGeometry FindGeometry(int id)
            {
                return Geometries.FirstOrDefault(geo => geo.Id == id);
            }

            public string StrVisuals()
            {
                string str = "    <library_visual_scenes>\r\n";


                foreach (ColladaScene scene in Scenes)
                {
                    str +=
                         "        <visual_scene id=\"ID" + scene.Id + "\">\r\n" +
                         "        <node name=\"IScene" + scene.Id + "\">\r\n";

                    foreach (int i in scene.Geometry)
                    {
                        ColladaGeometry geo = FindGeometry(i);
                        str += "        <instance_geometry url=\"#ID" + geo.Id + "\">\r\n" +
                                "        <bind_material>\r\n" +
                                "            <technique_common>\r\n" +
                                "            <instance_material symbol=\"Material" + geo.Materialid + "\" target=\"#ID" + geo.Materialid + "\">\r\n" +
                                "            <bind_vertex_input semantic=\"UVSET0\" input_semantic=\"TEXCOORD\" input_set=\"0\" />\r\n" +
                                "            </instance_material>\r\n" +
                                "            </technique_common>\r\n" +
                                "        </bind_material>\r\n" +
                                "        </instance_geometry>\r\n";
                    }

                    str +=
                         "        </node>\r\n" +
                         "        </visual_scene>\r\n";
                }

                str += "    </library_visual_scenes>";
                return str;
            }

            public string StrEffects()
            {
                string str = "    <library_effects>\r\n";


                foreach (ColladaEffect effect in Effects)
                {
                    float fr = effect.R / 255.0f;
                    float fg = effect.G / 255.0f;
                    float fb = effect.B / 255.0f;
                    float fa = effect.A / 255.0f;

                    str += "        <effect id=\"ID" + effect.Id + "\">\r\n" +
                            "            <profile_COMMON>\r\n" +
                            "                <technique sid=\"COMMON\">\r\n" +
                            "                    <lambert>\r\n" +
                            "                        <diffuse>\r\n" +
                            "                            <color>" + fr + " " + fg + " " + fb + " " + fa + "</color>\r\n" +
                            "                        </diffuse>\r\n" +
                            "                    </lambert>\r\n" +
                            "                </technique>\r\n" +
                            "            </profile_COMMON>\r\n" +
                            "        </effect>\r\n";
                }

                str += "    </library_effects>";
                return str;
            }

            public string StrMaterials()
            {
                string str = "    <library_materials>\r\n";

                foreach (ColladaMaterial material in Materials)
                {
                    str +=
                        "        <material id=\"ID" + material.Id + "\" name=\"material" + material.Id + "\">\r\n" +
                        "            <instance_effect url=\"#ID" + material.Url + "\" />\r\n" +
                        "        </material>\r\n";
                }

                str += "    </library_materials>";
                return str;
            }

            public string StrGeometries()
            {
                string str = "    <library_geometries>\r\n";
                foreach (ColladaGeometry geom in Geometries)
                {
                    str += StrGeometry(geom);
                }
                str += "    </library_geometries>";
                return str;
            }

            public static string StrGeometry(ColladaGeometry geom)
            {
                double minx = geom.X1;
                double miny = geom.Z1;
                double minz = geom.Y1;
                double maxx = geom.X2;
                double maxy = geom.Z2;
                double maxz = geom.Y2;

                double[] data =
                {
                    maxx,maxy,minz,
                    minx,miny,minz,
                    minx,maxy,minz,
                    maxx,miny,minz,
                    minx,maxy,maxz,
                    maxx,maxy,minz,
                    minx,maxy,minz,
                    maxx,maxy,maxz,
                    maxx,maxy,minz,
                    maxx,miny,maxz,
                    maxx,miny,minz,
                    maxx,maxy,maxz,
                    maxx,miny,maxz,
                    minx,miny,minz,
                    maxx,miny,minz,
                    minx,miny,maxz,
                    minx,maxy,maxz,
                    minx,miny,minz,
                    minx,miny,maxz,
                    minx,maxy,minz,
                    maxx,miny,maxz,
                    minx,maxy,maxz,
                    minx,miny,maxz,
                    maxx,maxy,maxz
                };


                string array = "";
                const string points = "0 1 2 1 0 3 4 5 6 5 4 7 8 9 10 9 8 11 12 13 14 13 12 15 16 17 18 17 16 19 20 21 22 21 20 23";
                const string normals = "0 0 -1 0 0 -1 0 0 -1 0 0 -1 1 0 0 1 0 0 1 0 0 1 0 0 -0 -1 -0 -0 -1 -0 -0 -1 -0 -0 -1 -0 -1 0 0 -1 0 0 -1 0 0 -1 0 0 -0 1 0 -0 1 0 -0 1 0 -0 1 0 0 0 1 0 0 1 0 0 1 0 0 1";

                for (int i = 0; i < data.Length; i += 3)
                {
                    double x = data[i + 0];
                    double y = data[i + 1];
                    double z = data[i + 2];
                    array += x + " " + y + " " + z + " ";
                }

                string str =
    "        <geometry id=\"ID" + geom.Id + "\">\r\n" +
    "            <mesh>\r\n" +
    "                <source id=\"ID" + geom.PositionId + "\">\r\n" +
    "                    <float_array id=\"ID" + geom.PositionId2 + "\" count=\"72\">" + array + "</float_array>\r\n" +
    "                    <technique_common>\r\n" +
    "                        <accessor count=\"24\" source=\"#ID" + geom.PositionId + "\" stride=\"3\">\r\n" +
    "                            <param name=\"X\" type=\"float\" />\r\n" +
    "                            <param name=\"Y\" type=\"float\" />\r\n" +
    "                            <param name=\"Z\" type=\"float\" />\r\n" +
    "                        </accessor>\r\n" +
    "                    </technique_common>\r\n" +
    "                </source>\r\n" +
    "                <source id=\"ID" + geom.NormalId + "\">\r\n" +
    "                    <float_array id=\"ID" + geom.NormalId2 + "\" count=\"72\">" + normals + "</float_array>\r\n" +
    "                    <technique_common>\r\n" +
    "                        <accessor count=\"24\" source=\"#ID" + geom.NormalId2 + "\" stride=\"3\">\r\n" +
    "                            <param name=\"X\" type=\"float\" />\r\n" +
    "                            <param name=\"Y\" type=\"float\" />\r\n" +
    "                            <param name=\"Z\" type=\"float\" />\r\n" +
    "                        </accessor>\r\n" +
    "                    </technique_common>\r\n" +
    "                </source>\r\n" +
    "                <vertices id=\"ID" + geom.VertexId + "\">\r\n" +
    "                    <input semantic=\"POSITION\" source=\"#ID" + geom.PositionId + "\" />\r\n" +
    "                    <input semantic=\"NORMAL\" source=\"#ID" + geom.NormalId + "\" />\r\n" +
    "                </vertices>\r\n" +
    "                <triangles count=\"12\" material=\"Material" + geom.Materialid + "\">\r\n" +
    "                    <input offset=\"0\" semantic=\"VERTEX\" source=\"#ID" + geom.VertexId + "\" />\r\n" +
    "                    <p>" + points + "</p>\r\n" +
    "                </triangles>\r\n" +
    "            </mesh>\r\n" +
    "        </geometry>\r\n";

                return str;
            }
        }

        public class ExportRectsCollada
        {
            static int _nodeid = 2;

            public static List<ulong> FindRgbaPalette(IRectList rects)
            {
                var palette = new List<ulong>();
                foreach (IRect rect in rects) if (palette.Contains(rect.Properties.Rgba) == false) palette.Add(rect.Properties.Rgba);
                return palette;
            }

            public static void WriteMesh(string filename, IRectList rects)
            {        
                List<ulong> palette = FindRgbaPalette(rects);

                _nodeid = 1;

                ColladaScene scene = new ColladaScene();
                scene.Id = _nodeid++;

                Collada collada = new Collada();
                collada.Scenes.Add(scene);

                foreach (ulong rgba in palette)
                {
                    ColladaEffect effect = new ColladaEffect();
                    Atomics.Converter.Ulong2Rgba(rgba, out effect.R, out effect.G, out effect.B, out effect.A);

                    ColladaMaterial material = new ColladaMaterial();
                    material.Id = _nodeid++;
                    effect.Id = _nodeid++;
                    material.Url = effect.Id;
                    effect.Rgba = rgba;

                    collada.Materials.Add(material);
                    collada.Effects.Add(effect);
                }

                foreach (IRect rect in rects)
                {
                    ColladaGeometry geom = new ColladaGeometry();
                    geom.Id = _nodeid++;
                    geom.PositionId = _nodeid++;
                    geom.NormalId = _nodeid++;
                    geom.PositionId2 = _nodeid++;
                    geom.NormalId2 = _nodeid++;
                    geom.VertexId = _nodeid++;

                    int material = collada.FindMaterial(rect.Properties.Rgba);
                    geom.Materialid = material;
                    geom.X1 = rect.Pt1.X;
                    geom.Y1 = rect.Pt1.Y;
                    geom.Z1 = rect.Pt1.Z;
                    geom.X2 = rect.Pt2.X;
                    geom.Y2 = rect.Pt2.Y;
                    geom.Z2 = rect.Pt2.Z;

                    collada.Geometries.Add(geom);
                    scene.Geometry.Add(geom.Id);
                }


                collada.WriteMesh(filename);
            }
        }
    }
}


