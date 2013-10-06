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
    public class RoleController
        : ApiController
    {
        IDataContext _Database;

        public RoleController(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _Database = db;
        }

        [InlineCountQueryable]
        public IQueryable<Models.RoleListItemModel> Get(ODataQueryOptions<Models.RoleListItemModel> query)
        {
            return Mapper.Map<List<Models.RoleListItemModel>>(_Database.Roles.All()).AsQueryable();
            //return _Database.Roles.All().Project().To<Models.RoleListItemModel>();
        }

        public Models.RoleEditModel Get(int id)
        {
            var result = Mapper.Map<Models.RoleEditModel>(_Database.Roles
                .SingleOrDefault(x => x.Id == id)
            );
            return result;
        }

        public Models.RoleListItemModel Post(Models.RoleEditModel model)
        {
            var item = Mapper.Map<DataModel.Role>(model);
            var result = _Database.Roles.Insert(item);
            var response = Mapper.Map<Models.RoleListItemModel>(result);
            _Database.Commit();

            return response;
        }

        public Models.RoleListItemModel Put(Models.RoleEditModel model)
        {
            var item = Mapper.Map<DataModel.Role>(model);
            var result = _Database.Roles.Update(item);
            var response = Mapper.Map<Models.RoleListItemModel>(result);
            _Database.Commit();

            return response;
        }

        public Models.RoleListItemModel Delete(int id)
        {
            var role = _Database.Roles.SingleOrDefault(x => x.Id == id);
            var response = Mapper.Map<Models.RoleListItemModel>(role);
            _Database.Roles.Delete(role);
            _Database.Commit();

            return response;
        }
    }
}