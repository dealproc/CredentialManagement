using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHost.ModelMappers
{
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ConstructServicesUsing(x => DependencyResolver.Current.GetService(x));

                cfg.AddProfile<Account>();
                cfg.AddProfile<Address>();
                cfg.AddProfile<Group>();
                cfg.AddProfile<Permission>();
                cfg.AddProfile<Reseller>();
                cfg.AddProfile<Role>();
                cfg.AddProfile<User>();
            });
        }
    }
}