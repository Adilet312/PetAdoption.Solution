using System.Collections.Generic;
using System;
namespace PetAdoption.Models
{
    public class Pet
    {
        public int PetId {get;set;}
        public string PetName {get;set;}
        public DateTime PetDateArrived {get;set;}
        public  ICollection<CustomerPet> Customers {get;}
        public virtual ApplicationUser User { get; set; }
        public Pet()
        {
            this.Customers = new HashSet<CustomerPet>();
        }
    }
}