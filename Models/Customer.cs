using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewletter { get; set; }
        public MembershipType MembershipType { get; set; }      // nevigation property
        public byte MembershipTypeId { get; set; }
    }
}