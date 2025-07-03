using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace myapi.Models
{
    [Index(nameof(UserId), IsUnique = true)]
    public class Cart
    {
            public int Id { get; set; }

            [ForeignKey("AppUser")]
            public string UserId { get; set; }
            public AppUser AppUser { get; set; }

            public List<CartItem> CartItems { get; set; }
    }
}