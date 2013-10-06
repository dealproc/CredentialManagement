using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Query;

namespace WebHost.Api
{
    public class GroupController
        : ApiController
    {
        IDataContext _Database;

        public GroupController(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _Database = db;
        }

        [InlineCountQueryable]
        public IQueryable<Models.GroupListItemModel> Get(ODataQueryOptions<Models.GroupListItemModel> query)
        {
            return _Database.Groups.All().Project().To<Models.GroupListItemModel>();
        }

        public Models.GroupEditModel Get(int id)
        {
            var result = Mapper.Map<Models.GroupEditModel>(_Database.Groups
                .SingleOrDefault(x => x.Id == id)
            );
            return result;
        }

        public Models.GroupListItemModel Post(Models.GroupEditModel model)
        {
            var item = Mapper.Map<DataModel.Group>(model);
            var result = _Database.Groups.Insert(item);
            var response = Mapper.Map<Models.GroupListItemModel>(result);
            _Database.Commit();

            return response;
        }

        public Models.GroupListItemModel Put(Models.GroupEditModel model)
        {
            var item = Mapper.Map<DataModel.Group>(model);
            var result = _Database.Groups.Update(item);
            var response = Mapper.Map<Models.GroupListItemModel>(result);
            _Database.Commit();

            return response;
        }
    }
}