using AutoMapper;
using DataModel;
using System;
using System.Linq;
using System.Web.Http;

namespace WebHost.Api
{
    public class ChildAccountController
        : ApiController
    {
        IDataContext _Database;

        public ChildAccountController(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _Database = db;
        }

        public Models.ChildAccountEditModel Get(int id)
        {
            var result = Mapper.Map<Models.ChildAccountEditModel>(_Database.Accounts
                .OfType<DataModel.ChildAccount>()
                .SingleOrDefault(x => x.Id == id)
            );
            return result;
        }

        public Models.MasterAccountListItemModel Post(Models.ChildAccountEditModel model)
        {
            var item = Mapper.Map<DataModel.ChildAccount>(model);
            var result = _Database.Accounts.Update(item);
            var response = Mapper.Map<Models.MasterAccountListItemModel>(
                _Database.Accounts.OfType<DataModel.MasterAccount>()
                .SingleOrDefault(x=>x.Id == model.MasterAccountId)
            );
            _Database.Commit();
            return response;
        }

        public Models.MasterAccountListItemModel Put(Models.ChildAccountEditModel model)
        {
            var item = Mapper.Map<DataModel.ChildAccount>(model);
            var result = _Database.Accounts.Update(item);
            var response = Mapper.Map<Models.MasterAccountListItemModel>(
                _Database.Accounts.OfType<DataModel.MasterAccount>()
                .SingleOrDefault(x => x.Id == model.MasterAccountId)
            );
            _Database.Commit();
            return response;
        }
    }
}