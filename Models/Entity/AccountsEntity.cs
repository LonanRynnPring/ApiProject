using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApprenticeWebAPI.Models.Entity
{
    /// <summary>
    /// This is the accounts entitiy used for the repository
    /// </summary>
    public class AccountsEntity
    {
            /// <summary>
            /// The account Id.
            /// </summary>
            public int AccountId { get; set; }

            /// <summary>
            /// The first name.
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// The surname.
            /// </summary>
            public string Surname { get; set; }

            /// <summary>
            /// The title.
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// The email.
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// The date created.
            /// </summary>
            public DateTime DateCreated { get; set; }

            /// <summary>
            /// The date last updated.
            /// </summary>
            public DateTime DateLastUpdated { get; set; }
    }
}
