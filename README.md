# Glyphics API

## Glyphics is:
*  A 3D Raster engine and C# programming API 
*  A 3D Printer STL model creation & manipulation API
*  A set of 1D/2D/3D raster drawing functions
*  A language, compiler, and virtual GPU
*  A pipeline for 3D Raster grids, pixels, rectangles, and triangles
*  A Stereolithography (.STL) file manipulator (translate/scale/rotate/etc)
*  A Portable Network Graphics (.PNG) file manipulator (resize/colorize/etc)
*  Intended to solve the problem domain of 3D raster grids
   
## Glyphics is NOT:
*  A game, game API, or high-performance 3d engine

### Glyphics requires C# to build and .NET 4 or greater to use.

## How it works
- Glyphics represents a 3D raster of pixels/voxels as a Grid
- Painters can draw various archetypal functions to that grid (rectangles, circles, etc)
- The glyphics language automates and allows compilation of scenes
- The grid can be output to volume rectangles by solving for runs in 3-dimensions
- Volume rectangles can be output to triangles by rendering either cube facets or models
- STL files can be read into a grid and rendered directly using 3d raster triangle functions
- Conversion paths allows many different kinds of inputs and outputs.

## Simple Glyphics code example
* ![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Apps/Animator/Simple-1.PNG)**Size3D4 16 16 16;PenColorD4 31 127 255 255;WallCube 1;**
* ![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Apps/Animator/Simple-2.PNG)**PenColorD4 255 255 255 255;Rect 0 0 0 15 0 15;**
* ![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Apps/Animator/Simple-3.PNG)**PenColorD4 255 31 127 255;FillRect 4 1 4 11 2 11;**
* ![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Apps/Animator/Simple-4.PNG)**PenColorD4 31 255 127 255;Text 6 3 8 65**

# High level view
![](http://i.imgur.com/KKtSuV0.png)

# Examples:

## Example: STL-To-Grid (-To-Rendered-To-PNG)
![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Examples/ExampleSTLToGrid/test.png)

## Example: Code-To-(Grid-To-Rendered-To)-PNG
![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Examples/ExampleCodeToPNG/Ascent.PNG)

## Example: Code-To-STL & preview PNG (Code-To-Grid-To-Triangles-To-Grid-To-Rendered-To-PNG)

### STL file view
![](http://i.imgur.com/LDqSVAF.png)

## Oblique Grid view
![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Examples/ExampleCodeToSTL/preview.png)

## Example of final print of STL generated by exampleCodeToSTL
![](http://i.imgur.com/9wrotEK.png)

![](http://i.imgur.com/SKPyyVX.png)

![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Apps/Animator/NexusAnim.gif)

![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Apps/Animator/AscentAnim.gif)

![](https://github.com/katascope/Glyphics/blob/master/Glyphics/Apps/Animator/ArenaAnim.gif)
