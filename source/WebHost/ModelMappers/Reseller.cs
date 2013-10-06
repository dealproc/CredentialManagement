using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHost.ModelMappers
{
    public class Reseller
        : Profile
    {
        protected override void Configure()
        {
            CreateMap<DataModel.Reseller, Models.ResellerListItemModel>();

            CreateMap<DataModel.Reseller, Models.ResellerEditItemModel>()
                .ForMember(x => x.UseBillToForShipTo, opt => opt.MapFrom(src => src.BillTo.Equals(src.ShipTo)))
                .ReverseMap()
                .ConstructUsing((ResolutionContext ctx) =>
                {
                    var model = ctx.SourceValue as Models.ResellerEditItemModel;
                    if (model == null)
                    {
                        return null;
                    }

                    var DbContext = DependencyResolver.Current.GetService<DataModel.IDataContext>();
                    return DbContext.Resellers.SingleOrDefault(u => u.Id == model.Id) ?? new DataModel.Reseller();
                })
                .AfterMap((src, dest) =>
                {
                    if (src.UseBillToForShipTo)
                    {
                        dest.ShipTo.Copy(dest.BillTo);
                    }
                });

            base.Configure();
        }
    }
}