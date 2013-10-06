using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataModel;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Query;

namespace WebHost.Api
{
    public class MasterAccountController
        : ApiController
    {
        IDataContext _Database;

        public MasterAccountController(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _Database = db;
        }

        [InlineCountQueryable]
        public IQueryable<Models.MasterAccountListItemModel> Get(ODataQueryOptions<Models.MasterAccountListItemModel> query)
        {
            return _Database.Accounts.OfType<DataModel.MasterAccount>().Project().To<Models.MasterAccountListItemModel>();
        }

        public Models.MasterAccountEditModel Get(int id)
        {
            var result = Mapper.Map<Models.MasterAccountEditModel>(_Database.Accounts
                .OfType<DataModel.MasterAccount>()
                .SingleOrDefault(x => x.Id == id)
            );
            return result;
        }

        public Models.MasterAccountListItemModel Post(Models.MasterAccountEditModel model)
        {
            var item = Mapper.Map<DataModel.MasterAccount>(model);
            var result = _Database.Accounts.Insert(item);
            var response = Mapper.Map<Models.MasterAccountListItemModel>(result);
            _Database.Commit();

            return response;
        }

        public Models.MasterAccountListItemModel Put(Models.MasterAccountEditModel model)
        {
            var item = Mapper.Map<DataModel.MasterAccount>(model);
            var result = _Database.Accounts.Update(item);
            var response = Mapper.Map<Models.MasterAccountListItemModel>(result);
            _Database.Commit();

            return response;
        }
    }
}