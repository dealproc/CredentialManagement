using AutoMapper.QueryableExtensions;
using DataModel;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Query;

namespace WebHost.Api
{
    public class PermissionController
        : ApiController
    {
        IDataContext _Database;

        public PermissionController(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _Database = db;
        }

        [InlineCountQueryable]
        public IQueryable<Models.PermissionListItemModel> Get(ODataQueryOptions<Models.PermissionListItemModel> query)
        {
            var result = _Database.Permissions
                .All()
                .Project()
                .To<Models.PermissionListItemModel>();
            return result;
        }
    }
}