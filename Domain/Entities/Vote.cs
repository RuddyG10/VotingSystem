using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Vote
    {
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public Guid CandidateId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
