using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataModel;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Query;

namespace WebHost.Api
{
    public class ResellerController
        : ApiController
    {
        IDataContext _Database;

        public ResellerController(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _Database = db;
        }

        [InlineCountQueryable]
        public IQueryable<Models.ResellerListItemModel> Get(ODataQueryOptions<Models.ResellerListItemModel> query)
        {
            return _Database.Resellers.All().Project().To<Models.ResellerListItemModel>();
        }

        public Models.ResellerEditItemModel Get(int id)
        {
            var result = Mapper.Map<Models.ResellerEditItemModel>(_Database.Resellers
                .SingleOrDefault(x => x.Id == id)
            );
            return result;
        }

        public Models.ResellerListItemModel Post(Models.ResellerEditItemModel model)
        {
            var item = Mapper.Map<DataModel.Reseller>(model);
            var result = _Database.Resellers.Insert(item);
            var response = Mapper.Map<Models.ResellerListItemModel>(result);
            _Database.Commit();

            return response;
        }

        public Models.ResellerListItemModel Put(Models.ResellerEditItemModel model)
        {
            var item = Mapper.Map<DataModel.Reseller>(model);
            var result = _Database.Resellers.Update(item);
            var response = Mapper.Map<Models.ResellerListItemModel>(result);
            _Database.Commit();

            return response;
        }
    }
}