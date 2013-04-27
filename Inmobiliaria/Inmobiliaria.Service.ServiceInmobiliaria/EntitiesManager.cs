using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inmobiliaria.Service.ServiceInmobiliaria
{
    public sealed class EntitiesManager
    {
        public INMOBILIARIAEntities Context { get;private set; }
        private EntitiesManager()
        {
            Context = new INMOBILIARIAEntities();
        }

        private static EntitiesManager _instance;
        public static EntitiesManager Instance 
        {
            get 
            {
                if (_instance == null)
                    _instance = new EntitiesManager();
                return _instance;
            }
        }
    }
}