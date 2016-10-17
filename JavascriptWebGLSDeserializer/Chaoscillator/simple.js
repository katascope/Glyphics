/* © 2009 ROBO Design
 * http://www.robodesign.ro
 */

// Keep everything in anonymous function, called on window load.
if(window.addEventListener) {
  window.addEventListener('load', function () {
  var canvas, context, canvaso, contexto;

  // The active tool instance.
  var tool;
  var tool_default = 'pencil';

  function init () {
    // Find the canvas element.
    canvaso = document.getElementById('imageView0');
    
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

    // Get the tool select input.
    var tool_select = document.getElementById('dtool');
    if (!tool_select) {
      alert('Error: failed to get the dtool element!');
      return;
    }
    tool_select.addEventListener('change', ev_tool_change, false);
    // Activate the default tool.
    if (tools[tool_default]) {
      tool = new tools[tool_default]();
      tool_select.value = tool_default;
    }

    var fractal_select = document.getElementById('fractal');
    if (!fractal_select) {
      alert('Error: failed to get the fractal element!');
      return;
    }
    fractal_select.addEventListener('change', fractaler, false);

    // Attach the mousedown, mousemove and mouseup event listeners.
    canvas.addEventListener('mousedown', ev_canvas, false);
    canvas.addEventListener('mousemove', ev_canvas, false);
    canvas.addEventListener('mouseup',   ev_canvas, false);
    document.getElementById('clears').addEventListener("click", commands.clears, false);
  }


  // The general-purpose event handler. This function just determines the mouse
  // position relative to the canvas element.
  function ev_canvas (ev) {
    if (ev.layerX || ev.layerX == 0) { // Firefox
      ev._x = ev.layerX;
      ev._y = ev.layerY;
    } else if (ev.offsetX || ev.offsetX == 0) { // Opera
      ev._x = ev.offsetX;
      ev._y = ev.offsetY;
    }

    // Call the event handler of the tool.
    var func = tool[ev.type];
    if (func) {
      func(ev);
    }
  }

  // The event handler for any changes made to the tool selector.
  function ev_tool_change (ev) {
    if (tools[this.value]) {
      tool = new tools[this.value]();
    }
  }

  // The event handler for any changes made to the fractal
  function fractaler (ev) {
    //alert("go: ");//+this.value);
    commands.fractal(this.value);
  }

  // This function draws the #imageTemp canvas on top of #imageView, after which
  // #imageTemp is cleared. This function is called each time when the user
  // completes a drawing operation.
  function img_update () {
		contexto.drawImage(canvas, 5,5);
		context.clearRect(0, 0, canvas.width, canvas.height);
  }

  function img_shadow() {
    context.shadowOffsetX = 5;
    context.shadowOffsetY = 5;
    context.shadowBlur    = 8;
    context.fillStyle     = 'rgba(0,0,0)';
    context.shadowColor   = 'rgba(0, 0, 0, 0.5)';
    context.fillRect(5,5,canvas.width-50,canvas.height-50);//20, 20, 150, 100);
  }



  var commands = {};

  //Functions
  commands.clears = function () {
//   img_shadow();
   context.fillStyle = "rgb(255,255,255)";
   context.fillRect(0, 0, canvas.width, canvas.height);
   img_shadow();
    img_update();
  }


  commands.fractal = function (frac) {
    // Create an ImageData object.
    var w = canvas.width;
    var h = canvas.height;
    var imgd = context.createImageData(w,h);
    var pix = imgd.data;
    var lum = 0;
    var offset = 0;
    var fractal = 1;

    for (var y = 0; y < h; y++) {
      for (var x = 0; x < w; x++) {
        sx = x-w/2;
        sy = y-h/2;

        if (frac==1) lum = ( ((sx*sy)/5)&255 );
        if (frac==2) lum = ( ((sx^sy)/5)&255 );
        if (frac==3) lum = ( ((sx&sy)/5)&255 );
        if (frac==4) lum = ( ((sx|sy)/5)&255 );

        offset = y*w*4+x*4+0;
        pix[offset+0] = lum;
        pix[offset+1] = lum;
        pix[offset+2] = lum;
        pix[offset+3] = 255;
      }
    }

//pix.length also there
    // Draw the ImageData object at the given (x,y) coordinates.
    context.putImageData(imgd, 0,0);
    img_update();
  };

 // This object holds the implementation of each drawing tool.
  var tools = {};

  // The drawing pencil.
  tools.pencil = function () {
    var tool = this;
    this.started = false;
    
    // This is called when you start holding down the mouse button.
    // This starts the pencil drawing.
    this.mousedown = function (ev) {
        context.beginPath();
        context.moveTo(ev._x, ev._y);
        tool.started = true;
    };

    // This function is called every time you move the mouse. Obviously, it only
    // draws if the tool.started state is set to true (when you are holding down
    // the mouse button).
    this.mousemove = function (ev) {
      if (tool.started) {
        context.lineTo(ev._x, ev._y);
        context.stroke();
      }
    };

    // This is called when you release the mouse button.
    this.mouseup = function (ev) {
      if (tool.started) {
        tool.mousemove(ev);
        tool.started = false;
        img_update();
      }
    };
  };

  // The rectangle tool.
  tools.rect = function () {
    var tool = this;
    this.started = false;

    this.mousedown = function (ev) {
      tool.started = true;
      tool.x0 = ev._x;
      tool.y0 = ev._y;
    };

    this.mousemove = function (ev) {
      if (!tool.started) {
        return;
      }

      var x = Math.min(ev._x,  tool.x0),
          y = Math.min(ev._y,  tool.y0),
          w = Math.abs(ev._x - tool.x0),
          h = Math.abs(ev._y - tool.y0);

      context.clearRect(0, 0, canvas.width, canvas.height);

      if (!w || !h) {
        return;
      }

      context.strokeRect(x, y, w, h);
    };

    this.mouseup = function (ev) {
      if (tool.started) {
        tool.mousemove(ev);
        tool.started = false;
        img_update();
      }
    };
  };

  // The line tool.
  tools.line = function () {
    var tool = this;
    this.started = false;

    this.mousedown = function (ev) {
      tool.started = true;
      tool.x0 = ev._x;
      tool.y0 = ev._y;
    };

    this.mousemove = function (ev) {
      if (!tool.started) {
        return;
      }

      context.clearRect(0, 0, canvas.width, canvas.height);

      context.beginPath();
      context.moveTo(tool.x0, tool.y0);
      context.lineTo(ev._x,   ev._y);
      context.stroke();
      context.closePath();
    };

    this.mouseup = function (ev) {
      if (tool.started) {
        tool.mousemove(ev);
        tool.started = false;
        img_update();
      }
    };
  };

  init();

}, false); }

// vim:set spell spl=en fo=wan1croql tw=80 ts=2 sw=2 sts=2 sta et ai cin fenc=utf-8 ff=unix:
