//chaoscillator functions
  function lerp(v1,v2,scale) {
    var v =( v1+(v2-v1)*scale );
    return v;
  };
  // Function to decode hex rgb value strings
  function hex2rgb(val,o) {
    return parseInt(val.charAt(o),16)*16+parseInt(val.charAt(o+1),16);
  }
  function get_color_h(gradient,colorindex) {
    v = hex2rgb(gradient,colorindex);
    return v;
  }
  function d2h(d) {return d.toString(16);}
  function h2d(h) {return parseInt(h,16);}

  function palgen(palette_text,palette) {
    //clear the palette
    for (var x = 0; x< 768; x++) {
        palette[x] = x/3;
    }
    //abort out if no palette
    if (palette_text.length==0)
      return;

    var bands = palette_text.length/7;
    var r,g,b;
    var r2,g2,b2;
    var bandsize = 256/(bands-1);
    var offset;

    //iterate through each palette band
    for (var band=0;band<=bands;band++) {
      r=get_color_h(palette_text,band*7+0);
      g=get_color_h(palette_text,band*7+2);
      b=get_color_h(palette_text,band*7+4);
      r2=get_color_h(palette_text,(band+1)*7+0);
      g2=get_color_h(palette_text,(band+1)*7+2);
      b2=get_color_h(palette_text,(band+1)*7+4);

      //iterate through each color in the palette band
      for (var c = 0; c < bandsize; c++) {
        scale = c/bandsize;
        offset = band*parseInt(bandsize)*3;
        palette[offset+c*3+0]=lerp(r,r2,scale);
        palette[offset+c*3+1]=lerp(g,g2,scale);
        palette[offset+c*3+2]=lerp(b,b2,scale);
      }
    }
  };

  function fracgen(equation,pix,w,h,palette) {
    var offset = 0;
    var r,g,b;
    var lum=0;

    for (var Y = 0; Y < h; Y++) {
      for (var X = 0; X < w; X++) {
        x = X-w/2;
        y = Y-h/2;

        //Do not allow external unverified equation, x-scripting attack might ensue
        lum=eval(equation)&255;

        r=palette[lum*3+0];
        g=palette[lum*3+1];
        b=palette[lum*3+2];
        offset = Y*w*4+X*4+0;
        pix[offset+0] = r;
        pix[offset+1] = g;
        pix[offset+2] = b;
        pix[offset+3] = 255;
      }
    }
  }
