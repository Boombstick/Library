using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models.Books;
using Microsoft.AspNetCore.Identity;

namespace Library.Models.Account;

// Add profile data for application users by adding properties to the LibraryUser class
public class LibraryUser : IdentityUser
{
    public int? Reader_Id { get; set; }


}

