using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using GlyphicsLibrary;

/*
 * Need:
 *  Response hook
 *  SerializedRectsToJson
 */
namespace WebServer
{
    class WebResponder
    {
        private static void quickResponse(HttpListenerContext context, string response)
        {
            string mime;
            context.Response.ContentType = "text/html";

            context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
            context.Response.AddHeader("Last-Modified", DateTime.Now.ToString("r"));
            context.Response.ContentLength64 = response.Length;
            byte[] buffer = new byte[1024 * 16];
            int nbytes;
            for (int i = 0; i < response.Length; i++)
            {
                buffer[i] = (byte)response[i];
            }
            context.Response.OutputStream.Write(buffer, 0, response.Length);
        }

        public static bool GetResponse(HttpListenerContext context)
        {
            string url = context.Request.Url.AbsolutePath;
            string query = context.Request.Url.Query;
            Console.WriteLine("url={0} : Query={1}", url, query);

            if (url.Equals("/Test"))
            {
                string response = "<title>This is a page</title><body>I know Foo!\nTest successful</body>";
                quickResponse(context, response);
                return true;
            }
            else if (url.Equals("/Execute"))
            {
                string response = "";
                string worldPart = query.Split('=')[1];
                worldPart = WebUtility.UrlDecode(worldPart);
                ICode code = GlyphicsApi.CreateCode(worldPart);
                IGrid grid = GlyphicsApi.CodeToGrid(code);
                ISerializedRects srects = GlyphicsApi.RectsToSerializedRects(GlyphicsApi.GridToRects(grid));
                //response = srects.SerializedData;
                response = "<A href=\"deserializer.html?serialized=" + srects.SerializedData+"\">Viewer</a>";
                quickResponse(context, response);
                return true;
            }
            else if (url.Equals("/Worlds"))
            {
                string response = "";
                foreach (ICode code in Program.codes)
                {
                    string name = code.Code.Split(',')[0];
                    //response += name + "<br>";
                    response += "<a href=\"World?name=" + name + "\">" + name + "<br>";
                }
                Console.WriteLine(response);
                quickResponse(context, response);
                return true;
            }
            else if (url.Equals("/World"))
            {
                //http://localhost:3838/World?name=Ascent
                string response = "";
                string worldPart = query.Split('=')[1];
                foreach (ICode code in Program.codes)
                {
                    string name = code.Code.Split(',')[0];
                    if (name.Equals(worldPart))
                    {
                        response += code.Code;
                        quickResponse(context, response);
                        return true;
                    }
                }
                return false;
            }
            else if (url.Equals("/srects2html"))
            {
                string serializedRectsString = query.Substring(1,query.Length-1);
                ISerializedRects srects = GlyphicsApi.CreateSerializedRects(serializedRectsString);
                IRectList rects = GlyphicsApi.SerializedRectsToRects(srects);

                string response = "<title>SerializedRects-to-HTML</title><body>";
                foreach (IRect rect in rects)
                    response += rect + "<br>";
                response += "</body>";
                quickResponse(context, response);
                return true;
            }
            else if (url.Equals("/srects2json"))
            {
                string serializedRectsString = query.Substring(1, query.Length - 1);
                ISerializedRects srects = GlyphicsApi.CreateSerializedRects(serializedRectsString);
                IRectList rects = GlyphicsApi.SerializedRectsToRects(srects);

                string response = "<title>SerializedRects-to-JSON</title>";
    /*            {"employees":[
    {"firstName":"John", "lastName":"Doe"},
    {"firstName":"Anna", "lastName":"Smith"},
    {"firstName":"Peter", "lastName":"Jones"}
]}*/
                response += "{\"rects\":[<br>\n";

                StringBuilder sb = new StringBuilder();

                foreach (IRect rect in rects)
                {
                    string str = "{\"Pt1\":\"" + rect.Pt1 + "\"," +
                                  "\"Pt2\":\"" + rect.Pt2 + "\"," +
                                  "\"RGBA\":\"" + rect.Properties.Rgba + "\"}<br>\n";
                    
                    sb.Append(str);
                }
                response += sb.ToString();
                response += "]}";
                quickResponse(context, response);
                return true;
            }
            return false;
        }
    }
    class Program
    {
        public static ICodeList codes;
        static void Main(string[] args)
        {
            Console.WriteLine("Simple web server starting up");

            const string mediaPath = "\\GitHub\\Glyphics\\Glyph Cores\\";
            codes = GlyphicsApi.GlyToCodes(mediaPath + "default.gly");
            Console.WriteLine("Glyphics core loaded");

            string myFolder = @"C:\Github\Glyphics\JavascriptWebGLSDeserializer\";

            //create server with auto assigned port
            SimpleHTTPServer myServer = new SimpleHTTPServer(myFolder, 3838);


            while (true) { }
        }
    }
}
