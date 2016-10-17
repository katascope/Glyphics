// Phase 1 gradient editor
// Phase 2 fractal editor
// Phase 3 Combination gradient+fractal editor

//Eventual codes:
//gradient/palette - lower/uppercase = down or up, hexcode numbers?
//0-0,a-z = 36
//so @FF00FFF0F = yellow,cyan,purple
// @F000F000F = red,green,blue
// @FF00FFF0FF000F000F = 6 color palette
// @000F000F000FFF00FFF0FFFF = black,6 colors, white

// @000F00FF00F00FF00FF0FFFF black, red, yellow, green, cyan, blue, violet, white

// equation codes?
// q(equation)
/*

//Basic star from black to white
=x*y/5@000FFF

//Complicated triple form
=x&y/5,x|y/5,x^y/5@000FFF@000F00FF00F00FF00FF0FFFF

//Complex
=255^(x%(y+1))/5@000F00FF00F00FF00FF0FFFF

is a nicely paletted star


*/





// Keep everything in anonymous function, called on window load.
if(window.addEventListener) {
  window.addEventListener('load', function () {
  var canvas, context, canvaso, contexto;

  function init () {
    // Find the canvas element.
    canvaso = document.getElementById('imageView');
    if (!canvaso) { alert('Error: I cannot find the canvas element!'); return; }
    if (!canvaso.getContext) { alert('Error: no canvas.getContext!'); return; }
    contexto = canvaso.getContext('2d');
    if (!contexto) { alert('Error: failed to getContext!'); return;}

    // Add the temporary canvas.
    var container = canvaso.parentNode;
    canvas = document.createElement('canvas');
    if (!canvas) {
      alert('Error: I cannot create a new canvas element!');
      return;
    }

    canvas.id     = 'imageTemp';
    canvas.width  = canvaso.width;
    canvas.height = canvaso.height;
    container.appendChild(canvas);

    context = canvas.getContext('2d');

    document.getElementById('calc').addEventListener("click", commands.calc, false);
    commands.calc();
  }

  // This function draws the #imageTemp canvas on top of #imageView, after which
  // #imageTemp is cleared. This function is called each time when the user
  // completes a drawing operation.
  function img_update () {
        contexto.drawImage(canvas, 5,5);
	context.clearRect(0, 0, canvas.width, canvas.height);
  }

  var commands = {};

  //Functions
  commands.calc = function () {
    var equation=document.getElementsByName("equation")[0].value;
    commands.fractal(equation);
  };

  commands.palette = function(palette) {
        for (var x = 0; x< 256; x++) {
          palette[x*3+0] = 0;
          palette[x*3+1] = 0;//x;
          palette[x*3+2] = x;
        }
  }

  commands.fractal = function(equation) {
    // Create an ImageData object.
    var w = canvas.width;
    var h = canvas.height-32;
    var imgd = context.createImageData(w,h);
    var pix = imgd.data;
    var lum = 0;
    var offset = 0;
    var fractal = 1;
    var r,g,b;

    var palette=new Array(768);

    commands.palette(palette);
    //Do not allow external unverified equation, x-scripting attack might ensue
//    var equation=document.getElementsByName("equation")[0].value;
//      var equation=frac;

    for (var sy = 0; sy < h; sy++) {
      for (var sx = 0; sx < w; sx++) {
        x = sx-w/2;
        y = sy-h/2;
        lum=eval(equation)&255;
        r=palette[lum*3+0];
        g=palette[lum*3+1];
        b=palette[lum*3+2];
        offset = sy*w*4+sx*4+0;
        pix[offset+0] = r;
        pix[offset+1] = g;
        pix[offset+2] = b;
        pix[offset+3] = 255;
      }
    }


//pix.length also there
    // Draw the ImageData object at the given (x,y) coordinates.
    context.putImageData(imgd, 0,32);

   var g = context.createLinearGradient(0, 32, w, 32);
   g.addColorStop(0, '#000000');
   g.addColorStop(1, '#ffffff');
   context.fillStyle = g;
   context.fillRect(0, 0, w, 30);
   



    img_update();
  };

  init();

}, false); }
