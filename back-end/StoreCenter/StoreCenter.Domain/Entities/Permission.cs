using System.Text.Json.Serialization;

namespace StoreCenter.Domain.Entities
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // JsonIgnore attribute is used to prevent the serialization of the Roles property
        [JsonIgnore]
        public ICollection<Role>? Roles { get; set; }
    }
}
