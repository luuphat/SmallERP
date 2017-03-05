using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallERP.Data.Models;
using SmallERP.Core.Log;

namespace SmallERP.Data.Controllers
{
    public class Core_StoreController : CoreDataContext
    {
        public Core_StoreController()
            : base(Core.Settings.ConnectionString)
        {
        }

        public string ClassName
        {
            get { return "Core_StoreController"; }
        }

        public List<Core_Store> GetStores(int storeId,string searchKey)
        {
            try
            {
                var _list = (from s in this.Core_Stores.Where(o => (o.Id == storeId || storeId == -1) &&
                                 o.Name.ToLower().Contains(searchKey.ToLower()))
                             select s).ToList();

                return _list;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Add(Core_Store _store)
        {
            try
            {
                this.Core_Stores.InsertOnSubmit(_store);
                this.SubmitChanges();
                return Core.Variable.DB_INSERT_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Edit(Core_Store _store)
        {
            try
            {
                Core_Store _o = this.Core_Stores.Single(o => o.Id == _store.Id);
                _o.Name = _store.Name;
                _o.Address = _store.Address;
                _o.DisplayOrder = _store.DisplayOrder;
                this.SubmitChanges();
                return Core.Variable.DB_UPDATE_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }
    }
}
