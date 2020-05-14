using Core.Enums;

namespace Core.Common
{
    /// <summary>
    /// Model for invitation response.
    /// </summary>
    public class InvitationResponce
    {
        /// <summary>
        /// Answer for invitation (accept of decline).
        /// </summary>
        public Answer Answer {get; set;}

        /// <summary>
        /// Recipient Identifier.
        /// </summary>
        public long ToId { get; set; }

        /// <summary>
        /// Sender Identifier.
        /// </summary>
        public long FromId { get; set; }
    }
}