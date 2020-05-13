namespace Core.Models
{
    /// <summary>
    /// Entity of application user.
    /// </summary>
    public class AppUser
    {
        /// <summary>
        /// User Identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Username.
        /// </summary>
        public string Username { get; set; }
    }
}