using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public Address Address { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string Phone { get; set; }

        [Url(ErrorMessage = "Invalid Website Url.")]
        public string Website { get; set; }

        [Required(ErrorMessage = "Company Id is required.")]
        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Company is required.")]
        
        public Company Company { get; set; }

    }

    public class Address
    {
        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Suite is required.")]
        public string Suite { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Geo Location is required.")]
        public GeoLocation Geo { get; set; }
    }

    public class GeoLocation
    {
        [Required(ErrorMessage = "Latitude is required.")]
        public decimal Lat { get; set; }

        [Required(ErrorMessage = "Longtitude is required.")]
        public decimal Lng { get; set; }
    }
}
