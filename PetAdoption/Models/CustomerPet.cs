using System;
namespace PetAdoption.Models
{
    public class CustomerPet
    {
        public int CustomerPetId {get;set;}
        public int CustomerId {get;set;}
        public Customer Customer {get;set;}
        public int PetId {get;set;}
        public Pet Pet {get;set;}
        //public DateTime AdoptionDate {get;set;}
        
    }
}