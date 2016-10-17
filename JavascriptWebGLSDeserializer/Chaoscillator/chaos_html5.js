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
    document.getElementById('zoomin').addEventListener("click", commands.zoomin, false);
    document.getElementById('zoomout').addEventListener("click", commands.zoomout, false);
    document.getElementById('invert').addEventListener("click", commands.inv, false);
    document.getElementById('directuse').addEventListener("click", commands.directuse, false);
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

  commands.zoomout = function() {
    var equation=document.getElementsByName("equation")[0].value;
    var sepa = equation.split('/');
    equation = sepa[0]+"/"+(parseFloat(sepa[1])-1);
    document.getElementsByName("equation")[0].value=equation;
    commands.fractal(equation);
  };
  commands.zoomin = function() {
    var equation=document.getElementsByName("equation")[0].value;
    var sepa = equation.split('/');
    equation = sepa[0]+"/"+(parseFloat(sepa[1])+1);
    document.getElementsByName("equation")[0].value=equation;
    commands.fractal(equation);
  };
  commands.inv = function() {
    var equation = document.getElementsByName("equation")[0].value;
    equation += "^255";
    document.getElementsByName("equation")[0].value=equation;
    commands.fractal(equation);
  };

  commands.directuse = function () {
    var direct=document.getElementsByName("direct")[0].value;
    direct = direct.replace(/^\s*|\s*$/g,'');
    var mySplitResult = direct.split("@");
    var equation= mySplitResult[0];
    var palette = mySplitResult[1];
    document.getElementsByName("equation")[0].value=equation;
    document.getElementsByName("gradient")[0].value=palette;
    commands.fractal(equation);
  };

  commands.fractal = function(equation) {
    // Create an ImageData object.
    var w = canvas.width;
    var h = canvas.height-32;
    var imgd = context.createImageData(w,h);
    var pix = imgd.data;
    //var lum = 0;
    var fractal = 1;

    var palette=new Array(768);

    var gradient=document.getElementsByName("gradient")[0].value;
    palgen(gradient,palette);
    fracgen(equation,pix,w,h,palette);

    context.putImageData(imgd, 0,32);

    //Draw the gradient
    var gradient=document.getElementsByName("gradient")[0].value;
    if (gradient.length!=0) {
      var bands = (gradient.length+1)/7;
      var grad = context.createLinearGradient(0, 32, w, 32);
      var r,g,b;
      for (var band=0;band<bands;band++) {
        r=get_color_h(gradient,band*7+0);
        g=get_color_h(gradient,band*7+2);
        b=get_color_h(gradient,band*7+4);
        gradstr = 'rgba('+r+','+g+','+b+',255)';
        grad.addColorStop(band/(bands-1), gradstr);
      }
      context.fillStyle = grad;
      context.fillRect(0, 0, w, 30);
    }

    var direct=document.getElementsByName("direct")[0];
    direct.value = document.getElementsByName("equation")[0].value+"@"+document.getElementsByName("gradient")[0].value;


    img_update();
  };

  init();

}, false); }
