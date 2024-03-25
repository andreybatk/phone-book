﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneBook.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DB
{
    // dotnet ef migrations add InitIdentity --project PhoneBook.DB --startup-project PhoneBook
    // dotnet ef database update --project PhoneBook.DB --startup-project PhoneBook

    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Person> Persons { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
