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
        //Prima Glyphs, the essential language "PrimaGlyphics"
        //Rules:
        // RGBA grids, always 3-dimensional
        // A pen, rect, and fill rect for drawing
        // No other operations, no "nop" operations.
        PrimaNop  = 0,//Nothingness
        PrimaSize = 1,// Size XYZ, assume RGBA
        PrimaRgba = 2,// Pen Color, assume RGBA
        PrimaRect = 3,// FillRect, with pen

        Reserved1= 4,
        Reserved2= 5,
        Reserved3= 6,
        Reserved4= 7,
        Reserved5= 8,
        Reserved6= 9,
        Reserved7 = 10,
        Reserved8 = 11,
        Reserved9 = 12,
        Reserved10 = 13,

        PenColorD1 = 14,
        PenColorD2 = 15,
        PenColorD3 = 16,
        PenColorD4 = 17,
        PenWidth   = 18,
        PenHeight  = 19,
        PenDepth   = 20,
        PenSize    = 21,
        PenHatch   = 22,

        PenShape   = 23,
        PenTex     = 24,
        PenAnim    = 25,
        PenPhysics = 26,

        Scissor    = 27,
        Clear      = 28,
        Plot       = 29,

        Line       = 30,
        Rect       = 31,
        FillRect   = 32,

        MaskRect       = 33,
        MaskFillRect   = 34,

        TubeXy         = 35,  
        TubeXz         = 36,
        TubeYz         = 37, 

        Stairs         = 38,
        ExtrudeLine    = 39, 
        Triangle       = 40, 
        FillTriangle2D = 41,
        Polygon        = 42,

        ArcXy          = 43,
        ArcXz          = 44,
        ArcYz          = 45,
        Circle3D       = 46,
        Circle2D       = 47,
        Circle2Dxy     = 48, 
        Circle2Dyz     = 49, 
        Circle2Dxz     = 50, 

        Oval3D         = 51,
        Diamond2D      = 52,
        Quad2D         = 53,
        Pyramid3D      = 54,

        FillCircle2D   = 55,
        FillCircle2Dxy = 56,
        FillCircle2Dyz = 57,
        FillCircle2Dxz = 58,
        FillCircle3D   = 59,

        ExtrudeX       = 60,  
        ExtrudeY       = 61,  
        ExtrudeZ       = 62,  

        ImgFlipX       = 63,
        ImgFlipY       = 64,
        ImgFlipZ       = 65,
        ImgMirrorX     = 66,
        ImgMirrorY     = 67,
        ImgMirrorZ     = 68,

        ImgPalettize   = 69,
        ImgInvert      = 70,
        ImgGrayscale   = 71,
        ImgColorize    = 72,
        ImgHueshift    = 73,
        ImgSaturate    = 74,
        ImgBrightness  = 75,
        ImgRotX        = 76,
        ImgRotY        = 77,
        ImgRotZ        = 78,

        ImgShadeX      = 79,
        ImgShadeY      = 80,
        ImgShadeZ      = 81,

        ImgEdge        = 82, 
        ImgEdgeX       = 83,
        ImgEdgeY       = 84,
        ImgEdgeZ       = 85,

        FilterBlur     = 86,
        FilterMotion   = 87,
        FilterEdgeHor  = 88,
        FilterEdgeVer  = 89,
        FilterEdgeAll  = 90,
        FilterSharpen  = 91,
        FilterEmboss   = 92,
        FilterSmooth   = 93,

        Genesis3D      = 94,
        Spawn          = 95,

        Pal1D          = 96,
        Pal2D          = 97,
        Pal3D          = 98,
        PalFromVal1D   = 99,
        PalFromVal2D   = 100,
        PalFromVal3D   = 101,
        PalFromGrid1D  = 102,
        PalFromGrid2D  = 103,
        PalFromGrid3D  = 104,
        PalGen1D1      = 105,
        PalGen1D2      = 106,
        PalGen1D3      = 107,
        PalGen1D4      = 108,

        Blit1D         = 109,
        Blit2D         = 110,
        Blit3D         = 111,
        RectBlit1D     = 112,
        RectBlit2D     = 113,
        RectBlit3D     = 114,
        BlendBlit1D    = 115,
        BlendBlit2D    = 116,
        BlendBlit3D    = 117,

        ArchPut        = 118,
        ArchLine       = 119,
        ArchRect       = 120,

        GenChaos       = 121,
        Alien          = 122,
        Star           = 123,
        Text           = 124,
        WallCube       = 125,
        CornerSupports = 126,
        Supports       = 127,
        UpV            = 128,

        FillTriangle   = 129,
        Shadows        = 130,
        Maze           = 131,

        Size1D1        = 132,
        Size2D1        = 133,
        Size3D1        = 134,
        Size1D2        = 135,
        Size2D2        = 136,
        Size3D2        = 137,
        Size1D3        = 138,
        Size2D3        = 139,
        Size3D3        = 140,
        Size1D4        = 141,
        Size2D4        = 142,
        Size3D4        = 143

    };

    internal static class Glyphs
    {
        private static readonly IGlyph[] GlyphDefs = 
            {
#region Prima
                new CGlyph(GlyphId.PrimaNop,         "Nop",                  0,    0,  "", "No operation" ),
                new CGlyph(GlyphId.PrimaSize,        "PrimaSize",            0,    3,  "x y z", "PrimaSize" ),
                new CGlyph(GlyphId.PrimaRgba,        "PrimaRGBA",            0,    4,  "r g b a", "PrimaRGBA" ),
                new CGlyph(GlyphId.PrimaRect,        "PrimaRect",            0,    6,  "x y z X Y Z", "Prima Fill Rect" ),
#endregion Prima
#region Reserved
                new CGlyph(GlyphId.Reserved1,        "Reserved1",            0,    0,  "", "Reserved1" ),
                new CGlyph(GlyphId.Reserved2,        "Reserved2",            0,    0,  "", "Reserved2" ),
                new CGlyph(GlyphId.Reserved3,        "Reserved3",            0,    0,  "", "Reserved3" ),
                new CGlyph(GlyphId.Reserved4,        "Reserved4",            0,    0,  "", "Reserved4" ),
                new CGlyph(GlyphId.Reserved5,        "Reserved5",            0,    0,  "", "Reserved5" ),
                new CGlyph(GlyphId.Reserved6,        "Reserved6",            0,    0,  "", "Reserved6" ),
                new CGlyph(GlyphId.Reserved7,        "Reserved7",            0,    0,  "", "Reserved7" ),
                new CGlyph(GlyphId.Reserved8,        "Reserved8",            0,    0,  "", "Reserved8" ),
                new CGlyph(GlyphId.Reserved9,        "Reserved9",            0,    0,  "", "Reserved9" ),
                new CGlyph(GlyphId.Reserved10,       "Reserved10",           0,    0,  "", "Reserved10" ),
#endregion Reserved
#region ByteGrid
                new CGlyph(GlyphId.Size1D1,        "Size1D1",            0,    1,  "w", "Create 1-byte 1D grid of <width>" ),
                new CGlyph(GlyphId.Size2D1,        "Size2D1",            0,    2,  "w h", "Create 1-byte 2D grid of <width> <height>" ),
                new CGlyph(GlyphId.Size3D1,        "Size3D1",            0,    3,  "w h d", "Create 1-byte 3D grid of <width> <height> <depth>" ),
                new CGlyph(GlyphId.Size1D2,        "Size1D2",            0,    1,  "w", "Create 2-byte 1D grid of <width>" ),
                new CGlyph(GlyphId.Size2D2,        "Size2D2",            0,    2,  "w h", "Create 2-byte 2D grid of <width> <height>" ),
                new CGlyph(GlyphId.Size3D2,        "Size3D2",            0,    3,  "w h d", "Create 2-byte 3D grid of <width> <height> <depth>" ),
                new CGlyph(GlyphId.Size1D3,        "Size1D3",            0,    1,  "w", "Create 3-byte 1D grid of <width>" ),
                new CGlyph(GlyphId.Size2D3,        "Size2D3",            0,    2,  "w h", "Create 3-byte 2D grid of <width> <height>" ),
                new CGlyph(GlyphId.Size3D3,        "Size3D3",            0,    3,  "w h d", "Create 3-byte 3D grid of <width> <height> <depth>" ),
                new CGlyph(GlyphId.Size1D4,        "Size1D4",            0,    1,  "w", "Create 4-byte 1D grid of <width>" ),
                new CGlyph(GlyphId.Size2D4,        "Size2D4",            0,    2,  "w h", "Create 4-byte 2D grid of <width> <height>" ),
                new CGlyph(GlyphId.Size3D4,        "Size3D4",            0,    3,  "w h d", "Create 4-byte 3D grid of <width> <height> <depth>" ),
                new CGlyph(GlyphId.PenColorD1,     "PenColorD1",         0,    1,  "v", "Set Pen rgba <v>"),
                new CGlyph(GlyphId.PenColorD2,     "PenColorD2",         0,    2,  "v v", "Set Pen rgba <v1> <v2>"),
                new CGlyph(GlyphId.PenColorD3,     "PenColorD3",         0,    3,  "r g b", "Set Pen rgba <r> <g> <b>"),
                new CGlyph(GlyphId.PenColorD4,     "PenColorD4",         0,    4,  "r g b a", "Set Pen rgba <r> <g> <b> <a>"),
                new CGlyph(GlyphId.PenWidth  ,     "PenWidth",           0,    1,  "w", "Set Pen Width <width>"),
                new CGlyph(GlyphId.PenHeight ,     "PenHeight",          0,    1,  "h", "Set Pen Height <height>"),
                new CGlyph(GlyphId.PenDepth  ,     "PenDepth",           0,    1,  "d", "Set Pen Depth <depth>"),
                new CGlyph(GlyphId.PenSize   ,     "PenSize",            0,    3,  "w h d", "Set Pen Size <width> <height> <depth>"),
                new CGlyph(GlyphId.PenHatch  ,     "PenHatch",           0,    3,  "# # #", "Set Pen hatching <ModulusX> <ModulusY> <ModulusZ>"),
                
                new CGlyph(GlyphId.PenShape  ,     "PenShape",           0,    1,  "#", ""),
                new CGlyph(GlyphId.PenTex    ,     "PenTex",             0,    1,  "#", ""),
                new CGlyph(GlyphId.PenAnim   ,     "PenAnim",            0,    1,  "#", "<animation value>"), 
                new CGlyph(GlyphId.PenPhysics,     "PenPhysics",         0,    0,  "", "<Affect-NoAffect> <Damage-NoDamage> <Radar-NoRadar> <Sticky>"), 

                new CGlyph(GlyphId.Scissor ,       "Scissor",            0,    6,  "x y z X Y Z", "Set Grid Scissor <x1> <y1> <z1> <x2> <y2> <z2>"), 
                new CGlyph(GlyphId.Clear   ,       "Clear",              0,    0,  "", "Clear entire grid" ),
                new CGlyph(GlyphId.Plot    ,       "Plot",               0,    3,  "x y z v", "Pen Plot at <x> <y> <z> value" ),

                new CGlyph(GlyphId.Line    ,       "Line",               0,    6,  "x y z X Y Z", "Draw Line <x1> <y1> <z1> <x2> <y2> <z2>" ), 
                new CGlyph(GlyphId.Rect    ,       "Rect",               0,    6,  "x y z X Y Z", "Draw hollow 3D rectangle <x1> <y1> <z1> <x2> <y2> <z2>" ), 
                new CGlyph(GlyphId.FillRect,       "FillRect",           0,    6,  "x y z X Y Z", "Draw 3D rectangle <x1> <y1> <z1> <x2> <y2> <z2>" ), 
                
                new CGlyph(GlyphId.MaskRect      , "MaskRect",           0,    7,  "x y z X Y Z #", "Draw Hollow 3D rectangle <x1> <y1> <z1> <x2> <y2> <z2> <mask>" ), 
                new CGlyph(GlyphId.MaskFillRect  , "MaskFillRect",       0,    7,  "x y z X Y Z #", "Draw Filled 3D rectangle <x1> <y1> <z1> <x2> <y2> <z2> <mask>" ), 

                new CGlyph(GlyphId.TubeXy        , "TubeXY",             0,    6,  "x y z X Y Z", "Draw 3D tube <x1> <y1> <z1> <x2> <y2> <z2>" ), 
                new CGlyph(GlyphId.TubeXz        , "TubeXZ",             0,    6,  "x y z X Y Z", "Draw 3D tube <x1> <y1> <z1> <x2> <y2> <z2>" ), 
                new CGlyph(GlyphId.TubeYz        , "TubeYZ",             0,    6,  "x y z X Y Z", "Draw 3D tube <x1> <y1> <z1> <x2> <y2> <z2>" ), 

                new CGlyph(GlyphId.Stairs        , "Stairs",             0,    9,  "x y z X Y Z w h d",   "Create stairs <x1> <y1> <z1> <x2> <y2> <z2> <PenWidth> <PenHeight> <PenDepth>" ),
                new CGlyph(GlyphId.ExtrudeLine   , "ExtrudeLine",        0,    9,  "x y z X Y Z # r R #", "Create extrusion along path <x1> <y1> <z1> <x2> <y2> <z2> <slices> <radius1> <radius 2>" ),
                new CGlyph(GlyphId.Triangle      , "Triangle",           0,    9,  "x y z X Y Z # # #", "Draw Triangle <x1> <y1> <z1> <x2> <y2> <z2> <x3> <y3> <z3>"),
                new CGlyph(GlyphId.FillTriangle2D, "FillTriangle2D",     0,    6,  "x y X Y # #", "Draw Filled 2D Triangle <x1> <y1> <x2> <y2> <x3> <y3>"),
                new CGlyph(GlyphId.Polygon       , "Polygon",            0,    5,  "x y z s #", "Draw 2D Polygon <x> <y> <z> <radius> <sides>" ), 

                new CGlyph(GlyphId.ArcXy         , "ArcXY",              0,    6,  "x y z s # #", "Draw 2D Arc <x> <y> <z> <radius> <startAngle> <sweepAngle>" ), 
                new CGlyph(GlyphId.ArcXz         , "ArcXZ",              0,    6,  "x y z s # #", "Draw 2D Arc <x> <y> <z> <radius> <startAngle> <sweepAngle>" ), 
                new CGlyph(GlyphId.ArcYz         , "ArcYZ",              0,    6,  "x y z s # #", "Draw 2D Arc <x> <y> <z> <radius> <startAngle> <sweepAngle>" ), 
                new CGlyph(GlyphId.Circle3D      , "Circle3D",           0,    4,  "x y z s", "Draw 2D Circle <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.Circle2D      , "Circle2D",           0,    4,  "x y z s", "Draw 2D Circle <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.Circle2Dxy    , "Circle2DXY",         0,    4,  "x y z s", "Draw 2D Circle on z-Axis <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.Circle2Dyz    , "Circle2DYZ",         0,    4,  "x y z s", "Draw 2D Circle on x-Axis <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.Circle2Dxz    , "Circle2DXZ",         0,    4,  "x y z s", "Draw 2D Circle on y-Axis <x> <y> <z> <radius>" ), 
                
                new CGlyph(GlyphId.Oval3D        , "Oval3D",             0,    6,  "x y z w h d", "Draw Oval at <x> <y> <z> <w> <h> <d>" ), 
                new CGlyph(GlyphId.Diamond2D     , "Diamond2D",          0,    5,  "x y z w h", "Draw Diamond at <x1> <y1> <z1> <w> <h>"),
                new CGlyph(GlyphId.Quad2D        , "Quad2D",             0,    6,  "x y X Y Z h", "Draw Quad <x1> <y1> to <x2> <y2> on <z> with <h>"),
                new CGlyph(GlyphId.Pyramid3D     , "Pyramid3D",          0,    6,  "x y z X Y Z", "Draw Pyramid <x1> <y1> <z1> to <x2> <y2> <z2>"),

                new CGlyph(GlyphId.FillCircle2D  , "FillCircle2D",       0,    4,  "x y z s", "Draw Filled 2D Circle <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.FillCircle2Dxy, "FillCircle2DXY",     0,    4,  "x y z s", "Draw Filled 2D Circle on z-axis <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.FillCircle2Dyz, "FillCircle2DYZ",     0,    4,  "x y z s", "Draw Filled 2D Circle on x-axis <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.FillCircle2Dxz, "FillCircle2DXZ",     0,    4,  "x y z s", "Draw Filled 2D Circle on y-axis <x> <y> <z> <radius>" ), 
                new CGlyph(GlyphId.FillCircle3D  , "FillCircle3D",       0,    4,  "x y z s", "Draw Filled 2D Circle <x> <y> <z> <radius>" ), 

                new CGlyph(GlyphId.ExtrudeX      , "ExtrudeX",           0,    8, "x y z X f s S #", "<x1> <y> <z> <x2> <Shape> <StartScale> <StopScale> <Skips>"),
                new CGlyph(GlyphId.ExtrudeY      , "ExtrudeY",           0,    8, "x y z Y f s S #", "<x> <y1> <z> <y2> <Shape> <StartScale> <StopScale> <Skips>"),
                new CGlyph(GlyphId.ExtrudeZ      , "ExtrudeZ",           0,    8, "x y z Z f s S #", "<x> <y> <z1> <z2> <Shape> <StartScale> <StopScale> <Skips>"),

#endregion
#region Imaging
                new CGlyph(GlyphId.ImgFlipX     ,  "ImgFlipX",           0,    0,  "", "Flip on X axis"), 
                new CGlyph(GlyphId.ImgFlipY     ,  "ImgFlipY",           0,    0,  "", "Flip on Y axis"), 
                new CGlyph(GlyphId.ImgFlipZ     ,  "ImgFlipZ",           0,    0,  "", "Flip on Z axis"), 
                new CGlyph(GlyphId.ImgMirrorX   ,  "ImgMirrorX",         0,    0,  "", "Mirror on X axis"), 
                new CGlyph(GlyphId.ImgMirrorY   ,  "ImgMirrorY",         0,    0,  "", "Mirror on Y axis"), 
                new CGlyph(GlyphId.ImgMirrorZ   ,  "ImgMirrorZ",         0,    0,  "", "Mirror on Z axis"), 

                new CGlyph(GlyphId.ImgPalettize ,  "ImgPalettize",       0,    1,  "#", "Use a palette to remap a grid <palette>"), 
                new CGlyph(GlyphId.ImgInvert    ,  "ImgInvert",          0,    0,  "", "Invert the image"), 
                new CGlyph(GlyphId.ImgGrayscale ,  "ImgGrayscale",       0,    0,  "", "Grayscale the image"), 
                new CGlyph(GlyphId.ImgColorize  ,  "ImgColorize",        0,    2,  "# #", "Colorize the image <hue> <saturation>"), 
                new CGlyph(GlyphId.ImgHueshift  ,  "ImgHueshift",        0,    1,  "#", "Hue shift the image <hue shift>"), 
                new CGlyph(GlyphId.ImgSaturate  ,  "ImgSaturate",        0,    1,  "%", "Adjust saturation <adjust %>"), 
                new CGlyph(GlyphId.ImgBrightness,  "ImgBrightness",      0,    1,  "%", "Adjust brightness <adjust %>"), 
                new CGlyph(GlyphId.ImgRotX      ,  "ImgRotX",            0,    0,  "", "Rotate image on X-axis 90 degrees" ), 
                new CGlyph(GlyphId.ImgRotY      ,  "ImgRotY",            0,    0,  "", "Rotate image on Y-axis 90 degrees" ), 
                new CGlyph(GlyphId.ImgRotZ      ,  "ImgRotZ",            0,    0,  "", "Rotate image on Z-axis 90 degrees" ), 

                new CGlyph(GlyphId.ImgShadeX    ,  "ImgShadeX",          0,    6,  "r g b a R G B A", "ShadeX <r1> <g1> <b1> <r2> <g2> <b2>"),
                new CGlyph(GlyphId.ImgShadeY    ,  "ImgShadeY",          0,    6,  "r g b a R G B A", "ShadeY <r1> <g1> <b1> <r2> <g2> <b2>"),
                new CGlyph(GlyphId.ImgShadeZ    ,  "ImgShadeZ",          0,    6,  "r g b a R G B A", "ShadeZ <r1> <g1> <b1> <r2> <g2> <b2>"),

                new CGlyph(GlyphId.ImgEdge      ,  "ImgEdge",            0,    4,  "r g b a", "Set edge color <r> <g> <b> <a>"),
                new CGlyph(GlyphId.ImgEdgeX     ,  "ImgEdgeX",           0,    4,  "r g b a", "Set edge x color <r> <g> <b> <a>"),
                new CGlyph(GlyphId.ImgEdgeY     ,  "ImgEdgeY",           0,    4,  "r g b a", "Set edge y color <r> <g> <b> <a>"),
                new CGlyph(GlyphId.ImgEdgeZ     ,  "ImgEdgeZ",           0,    4,  "r g b a", "Set edge z color <r> <g> <b> <a>"),
                
                new CGlyph(GlyphId.FilterBlur   ,  "FilterBlur",         0,    0,  "", "Blur filter"), 
                new CGlyph(GlyphId.FilterMotion ,  "FilterMotion",       0,    0,  "", "Motion filter"), 
                new CGlyph(GlyphId.FilterEdgeHor,  "FilterEdgeHor",      0,    0,  "", "Edge horizontal filter"), 
                new CGlyph(GlyphId.FilterEdgeVer,  "FilterEdgeVer",      0,    0,  "", "Edge vertical filter"), 
                new CGlyph(GlyphId.FilterEdgeAll,  "FilterEdgeAll",      0,    0,  "", "Edge filteattenuate, r"), 
                new CGlyph(GlyphId.FilterSharpen,  "FilterSharpen",      0,    0,  "", "Sharpen filter"), 
                new CGlyph(GlyphId.FilterEmboss ,  "FilterEmboss",       0,    0,  "", "Emboss filter"), 
                new CGlyph(GlyphId.FilterSmooth ,  "FilterSmooth",       0,    0,  "", "Smooth filter"), 
#endregion
#region Simulation
                new CGlyph(GlyphId.Genesis3D,  "Genesis3D",         0,    1,  "s","New grid of rgba, xyz <size and floor;person in center;CorNew;DekNew;ScnNew;EleNew"),
                new CGlyph(GlyphId.Spawn,      "Spawn",             0,    3,  "x y z","Spawn <x> <y> <z>"),
#endregion
#region Archtypes
                new CGlyph(GlyphId.Pal1D        , "Pal1D",              0,    1,  "w", "Create 1D Palette <width>" ),
                new CGlyph(GlyphId.Pal2D        , "Pal2D",              0,    2,  "w h", "Create 2D Palette <width> <height>" ),
                new CGlyph(GlyphId.Pal3D        , "Pal3D",              0,    3,  "w h d", "Create 3D Palette <width> <height> <depth>" ),
                new CGlyph(GlyphId.PalFromVal1D , "PalFromVal1D",       1,    1,  "", "Create 1D Palette <width> from values <#> ..." ),
                new CGlyph(GlyphId.PalFromVal2D , "PalFromVal2D",       2,    2,  "", "Create 2D Palette <width> <height> from values <#> ..." ),
                new CGlyph(GlyphId.PalFromVal3D , "PalFromVal3D",       3,    3,  "", "Create 3D Palette <width> <height> <depth> from values <#> ..." ),
                new CGlyph(GlyphId.PalFromGrid1D, "PalFromGrid1D",      0,    2,  "x X", "Create new palette from grizd <x1> <x2>"), 
                new CGlyph(GlyphId.PalFromGrid2D, "PalFromGrid2D",      0,    4,  "x y X Y", "Create new palette from grid <x1> <y1> <x2> <y2>"), 
                new CGlyph(GlyphId.PalFromGrid3D, "PalFromGrid3D",      0,    6,  "x y z X Y Z", "Create new palette from grid <x1> <y1> <z1> <x2> <y2> <z2>"), 
                new CGlyph(GlyphId.PalGen1D1    , "PalGen1D1",          0,    3,  "# #", "Generate gradient palette <palette> <vs> <vd>"),
                new CGlyph(GlyphId.PalGen1D2    , "PalGen1D2",          0,    5,  "# # # #", "Generate gradient palette <palette> <vs1> <vs2> <vd1> <vd2>"),
                new CGlyph(GlyphId.PalGen1D3    , "PalGen1D3",          0,    7,  "# # # # # #", "Generate gradient palette <palette> <vs1> <vs2> <vs3> <vd1> <vd2> <vd3>"),
                new CGlyph(GlyphId.PalGen1D4    , "PalGen1D4",          0,    9,  "# # # # # # # #", "Generate gradient palette <palette> <vs1> <vs2> <vs3> <vs4> <vd1> <vd2> <vd3> <vd4>"),

                new CGlyph(GlyphId.Blit1D       , "Blit1D",             0,    2,  "# x", "Draw Palette at <palette> <x>" ),
                new CGlyph(GlyphId.Blit2D       , "Blit2D",             0,    3,  "# x y", "Draw Palette at <palette> <x> <y>" ),
                new CGlyph(GlyphId.Blit3D       , "Blit3D",             0,    4,  "# x y z", "Draw Palette at <palette> <x> <y> <z>" ),
                new CGlyph(GlyphId.RectBlit1D   , "RectBlit1D",         0,    3,  "# x X", "Draw Palette at <palette> <x1> <x2>" ),
                new CGlyph(GlyphId.RectBlit2D   , "RectBlit2D",         0,    5,  "# x y x Y", "Draw Palette at <palette> <x1> <y1> <x2> <y2>"),
                new CGlyph(GlyphId.RectBlit3D   , "RectBlit3D",         0,    7,  "# x y z X Y Z", "Draw Palette at <palette> <x1> <y1> <z1> <x2> <y2> <z2>"),
                new CGlyph(GlyphId.BlendBlit1D  , "BlendBlit1D",        0,    4,  "# x X #", "Blend Palette at <palette> <x1> <x2> <blend>" ),
                new CGlyph(GlyphId.BlendBlit2D  , "BlendBlit2D",        0,    6,  "# x y X Y #", "Blend Palette at <palette> <x1> <y1> <x2> <y2> <blend>"),
                new CGlyph(GlyphId.BlendBlit3D  , "BlendBlit3D",        0,    8,  "# x y z X Y Z #", "Blend Palette at <palette> <x1> <y1> <z1> <x2> <y2> <z2> <blend>"),

                new CGlyph(GlyphId.ArchPut      , "ArchPut",            0,    4,  "x y z #",             "Draw (at) <x> <y> <z> <archtype> "),
                new CGlyph(GlyphId.ArchLine     , "ArchLine",           0,    9,  "x y z X Y Z # # #",   "Draw <x> <y> <z> <x2> <y2> <z2> <archtype> <rotation> <spacing>"),
                new CGlyph(GlyphId.ArchRect     , "ArchRect",           0,    9,  "x y z X Y Z # # #",   "Draw <x> <y> <z> <x2> <y2> <z2> <archtype> <rotation> <spacing>"),
#endregion
#region FX
                new CGlyph(GlyphId.GenChaos      , "GenChaos",           0,    4,  "# x y z", "Generate chaos <0..11> <x> <y> <z>"), 
                new CGlyph(GlyphId.Alien         , "Alien",              0,    4,  "x y z s",   "Draw <x> <y> <z> <scale>"),
                new CGlyph(GlyphId.Star          , "Star",               0,    4,  "x y z s",   "Draw <x> <y> <z> <scale>"),
                new CGlyph(GlyphId.Text          , "Text",               0,    4,  "x y z #", "Draw 2D Letter <x> <y> <z> <letter>" ), 
                new CGlyph(GlyphId.WallCube      , "WallCube",           0,    1,  "f", "Put Walls on Cube <bitmask>" ), 
                new CGlyph(GlyphId.CornerSupports, "CornerSupports",     0,    6,  "x y z X Y Z",   "Fill (cellular automata) <x> <y> <z> <x2> <y2> <z2>"),
                new CGlyph(GlyphId.Supports      , "Supports",           0,    0,  "", "Put Supports in world" ),
                new CGlyph(GlyphId.UpV           , "UpV",                0,    5, "# # r g b a", "UpV <reps> <search r> <search g> <search b> <search a>"),
#endregion
#region RecentlyAdded
                new CGlyph(GlyphId.FillTriangle  , "FillTriangle",       0,    9,  "x y z X Y Z # # #", "Draw Filled Triangle <x1> <y1> <z1> <x2> <y2> <z2> <x3> <y3> <z3>"),
                new CGlyph(GlyphId.Shadows       , "Shadows",            0,    0,  "", "Simulate shadows" ),
                new CGlyph(GlyphId.Maze          , "Maze",               0,    6,  "# x y z X Z", "Generate maze (random seed=#, 0 for no seed) from xyz to XyZ" )
#endregion
            };

        //GetToken the glyph id of a name
        public static int GetId(string str)
        {
            foreach (IGlyph g in GlyphDefs)
                if (g.Name == str)
                    return g.Id;
            return 0;
        }

        //Get glyph at id, null if out of range
        //TODO: Optimize this
        public static IGlyph GetGlyph(int id)
        {
            if (id < 0 || id >= GlyphDefs.Length) return null;

            foreach (IGlyph g in GlyphDefs)
                if (g.Id == id) return g;
            return null;
        }

        //Return an array of all the glyphs
        public static IGlyph[] GetGlyphDefs()
        {
            return GlyphDefs;
        }
    }
}

