#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
namespace GlyphicsLibrary.Language
{
    /* legend
     * w - width
     * h - height
     * d - depth
     * v - value
     * r - red
     * g - green
     * b - blue
     * a - alpha
     * x - x or x1
     * y - y or y1
     * z - z or z1
     * X - x2
     * Y - y2
     * Z - z2
     * */

    //Enums here both help identify the glyphs.. and binds them to a number permanently so bytecode is reusable
    public enum GlyphId
    {
        GlyphNoid=0,
        GlyphNop     = 1,
        GlyphSize1D1 = 2,
        GlyphSize2D1 = 3,
        GlyphSize3D1 = 4,
        GlyphSize1D2 = 5,
        GlyphSize2D2 = 6,
        GlyphSize3D2 = 7,
        GlyphSize1D3 = 8,
        GlyphSize2D3 = 9,
        GlyphSize3D3 = 10,
        GlyphSize1D4 = 11,
        GlyphSize2D4 = 12,
        GlyphSize3D4 = 13,

        GlyphPenColorD1 = 14,
        GlyphPenColorD2 = 15,
        GlyphPenColorD3 = 16,
        GlyphPenColorD4 = 17,
        GlyphPenWidth   = 18,
        GlyphPenHeight  = 19,
        GlyphPenDepth   = 20,
        GlyphPenSize    = 21,
        GlyphPenHatch   = 22,

        GlyphPenShape   = 23,
        GlyphPenTex     = 24,
        GlyphPenAnim    = 25,
        GlyphPenPhysics = 26,

        GlyphScissor    = 27,
        GlyphClear      = 28,
        GlyphPlot       = 29,

        GlyphLine       = 30,
        GlyphRect       = 31,
        GlyphFillRect   = 32,

        GlyphMaskRect       = 33,
        GlyphMaskFillRect   = 34,

        GlyphTubeXy         = 35,  
        GlyphTubeXz         = 36,
        GlyphTubeYz         = 37, 

        GlyphStairs         = 38,
        GlyphExtrudeLine    = 39, 
        GlyphTriangle       = 40, 
        GlyphFillTriangle2D = 41,
        GlyphPolygon        = 42,

        GlyphArcXy          = 43,
        GlyphArcXz          = 44,
        GlyphArcYz          = 45,
        GlyphCircle3D       = 46,
        GlyphCircle2D       = 47,
        GlyphCircle2Dxy     = 48, 
        GlyphCircle2Dyz     = 49, 
        GlyphCircle2Dxz     = 50, 

        GlyphOval3D         = 51,
        GlyphDiamond2D      = 52,
        GlyphQuad2D         = 53,
        GlyphPyramid3D      = 54,

        GlyphFillCircle2D   = 55,
        GlyphFillCircle2Dxy = 56,
        GlyphFillCircle2Dyz = 57,
        GlyphFillCircle2Dxz = 58,
        GlyphFillCircle3D   = 59,

        GlyphExtrudeX       = 60,  
        GlyphExtrudeY       = 61,  
        GlyphExtrudeZ       = 62,  

        GlyphImgFlipX       = 63,
        GlyphImgFlipY       = 64,
        GlyphImgFlipZ       = 65,
        GlyphImgMirrorX     = 66,
        GlyphImgMirrorY     = 67,
        GlyphImgMirrorZ     = 68,

        GlyphImgPalettize   = 69,
        GlyphImgInvert      = 70,
        GlyphImgGrayscale   = 71,
        GlyphImgColorize    = 72,
        GlyphImgHueshift    = 73,
        GlyphImgSaturate    = 74,
        GlyphImgBrightness  = 75,
        GlyphImgRotX        = 76,
        GlyphImgRotY        = 77,
        GlyphImgRotZ        = 78,

        GlyphImgShadeX      = 79,
        GlyphImgShadeY      = 80,
        GlyphImgShadeZ      = 81,

        GlyphImgEdge        = 82, 
        GlyphImgEdgeX       = 83,
        GlyphImgEdgeY       = 84,
        GlyphImgEdgeZ       = 85,

        GlyphFilterBlur     = 86,
        GlyphFilterMotion   = 87,
        GlyphFilterEdgeHor  = 88,
        GlyphFilterEdgeVer  = 89,
        GlyphFilterEdgeAll  = 90,
        GlyphFilterSharpen  = 91,
        GlyphFilterEmboss   = 92,
        GlyphFilterSmooth   = 93,

        GlyphGenesis3D      = 94,
        GlyphSpawn          = 95,

        GlyphPal1D          = 96,
        GlyphPal2D          = 97,
        GlyphPal3D          = 98,
        GlyphPalFromVal1D   = 99,
        GlyphPalFromVal2D   = 100,
        GlyphPalFromVal3D   = 101,
        GlyphPalFromGrid1D  = 102,
        GlyphPalFromGrid2D  = 103,
        GlyphPalFromGrid3D  = 104,
        GlyphPalGen1D1      = 105,
        GlyphPalGen1D2      = 106,
        GlyphPalGen1D3      = 107,
        GlyphPalGen1D4      = 108,

        GlyphBlit1D         = 109,
        GlyphBlit2D         = 110,
        GlyphBlit3D         = 111,
        GlyphRectBlit1D     = 112,
        GlyphRectBlit2D     = 113,
        GlyphRectBlit3D     = 114,
        GlyphBlendBlit1D    = 115,
        GlyphBlendBlit2D    = 116,
        GlyphBlendBlit3D    = 117,

        GlyphArchPut        = 118,
        GlyphArchLine       = 119,
        GlyphArchRect       = 120,

        GlyphGenChaos       = 121,
        GlyphAlien          = 122,
        GlyphStar           = 123,
        GlyphText           = 124,
        GlyphWallCube       = 125,
        GlyphCornerSupports = 126,
        GlyphSupports       = 127,
        GlyphUpV            = 128,

        GlyphFillTriangle   = 129,
        GlyphShadows        = 130,
        GlyphMaze           = 131
    };

    internal static class Glyphs
    {
        private static readonly IGlyph[] GlyphDefs = 
            {
                new CGlyph(GlyphId.GlyphNop,            "Nop",                0,    0,  "", "No operation" ),
#region ByteGrid
                new CGlyph(GlyphId.GlyphSize1D1,        "Size1D1",            0,    1,  "w", "Create 1-byte 1D grid of <width>" ),
                new CGlyph(GlyphId.GlyphSize2D1,        "Size2D1",            0,    2,  "w h", "Create 1-byte 2D grid of <width> <height>" ),
                new CGlyph(GlyphId.GlyphSize3D1,        "Size3D1",            0,    3,  "w h d", "Create 1-byte 3D grid of <width> <height> <depth>" ),
                new CGlyph(GlyphId.GlyphSize1D2,        "Size1D2",            0,    1,  "w", "Create 2-byte 1D grid of <width>" ),
                new CGlyph(GlyphId.GlyphSize2D2,        "Size2D2",            0,    2,  "w h", "Create 2-byte 2D grid of <width> <height>" ),
                new CGlyph(GlyphId.GlyphSize3D2,        "Size3D2",            0,    3,  "w h d", "Create 2-byte 3D grid of <width> <height> <depth>" ),
                new CGlyph(GlyphId.GlyphSize1D3,        "Size1D3",            0,    1,  "w", "Create 3-byte 1D grid of <width>" ),
                new CGlyph(GlyphId.GlyphSize2D3,        "Size2D3",            0,    2,  "w h", "Create 3-byte 2D grid of <width> <height>" ),
                new CGlyph(GlyphId.GlyphSize3D3,        "Size3D3",            0,    3,  "w h d", "Create 3-byte 3D grid of <width> <height> <depth>" ),
                new CGlyph(GlyphId.GlyphSize1D4,        "Size1D4",            0,    1,  "w", "Create 4-byte 1D grid of <width>" ),
                new CGlyph(GlyphId.GlyphSize2D4,        "Size2D4",            0,    2,  "w h", "Create 4-byte 2D grid of <width> <height>" ),
                new CGlyph(GlyphId.GlyphSize3D4,        "Size3D4",            0,    3,  "w h d", "Create 4-byte 3D grid of <width> <height> <depth>" ),
                new CGlyph(GlyphId.GlyphPenColorD1,     "PenColorD1",         0,    1,  "v", "Set Pen rgba <v>"),
                new CGlyph(GlyphId.GlyphPenColorD2,     "PenColorD2",         0,    2,  "v v", "Set Pen rgba <v1> <v2>"),
                new CGlyph(GlyphId.GlyphPenColorD3,     "PenColorD3",         0,    3,  "r g b", "Set Pen rgba <r> <g> <b>"),
                new CGlyph(GlyphId.GlyphPenColorD4,     "PenColorD4",         0,    4,  "r g b a", "Set Pen rgba <r> <g> <b> <a>"),
                new CGlyph(GlyphId.GlyphPenWidth  ,     "PenWidth",           0,    1,  "w", "Set Pen Width <width>"),
                new CGlyph(GlyphId.GlyphPenHeight ,     "PenHeight",          0,    1,  "h", "Set Pen Height <height>"),
                new CGlyph(GlyphId.GlyphPenDepth  ,     "PenDepth",           0,    1,  "d", "Set Pen Depth <depth>"),
                new CGlyph(GlyphId.GlyphPenSize   ,     "PenSize",            0,    3,  "w h d", "Set Pen Size <width> <height> <depth>"),
                new CGlyph(GlyphId.GlyphPenHatch  ,     "PenHatch",           0,    3,  "# # #", "Set Pen hatching <ModulusX> <ModulusY> <ModulusZ>"),
                
                new CGlyph(GlyphId.GlyphPenShape  ,     "PenShape",           0,    1,  "#", ""),
                new CGlyph(GlyphId.GlyphPenTex    ,     "PenTex",             0,    1,  "#", ""),
                new CGlyph(GlyphId.GlyphPenAnim   ,     "PenAnim",            0,    1,  "#", "<animation value>"), 
                new CGlyph(GlyphId.GlyphPenPhysics,     "PenPhysics",         0,    0,  "", "<Affect-NoAffect> <Damage-NoDamage> <Radar-NoRadar> <Sticky>"), 

                new CGlyph(GlyphId.GlyphScissor ,       "Scissor",            0,    6,  "x y z X Y Z", "Set Grid Scissor <x1> <y1> <z1> <x2> <y2> <z2>"), 
                new CGlyph(GlyphId.GlyphClear   ,       "Clear",              0,    0,  "", "Clear entire grid" ),
                new CGlyph(GlyphId.GlyphPlot    ,       "Plot",               0,    3,  "x y z v", "Pen Plot at <x> <y> <z> value" ),

                new CGlyph(GlyphId.GlyphLine    ,       "Line",               0,    6,  "x y z X Y Z", "Draw Line <x1> <y1> <z1> <x2> <y2> <z2>" ), 
                new CGlyph(GlyphId.GlyphRect    ,       "Rect",               0,    6,  "x y z X Y Z", "Draw hollow 3D rectangle <x1> <y1> <z1> <x2> <y2> <z2>" ), 
                new CGlyph(GlyphId.GlyphFillRect,       "FillRect",           0,    6,  "x y z X Y Z", "Draw 3D rectangle <x1> <y1> <z1> <x2> <y2> <z2>" ), 
                
                new CGlyph(GlyphId.GlyphMaskRect      , "MaskRect",           0,    7,  "x y z X Y Z #", "Draw Hollow 3D rectangle <x1> <y1> <z1> <x2> <y2> <z2> <mask>" ), 
                new CGlyph(GlyphId.GlyphMaskFillRect  , "MaskFillRect",       0,    7,  "x y z X Y Z #", "Draw Filled 3D rectangle <x1> <y1> <z1> <x2> <y2> <z2> <mask>" ), 

                new CGlyph(GlyphId.GlyphTubeXy        , "TubeXY",             0,    6,  "x y z X Y Z", "Draw 3D tube <x1> <y1> <z1> <x2> <y2> <z2>" ), 
                new CGlyph(GlyphId.GlyphTubeXz        , "TubeXZ",             0,    6,  "x y z X Y Z", "Draw 3D tube <x1> <y1> <z1> <x2> <y2> <z2>" ), 
                new CGlyph(GlyphId.GlyphTubeYz        , "TubeYZ",             0,    6,  "x y z X Y Z", "Draw 3D tube <x1> <y1> <z1> <x2> <y2> <z2>" ), 

                new CGlyph(GlyphId.GlyphStairs        , "Stairs",             0,    9,  "x y z X Y Z w h d",   "Create stairs <x1> <y1> <z1> <x2> <y2> <z2> <PenWidth> <PenHeight> <PenDepth>" ),
                new CGlyph(GlyphId.GlyphExtrudeLine   , "ExtrudeLine",        0,    9,  "x y z X Y Z # r R #", "Create extrusion along path <x1> <y1> <z1> <x2> <y2> <z2> <slices> <radius1> <radius 2>" ),
                new CGlyph(GlyphId.GlyphTriangle      , "Triangle",           0,    9,  "x y z X Y Z # # #", "Draw Triangle <x1> <y1> <z1> <x2> <y2> <z2> <x3> <y3> <z3>"),
                new CGlyph(GlyphId.GlyphFillTriangle2D, "FillTriangle2D",     0,    6,  "x y X Y # #", "Draw Filled 2D Triangle <x1> <y1> <x2> <y2> <x3> <y3>"),
                new CGlyph(GlyphId.GlyphPolygon       , "Polygon",            0,    5,  "x y z s #", "Draw 2D Polygon <x> <y> <z> <radius> <sides>" ), 

                new CGlyph(GlyphId.GlyphArcXy         , "ArcXY",              0,    6,  "x y z s # #", "Draw 2D Arc <x> <y> <z> <radius> <startAngle> <sweepAngle>" ), 
                new CGlyph(GlyphId.GlyphArcXz         , "ArcXZ",              0,    6,  "x y z s # #", "Draw 2D Arc <x> <y> <z> <radius> <startAngle> <sweepAngle>" ), 
                new CGlyph(GlyphId.GlyphArcYz         , "ArcYZ",              0,    6,  "x y z s # #", "Draw 2D Arc <x> <y> <z> <radius> <startAngle> <sweepAngle>" ), 
                new CGlyph(GlyphId.GlyphCircle3D      , "Circle3D",           0,    4,  "x y z s", "Draw 2D Circle <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.GlyphCircle2D      , "Circle2D",           0,    4,  "x y z s", "Draw 2D Circle <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.GlyphCircle2Dxy    , "Circle2DXY",         0,    4,  "x y z s", "Draw 2D Circle on z-Axis <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.GlyphCircle2Dyz    , "Circle2DYZ",         0,    4,  "x y z s", "Draw 2D Circle on x-Axis <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.GlyphCircle2Dxz    , "Circle2DXZ",         0,    4,  "x y z s", "Draw 2D Circle on y-Axis <x> <y> <z> <radius>" ), 
                
                new CGlyph(GlyphId.GlyphOval3D        , "Oval3D",             0,    6,  "x y z w h d", "Draw Oval at <x> <y> <z> <w> <h> <d>" ), 
                new CGlyph(GlyphId.GlyphDiamond2D     , "Diamond2D",          0,    5,  "x y z w h", "Draw Diamond at <x1> <y1> <z1> <w> <h>"),
                new CGlyph(GlyphId.GlyphQuad2D        , "Quad2D",             0,    6,  "x y X Y Z h", "Draw Quad <x1> <y1> to <x2> <y2> on <z> with <h>"),
                new CGlyph(GlyphId.GlyphPyramid3D     , "Pyramid3D",          0,    6,  "x y z X Y Z", "Draw Pyramid <x1> <y1> <z1> to <x2> <y2> <z2>"),

                new CGlyph(GlyphId.GlyphFillCircle2D  , "FillCircle2D",       0,    4,  "x y z s", "Draw Filled 2D Circle <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.GlyphFillCircle2Dxy, "FillCircle2DXY",     0,    4,  "x y z s", "Draw Filled 2D Circle on z-axis <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.GlyphFillCircle2Dyz, "FillCircle2DYZ",     0,    4,  "x y z s", "Draw Filled 2D Circle on x-axis <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.GlyphFillCircle2Dxz, "FillCircle2DXZ",     0,    4,  "x y z s", "Draw Filled 2D Circle on y-axis <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.GlyphFillCircle3D  , "FillCircle3D",       0,    4,  "x y z s", "Draw Filled 2D Circle <x> <y> <z> <radius>" ), 

                new CGlyph(GlyphId.GlyphExtrudeX      , "ExtrudeX",           0,    8, "x y z X f s S #", "<x1> <y> <z> <x2> <Shape> <StartScale> <StopScale> <Skips>"),
                new CGlyph(GlyphId.GlyphExtrudeY      , "ExtrudeY",           0,    8, "x y z Y f s S #", "<x> <y1> <z> <y2> <Shape> <StartScale> <StopScale> <Skips>"),
                new CGlyph(GlyphId.GlyphExtrudeZ      , "ExtrudeZ",           0,    8, "x y z Z f s S #", "<x> <y> <z1> <z2> <Shape> <StartScale> <StopScale> <Skips>"),

#endregion
#region Imaging
                new CGlyph(GlyphId.GlyphImgFlipX     ,  "ImgFlipX",           0,    0,  "", "Flip on X axis"), 
                new CGlyph(GlyphId.GlyphImgFlipY     ,  "ImgFlipY",           0,    0,  "", "Flip on Y axis"), 
                new CGlyph(GlyphId.GlyphImgFlipZ     ,  "ImgFlipZ",           0,    0,  "", "Flip on Z axis"), 
                new CGlyph(GlyphId.GlyphImgMirrorX   ,  "ImgMirrorX",         0,    0,  "", "Mirror on X axis"), 
                new CGlyph(GlyphId.GlyphImgMirrorY   ,  "ImgMirrorY",         0,    0,  "", "Mirror on Y axis"), 
                new CGlyph(GlyphId.GlyphImgMirrorZ   ,  "ImgMirrorZ",         0,    0,  "", "Mirror on Z axis"), 

                new CGlyph(GlyphId.GlyphImgPalettize ,  "ImgPalettize",       0,    1,  "#", "Use a palette to remap a grid <palette>"), 
                new CGlyph(GlyphId.GlyphImgInvert    ,  "ImgInvert",          0,    0,  "", "Invert the image"), 
                new CGlyph(GlyphId.GlyphImgGrayscale ,  "ImgGrayscale",       0,    0,  "", "Grayscale the image"), 
                new CGlyph(GlyphId.GlyphImgColorize  ,  "ImgColorize",        0,    2,  "# #", "Colorize the image <hue> <saturation>"), 
                new CGlyph(GlyphId.GlyphImgHueshift  ,  "ImgHueshift",        0,    1,  "#", "Hue shift the image <hue shift>"), 
                new CGlyph(GlyphId.GlyphImgSaturate  ,  "ImgSaturate",        0,    1,  "%", "Adjust saturation <adjust %>"), 
                new CGlyph(GlyphId.GlyphImgBrightness,  "ImgBrightness",      0,    1,  "%", "Adjust brightness <adjust %>"), 
                new CGlyph(GlyphId.GlyphImgRotX      ,  "ImgRotX",            0,    0,  "", "Rotate image on X-axis 90 degrees" ), 
                new CGlyph(GlyphId.GlyphImgRotY      ,  "ImgRotY",            0,    0,  "", "Rotate image on Y-axis 90 degrees" ), 
                new CGlyph(GlyphId.GlyphImgRotZ      ,  "ImgRotZ",            0,    0,  "", "Rotate image on Z-axis 90 degrees" ), 

                new CGlyph(GlyphId.GlyphImgShadeX    ,  "ImgShadeX",          0,    6,  "r g b a R G B A", "ShadeX <r1> <g1> <b1> <r2> <g2> <b2>"),
                new CGlyph(GlyphId.GlyphImgShadeY    ,  "ImgShadeY",          0,    6,  "r g b a R G B A", "ShadeY <r1> <g1> <b1> <r2> <g2> <b2>"),
                new CGlyph(GlyphId.GlyphImgShadeZ    ,  "ImgShadeZ",          0,    6,  "r g b a R G B A", "ShadeZ <r1> <g1> <b1> <r2> <g2> <b2>"),

                new CGlyph(GlyphId.GlyphImgEdge      ,  "ImgEdge",            0,    4,  "r g b a", "Set edge color <r> <g> <b> <a>"),
                new CGlyph(GlyphId.GlyphImgEdgeX     ,  "ImgEdgeX",           0,    4,  "r g b a", "Set edge x color <r> <g> <b> <a>"),
                new CGlyph(GlyphId.GlyphImgEdgeY     ,  "ImgEdgeY",           0,    4,  "r g b a", "Set edge y color <r> <g> <b> <a>"),
                new CGlyph(GlyphId.GlyphImgEdgeZ     ,  "ImgEdgeZ",           0,    4,  "r g b a", "Set edge z color <r> <g> <b> <a>"),
                
                new CGlyph(GlyphId.GlyphFilterBlur   ,  "FilterBlur",         0,    0,  "", "Blur filter"), 
                new CGlyph(GlyphId.GlyphFilterMotion ,  "FilterMotion",       0,    0,  "", "Motion filter"), 
                new CGlyph(GlyphId.GlyphFilterEdgeHor,  "FilterEdgeHor",      0,    0,  "", "Edge horizontal filter"), 
                new CGlyph(GlyphId.GlyphFilterEdgeVer,  "FilterEdgeVer",      0,    0,  "", "Edge vertical filter"), 
                new CGlyph(GlyphId.GlyphFilterEdgeAll,  "FilterEdgeAll",      0,    0,  "", "Edge filteattenuate, r"), 
                new CGlyph(GlyphId.GlyphFilterSharpen,  "FilterSharpen",      0,    0,  "", "Sharpen filter"), 
                new CGlyph(GlyphId.GlyphFilterEmboss ,  "FilterEmboss",       0,    0,  "", "Emboss filter"), 
                new CGlyph(GlyphId.GlyphFilterSmooth ,  "FilterSmooth",       0,    0,  "", "Smooth filter"), 
#endregion
#region Simulation
                new CGlyph(GlyphId.GlyphGenesis3D,  "Genesis3D",         0,    1,  "s","New grid of rgba, xyz <size and floor;person in center;CorNew;DekNew;ScnNew;EleNew"),
                new CGlyph(GlyphId.GlyphSpawn,      "Spawn",             0,    3,  "x y z","Spawn <x> <y> <z>"),
#endregion
#region Archtypes
                new CGlyph(GlyphId.GlyphPal1D        , "Pal1D",              0,    1,  "w", "Create 1D Palette <width>" ),
                new CGlyph(GlyphId.GlyphPal2D        , "Pal2D",              0,    2,  "w h", "Create 2D Palette <width> <height>" ),
                new CGlyph(GlyphId.GlyphPal3D        , "Pal3D",              0,    3,  "w h d", "Create 3D Palette <width> <height> <depth>" ),
                new CGlyph(GlyphId.GlyphPalFromVal1D , "PalFromVal1D",       1,    1,  "", "Create 1D Palette <width> from values <#> ..." ),
                new CGlyph(GlyphId.GlyphPalFromVal2D , "PalFromVal2D",       2,    2,  "", "Create 2D Palette <width> <height> from values <#> ..." ),
                new CGlyph(GlyphId.GlyphPalFromVal3D , "PalFromVal3D",       3,    3,  "", "Create 3D Palette <width> <height> <depth> from values <#> ..." ),
                new CGlyph(GlyphId.GlyphPalFromGrid1D, "PalFromGrid1D",      0,    2,  "x X", "Create new palette from grizd <x1> <x2>"), 
                new CGlyph(GlyphId.GlyphPalFromGrid2D, "PalFromGrid2D",      0,    4,  "x y X Y", "Create new palette from grid <x1> <y1> <x2> <y2>"), 
                new CGlyph(GlyphId.GlyphPalFromGrid3D, "PalFromGrid3D",      0,    6,  "x y z X Y Z", "Create new palette from grid <x1> <y1> <z1> <x2> <y2> <z2>"), 
                new CGlyph(GlyphId.GlyphPalGen1D1    , "PalGen1D1",          0,    3,  "# #", "Generate gradient palette <palette> <vs> <vd>"),
                new CGlyph(GlyphId.GlyphPalGen1D2    , "PalGen1D2",          0,    5,  "# # # #", "Generate gradient palette <palette> <vs1> <vs2> <vd1> <vd2>"),
                new CGlyph(GlyphId.GlyphPalGen1D3    , "PalGen1D3",          0,    7,  "# # # # # #", "Generate gradient palette <palette> <vs1> <vs2> <vs3> <vd1> <vd2> <vd3>"),
                new CGlyph(GlyphId.GlyphPalGen1D4    , "PalGen1D4",          0,    9,  "# # # # # # # #", "Generate gradient palette <palette> <vs1> <vs2> <vs3> <vs4> <vd1> <vd2> <vd3> <vd4>"),

                new CGlyph(GlyphId.GlyphBlit1D       , "Blit1D",             0,    2,  "# x", "Draw Palette at <palette> <x>" ),
                new CGlyph(GlyphId.GlyphBlit2D       , "Blit2D",             0,    3,  "# x y", "Draw Palette at <palette> <x> <y>" ),
                new CGlyph(GlyphId.GlyphBlit3D       , "Blit3D",             0,    4,  "# x y z", "Draw Palette at <palette> <x> <y> <z>" ),
                new CGlyph(GlyphId.GlyphRectBlit1D   , "RectBlit1D",         0,    3,  "# x X", "Draw Palette at <palette> <x1> <x2>" ),
                new CGlyph(GlyphId.GlyphRectBlit2D   , "RectBlit2D",         0,    5,  "# x y x Y", "Draw Palette at <palette> <x1> <y1> <x2> <y2>"),
                new CGlyph(GlyphId.GlyphRectBlit3D   , "RectBlit3D",         0,    7,  "# x y z X Y Z", "Draw Palette at <palette> <x1> <y1> <z1> <x2> <y2> <z2>"),
                new CGlyph(GlyphId.GlyphBlendBlit1D  , "BlendBlit1D",        0,    4,  "# x X #", "Blend Palette at <palette> <x1> <x2> <blend>" ),
                new CGlyph(GlyphId.GlyphBlendBlit2D  , "BlendBlit2D",        0,    6,  "# x y X Y #", "Blend Palette at <palette> <x1> <y1> <x2> <y2> <blend>"),
                new CGlyph(GlyphId.GlyphBlendBlit3D  , "BlendBlit3D",        0,    8,  "# x y z X Y Z #", "Blend Palette at <palette> <x1> <y1> <z1> <x2> <y2> <z2> <blend>"),

                new CGlyph(GlyphId.GlyphArchPut      , "ArchPut",            0,    4,  "x y z #",             "Draw (at) <x> <y> <z> <archtype> "),
                new CGlyph(GlyphId.GlyphArchLine     , "ArchLine",           0,    9,  "x y z X Y Z # # #",   "Draw <x> <y> <z> <x2> <y2> <z2> <archtype> <rotation> <spacing>"),
                new CGlyph(GlyphId.GlyphArchRect     , "ArchRect",           0,    9,  "x y z X Y Z # # #",   "Draw <x> <y> <z> <x2> <y2> <z2> <archtype> <rotation> <spacing>"),
#endregion
#region FX
                new CGlyph(GlyphId.GlyphGenChaos      , "GenChaos",           0,    4,  "# x y z", "Generate chaos <0..11> <x> <y> <z>"), 
                new CGlyph(GlyphId.GlyphAlien         , "Alien",              0,    4,  "x y z s",   "Draw <x> <y> <z> <scale>"),
                new CGlyph(GlyphId.GlyphStar          , "Star",               0,    4,  "x y z s",   "Draw <x> <y> <z> <scale>"),
                new CGlyph(GlyphId.GlyphText          , "Text",               0,    4,  "x y z #", "Draw 2D Letter <x> <y> <z> <letter>" ), 
                new CGlyph(GlyphId.GlyphWallCube      , "WallCube",           0,    1,  "f", "Put Walls on Cube <bitmask>" ), 
                new CGlyph(GlyphId.GlyphCornerSupports, "CornerSupports",     0,    6,  "x y z X Y Z",   "Fill (cellular automata) <x> <y> <z> <x2> <y2> <z2>"),
                new CGlyph(GlyphId.GlyphSupports      , "Supports",           0,    0,  "", "Put Supports in world" ),
                new CGlyph(GlyphId.GlyphUpV           , "UpV",                0,    5, "# # r g b a", "UpV <reps> <search r> <search g> <search b> <search a>"),
#endregion
#region RecentlyAdded
                new CGlyph(GlyphId.GlyphFillTriangle  , "FillTriangle",       0,    9,  "x y z X Y Z # # #", "Draw Filled Triangle <x1> <y1> <z1> <x2> <y2> <z2> <x3> <y3> <z3>"),
                new CGlyph(GlyphId.GlyphShadows       , "Shadows",            0,    0,  "", "Simulate shadows" ),
                new CGlyph(GlyphId.GlyphMaze          , "Maze",               0,    5,  "x y z X Z", "Generate maze from xyz to XyZ" )
#endregion
            };

        //GetToken the glyph id of a name
        public static int GetId(string str)
        {
            foreach (IGlyph g in GlyphDefs)
                if (g.Name == str) return g.Id;
            return -1;
        }

        //Get glyph at id, null if out of range
        public static IGlyph GetGlyph(int id)
        {
            if (id < 0 || id >= GlyphDefs.Length) return null;
            return GlyphDefs[id];
        }

        //Return an array of all the glyphs
        public static IGlyph[] GetGlyphDefs()
        {
            return GlyphDefs;
        }
    }
}

