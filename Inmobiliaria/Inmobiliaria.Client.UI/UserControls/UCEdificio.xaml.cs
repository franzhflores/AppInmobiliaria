using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Inmobiliaria.Client.Controller;
using Model = Inmobiliaria.Client.Controller.ServiceInmobiliaria;

namespace Inmobiliaria.Client.UI.UserControls
{
    /// <summary>
    /// Lógica de interacción para UCEdificio.xaml
    /// </summary>
    public partial class UCEdificio : UserControl
    {
        Dictionary<Help.InfoImage,object> _dicInfoImage;
        string _idUbicacion;
        string _idUbicacionDetalle;

        public UCEdificio()
        {
            InitializeComponent();
            lbx_DataList.ItemsSource = LocalDataStore.ListEdificios;
            cbx_Provincia.ItemsSource = LocalDataStore.ListUbicaciones;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            Model.Edificio newEdificio = new Model.Edificio();
            newEdificio.Nombre = txt_Nombre.Text;
            newEdificio.N_Plantas = Convert.ToInt16( txt_NPlantas.Text);
            newEdificio.mainfoto = @"http://localhost:1835/Images/ " + _dicInfoImage[Help.InfoImage.IMAGENAME];
            newEdificio.A_Contruccion = calendar1.SelectedDate.Value ;
            newEdificio.Inf_Adicional = txt_IAdicional.Text;
            newEdificio.Id_Ubi_Detalle = _idUbicacionDetalle;            
            bool response = LocalDataStore.GuardarEdificio(newEdificio, _dicInfoImage[Help.InfoImage.FULLPATH].ToString());
            if (response == true)
            {
                lbx_DataList.Items.Refresh();
                MessageBox.Show("El registro fue exitoso", "Añadir nuevo elemento", MessageBoxButton.OK, MessageBoxImage.Information);
            }
                //Help.UploadFile(newEdificio.mainfoto,_dicInfoImage[Help.InfoImage.FULLPATH].ToString()) ;

        }

        private void tabItem_Nuevo_Loaded(object sender, RoutedEventArgs e)
        {
           // cbx_Provincia.ItemsSource = LocalDataStore.ListUbicaciones;
        }

        private void tabItem_Nuevo_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void cbx_Provincia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Ubicacion ubicacion  = cbx_Provincia.SelectedItem as Model.Ubicacion;
            if (ubicacion == null) return;
            _idUbicacion = ubicacion.Id;
            cbx_Zona.ItemsSource = LocalDataStore.GetUbicacionDetalle(ubicacion);
        }

        private void cbx_Zona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Ubicacion_Detalle ubiDetalle = cbx_Zona.SelectedItem as Model.Ubicacion_Detalle;
            if (ubiDetalle == null) return;
            _idUbicacionDetalle = ubiDetalle.Id;
        }

        private void btn_AgregarFoto_Click(object sender, RoutedEventArgs e)
        {
            _dicInfoImage = Help.GetInfoImage();
            if (_dicInfoImage!=null)
            {
                ima_main.Source = _dicInfoImage[Help.InfoImage.IMAGE] as BitmapImage;
            }
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            string idEdificio = (sender as Button).Tag.ToString();
            if (idEdificio == "") return;
            Model.Edificio edificio = LocalDataStore.ListEdificios.Find(P => P.Id == idEdificio);
            Win_ModificarEdificio win = new Win_ModificarEdificio(edificio);
            win.ShowDialog();
            lbx_DataList.Items.Refresh();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            bool response = LocalDataStore.EliminarEdificio(btn.Tag.ToString());
            if (response)
            {
                MessageBox.Show("Archivo eliminado", "Eliminar un registro", MessageBoxButton.OK, MessageBoxImage.Information);
                lbx_DataList.Items.Refresh();
                //lbx_DataList.ItemsSource = LocalDataStore.ListEdificios;
            }
        }

    }
}
