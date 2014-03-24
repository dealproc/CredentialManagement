using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite.DataMaps
{
    public class ResellerMap
        : ClassMap<Reseller>
    {
        public ResellerMap()
        {
            Id(x => x.Id);

            Map(x => x.LegalName);
            Map(x => x.DoingBusinessAsName);
            Map(x => x.BillingAccountNumber);

            Component(x => x.BillTo, a =>
            {
                a.Access.Property();
                a.Map(x => x.City).Column("BillCity");
                a.Map(x => x.Country).Column("BillCountry");
                a.Map(x => x.StateIntlOther).Column("BillStateIntlOther");
                a.Map(x => x.Street).Column("BillStreet");
                a.Map(x => x.ZipPostal).Column("BillZipPostal");
            });

            Component(x => x.ShipTo, a =>
            {
                a.Access.Property();
                a.Map(x => x.City).Column("ShipCity");
                a.Map(x => x.Country).Column("ShipCountry");
                a.Map(x => x.StateIntlOther).Column("ShipStateIntlOther");
                a.Map(x => x.Street).Column("ShipStreet");
                a.Map(x => x.ZipPostal).Column("ShipZipPostal");
            });

        }
    }
}
