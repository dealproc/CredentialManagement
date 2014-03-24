namespace DataModel {
    using System;
    public class Address
        : IEquatable<Address> {
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string StateIntlOther { get; set; }
        public virtual string ZipPostal { get; set; }
        public virtual string Country { get; set; }

        public virtual void Copy(Address from) {
            this.Street = from.Street;
            this.City = from.City;
            this.StateIntlOther = from.StateIntlOther;
            this.ZipPostal = from.ZipPostal;
            this.Country = from.Country;
        }

        public bool Equals(Address other) {
            return Street.Equals(other.Street) &&
                City.Equals(other.City) &&
                StateIntlOther.Equals(other.StateIntlOther) &&
                ZipPostal.Equals(other.ZipPostal) &&
                Country.Equals(other.Country);
        }
    }
}