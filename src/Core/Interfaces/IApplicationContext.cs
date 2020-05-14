using System.Threading;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces
{
    /// <summary>
    /// Interface for application context.
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// Users table.
        /// </summary>
        DbSet<AppUser> AppUsers { get; set; }

        /// <summary>
        /// Save changes to database.
        /// </summary>
        /// <returns>Saving result.</returns>
        Task<int> SaveChangesAsync();

        ///// <summary>
        ///// Save changes to database.
        ///// </summary>
        ///// <param name="cancellationToken">Cancellation token.</param>
        ///// <returns>Saving result.</returns>
        //Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}