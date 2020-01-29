using System.Collections.Generic;
namespace PetAdoption.Models
{
    public class Customer
    {
        public int CustomerId {get;set;}
        public string CustName {get;set;}
        public string CustPhone {get;set;}
        public double CustBalance {get;set;}
        public virtual ICollection<CustomerPet> Pets {get;set;}

        public Customer()
        {
            this.Pets = new HashSet<CustomerPet>();
        } 
    }
}