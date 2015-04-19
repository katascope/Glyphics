# Glyphics API

Glyphics is:
*  A 3D Raster engine and C# programming API 
*  A set of 1D/2D/3D raster drawing functions
*  A language, compiler, and virtual GPU
*  A pipeline for 3D Raster grids, pixels, rectangles, and triangles
*  A Stereolithography (.STL) file manipulator (translate/scale/rotate/etc)
*  A Portable Network Graphics (.PNG) file manipulator (resize/colorize/etc)
*  Intended to solve the problem domain of 3D raster grids
   
Glyphics is NOT:
*  A game, game API, or high-performance 3d engine

Glyphics requires C# to build and .NET 4 or greater to use.
 
# High level view
![](http://i.imgur.com/KKtSuV0.png)

# Examples:

Example: STL-To-Grid 
![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Examples/ExampleSTLToGrid/test.png)

Example: Code-To-PNG
![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Examples/ExampleCodeToPNG/Ascent.PNG)

Example: Code-To-STL, with preview
![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Examples/\ExampleCodeToSTL/preview.png)

# How it works

- Glyphics represents a 3D raster of pixels/voxels as a Grid
- Painters can draw various archetypal functions to that grid (rectangles, circles, etc)
- The glyphics language automates and allows compilation of scenes
- The grid can be output to volume rectangles by solving for runs in 3-dimensions
- Volume rectangles can be output to triangles by rendering either cube facets or models
- STL files can be read into a grid and rendered directly using 3d raster triangle functions
- Conversion paths allows many different kinds of inputs and outputs.

