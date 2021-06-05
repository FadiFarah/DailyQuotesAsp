using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteWebApp.Models
{
    public class Quote
    {
        [BsonId]
        public string Id { get; set;}

        public string UserId { get; set; }

        public string UserName { get; set; }
        [Required]
        [Display(Name ="Title")]
        public string QuoteTitle { get; set; }

        [Required]
        [Display(Name ="Description")]
        public string QuoteDescription { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string QuoteImageURL { get; set; }

        [Required]
        [Display(Name = "Authur")]
        public string QuoteAuthur { get; set; }

        [Required]
        [Display(Name = "Date")]
        public string QuoteDate { get; set; }

    }
}