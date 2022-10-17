using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace htmlToImg
{
    class Program
    {

        //Html a imagen
        // para poder usuarlo hay que instalar 2 paquetes nugget
        //HtmlRenderer.WinForms
        //System.Drawing.Common
        static void Main(string[] args)
        {
            //este sera el cogigo html a renderisar 
            var source = @"<!DOCTYPE html>
<html>

<head>
    <style>
     

        table {
            font-family: arial, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

        td,
        th {
            border: 1px solid #dddddd;
            text-align: left;
            padding: 8px;
        }

        tr:nth-child(even) {
            background-color: #dddddd;
        }
        #headBody{
            height:100Px !important ;
        }
    </style>
</head>

<body>
<div  style=' text-align: center;'>
<img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/boleto-compartir-sorteo-educativo.png'
 width='700' 
>
</div>
    <table>

        <tr>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
        </tr>
        <tr>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
        </tr>
        <tr>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
        </tr>
        <tr>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
        </tr>
        <tr>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
        </tr>
        <tr>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
            <td>
                <img src='https://colaboro.sorteostec.org/Colaboro/Testing/ImagenesColaboro//Sorteos/61489/LogoSoECarrusel.jpeg'
                    width='220' height='100'>
            </td>
        </tr>
    </table>
</body>

</html>";
            StartBrowser(source);
            Console.ReadLine();
        }

        //levantamos un WebBrowser
        private static void StartBrowser(string source)
        {
            var th = new Thread(() =>
            {
                var webBrowser = new WebBrowser
                {
                    ScrollBarsEnabled = false,
                    IsWebBrowserContextMenuEnabled = true,
                    AllowNavigation = true
                };

                ///Agregamos un event
                webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
                //Asignamos tamaños de la iagen que queremos generar
                webBrowser.Height = 1334;
                webBrowser.Width = 750;
                webBrowser.DocumentText = source;

                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        static void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //se genera y guarda la imagen, se podria returnar el stream sin guardar para agilizar las operaciones

            var webBrowser = (WebBrowser)sender;
            using (Bitmap bitmap =
                new Bitmap(
                    webBrowser.Width,
                    webBrowser.Height))
            {
                webBrowser
                    .DrawToBitmap(
                    bitmap,
                    new System.Drawing
                        .Rectangle(0, 0, bitmap.Width, bitmap.Height));
                bitmap.Save(@"filename.jpg",
                    System.Drawing.Imaging.ImageFormat.Jpeg);

                
            }

        }
    }
}
