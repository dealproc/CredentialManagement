using AutoMapper;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebHost.Api
{
    public class InheritRoleController
        : ApiController
    {
        IDataContext _Database;
        public InheritRoleController(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _Database = db;
        }
        
        public Models.RoleListItemModel Post(Models.InheritRoleEditModel model)
        {
            var role = _Database.Roles.Single(x => x.Id == model.RoleId);

            role.AddRoles(_Database.Roles.All().Where(r => model.IncludeRoleIds.Contains(r.Id)));
            var response = Mapper.Map<Models.RoleListItemModel>(role);
            _Database.Commit();

            return response;
        }

        public Models.RoleListItemModel Delete(Models.InheritRoleEditModel model)
        {
            var role = _Database.Roles.Single(x => x.Id == model.RoleId);

            role.RemoveRoles(_Database.Roles.All().Where(r => model.IncludeRoleIds.Contains(r.Id)));
            _Database.Roles.Update(role);
            var response = Mapper.Map<Models.RoleListItemModel>(role);
            _Database.Commit();

            return response;
        }
    }
}