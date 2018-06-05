using System;
using System.IdentityModel.Tokens.Jwt;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaData.Models
{
    public class user
    {
        [Key]
        public int id{get;set;}
        public string token{get;set;}
    };

    public class city
    {
        [Key]
        public Guid ao_id{get;set;}
        public string off_name{get;set;}
        public string short_name{get;set;}
        public string area{get;set;}
        public string region{get; set;}
        public string district{get;set;}
    }
}