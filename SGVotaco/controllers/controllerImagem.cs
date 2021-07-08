using SGVotaco.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SGVotaco.controllers
{
    class controllerImagem
    {

        public controllerImagem()
        {

        }

        private Bitmap bmp;
        private OpenFileDialog openFileDialog;
        FileInfo fileInfo;
        private string filter { get; } = "Jpg Files (*.Jpg)|*.Jpg| Png Files (*.Png)|*.Png| AllFiles (*.*)|*.*";
        private string caminhoOriginal = "";
        private string caminhoDestino = "";

        private PictureBox Picture(PictureBox pictureBox)
        {
            try
            {
                openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = filter;


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    caminhoOriginal = openFileDialog.FileName;
                    fileInfo = new FileInfo(caminhoOriginal);
                    bmp = new Bitmap(caminhoOriginal);
                    pictureBox.ImageLocation = caminhoOriginal;
                }
            }
            catch { }

            return pictureBox;
        }

        public string Upload(string CDestino)
        {
            try
            {

                if (File.Exists(CDestino))
                {
                    File.Delete(CDestino);
                    File.Copy(caminhoOriginal, CDestino);
                }
                else File.Copy(caminhoOriginal, CDestino);

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }


            return "";
        }
        public string GetCaminhoDestinoImg()
        {
            string nome = fileInfo.Name;
            nome = nome.Substring(0, nome.Length - 3) + "png";
            return caminhoDestino = getCaminhoProjeto() + nome;
        }

        public string getCaminhoProjeto()
        {
            return caminhoDestino = Application.StartupPath + @"imagem\";
        }

        public string getImagemDefault()
        {
            return getCaminhoProjeto() + "default.png";
        }
        public string getImagemDefault(string nome)
        {
            return getCaminhoProjeto() + $"{nome}.png";
        }

        public PictureBox GetPicture(PictureBox pictureBox) => Picture(pictureBox);
    }
}
