﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.Net;

namespace Inmobiliaria.Client.UI
{
    public static class Help
    {
        /*
        * 
        * Metodo que nos permite abrir archivos de tipo imagen desde las unidades locales...
        *
        * /
        /**/
        public  enum InfoImage { IMAGENAME, FULLPATH, IMAGE };

        public static Dictionary<InfoImage, object> GetInfoImage()
        {
            Dictionary<InfoImage, object> dicDataImage = new Dictionary<InfoImage, object>();
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.Filter = "Archivos de imágen (.jpg)|*.jpg|All Files (*.*)|*.*";
            openFile.Multiselect = false;
            openFile.ShowDialog();

            if (openFile.SafeFileName.ToString() != "")
            {
                dicDataImage[InfoImage.IMAGENAME] = openFile.SafeFileName.ToString();
                dicDataImage[InfoImage.FULLPATH] = openFile.FileName;
                dicDataImage[InfoImage.IMAGE] = GetImage(openFile.FileName);
                return dicDataImage;
            }
            else return null;

        }

        public static BitmapImage GetImage(string fullPath)
        {
            BitmapImage ima = new BitmapImage();
            ima.BeginInit();
            ima.UriSource = new Uri(fullPath, UriKind.RelativeOrAbsolute);
            ima.EndInit();
            return ima;
        }     

        public static void UploadFile(string pathServer, string pathLocal)
        {
            WebClient web = new WebClient();
            Uri addressHost = new Uri(pathServer);

            web.UploadFile(addressHost,pathLocal);

        }
    }
}
