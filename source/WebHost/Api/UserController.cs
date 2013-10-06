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
    public class UserController
        : ApiController
    {
        IDataContext _Database;

        public UserController(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _Database = db;
        }

        [InlineCountQueryable]
        public IQueryable<Models.UserEditModel> Get(ODataQueryOptions<Models.UserEditModel> query)
        {
            return _Database.Users.All().Project().To<Models.UserEditModel>();
        }

        public Models.UserEditModel Get(int id)
        {
            var result = Mapper.Map<Models.UserEditModel>(_Database.Users
                .SingleOrDefault(x => x.Id == id)
            );
            return result;
        }

        public Models.UserEditModel Post(Models.UserEditModel model)
        {
            var item = Mapper.Map<DataModel.User>(model);
            var result = _Database.Users.Insert(item);
            _Database.Commit();

            _Database.Reconnect();
            var response = Mapper.Map<Models.UserEditModel>(result);
            return response;
        }

        public Models.UserEditModel Put(Models.UserEditModel model)
        {
            var item = Mapper.Map<DataModel.User>(model);
            var result = _Database.Users.Update(item);
            _Database.Commit();

            _Database.Reconnect();
            var response = Mapper.Map<Models.UserEditModel>(result);
            return response;
        }
    }
}