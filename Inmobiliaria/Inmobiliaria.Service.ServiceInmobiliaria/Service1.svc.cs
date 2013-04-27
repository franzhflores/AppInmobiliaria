using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Inmobiliaria.Service.ServiceInmobiliaria
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    public class Service1 : IService1
    {
        EntitiesManager _entitiesManager = EntitiesManager.Instance;

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public List<Edificio> GetTodosLosEdificios()
        {

            //List<Edificio> temp = new List<Edificio>();
            //var context = new INMOBILIARIAEntities();
            return _entitiesManager.Context.msp_getAllEdificio().ToList();
            
            //temp.Add(new Edificio() { Id = "321312", A_Contruccion = DateTime.Now });
            //return temp;

            /*using (var context = new INMOBILIARIAEntities()) 
            {
                return context.msp_getAllEdificio().ToList();
            }*/
        }




        public Dictionary<Ubicacion, List<Ubicacion_Detalle>> GetDictionaryUbicaciones()
        {
            List<Ubicacion> tempListUbicacion =( from t in _entitiesManager.Context.Ubicacion
                                select t).ToList();
            var tempListUbicacionDetalle = from t in _entitiesManager.Context.Ubicacion_Detalle
                                       select t;
            Dictionary<Ubicacion, List<Ubicacion_Detalle>> tempDic = new Dictionary<Ubicacion, List<Ubicacion_Detalle>>();
            foreach (Ubicacion ubi in tempListUbicacion)
                tempDic.Add(ubi, new List<Ubicacion_Detalle>());

            foreach (Ubicacion_Detalle ud in tempListUbicacionDetalle)
                   tempDic.First(P => P.Key.Id == ud.Id_Ubicacion).Value.Add(ud);
            return tempDic;
        }

        public string GuardarEdificio(Edificio edificio, string pathImageOrigen)
        {
            //System.IO.File.Copy(pathImageOrigen, edificio.mainfoto, true);
            System.Data.Objects.ObjectResult  objectResponse = _entitiesManager.Context.InsertEdificio(edificio.Id_Ubi_Detalle,edificio.Nombre, edificio.A_Contruccion, edificio.N_Plantas, edificio.Inf_Adicional, edificio.mainfoto);           
            foreach(string r in objectResponse)
                return r;
            return "";
        }

        public bool EliminarEdificio(string Id_Edificio)
        {
            System.Data.Objects.ObjectResult objectResponse = _entitiesManager.Context.sp_EliminarEdificio(Id_Edificio);
            foreach (int r in objectResponse)
                return r == 1 ? true : false;
            return false;
        }


        public bool ModificarEdificio(Edificio edificio)
        {
            System.Data.Objects.ObjectResult objectResponse = _entitiesManager.Context.msp_ModificarEdificio(edificio.Id,edificio.Id_Ubi_Detalle, edificio.Nombre, edificio.A_Contruccion, edificio.N_Plantas, edificio.Inf_Adicional, edificio.mainfoto);
            foreach (int r in objectResponse)
                return r == 1 ? true : false;
            return false;
        }
    }
}
