using AutoMapper;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebHost.Api
{
    public class RolePermissionController
        : ApiController
    {
        IDataContext _Database;

        public RolePermissionController(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _Database = db;
        }

        public Models.RoleListItemModel Post(Models.RolePermissionEditModel model)
        {
            var role = _Database.Roles
                .Single(x => x.Id == model.RoleId);

            role.AddPermissions(_Database.Permissions.All().Where(p => model.PermissionIds.Contains(p.Id)));
            var response = Mapper.Map<Models.RoleListItemModel>(role);
            _Database.Commit();

            return response;
        }

        public Models.RoleListItemModel Delete(Models.RolePermissionEditModel model)
        {
            var role = _Database.Roles.Single(x => x.Id == model.RoleId);

            role.RemovePermissions(_Database.Permissions.All().Where(p => model.PermissionIds.Contains(p.Id)));
            var response = Mapper.Map<Models.RoleListItemModel>(role);
            _Database.Commit();

            return response;
        }
    }
}