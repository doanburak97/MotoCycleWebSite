//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MotorCycleWebsite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public Nullable<int> DealerId { get; set; }
    
        public virtual Dealers Dealers { get; set; }
    }
}