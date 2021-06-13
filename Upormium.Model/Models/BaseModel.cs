using System;

namespace Upormium.Model.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
