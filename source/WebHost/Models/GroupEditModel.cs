
namespace WebHost.Models
{
    public class GroupEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupKey { get; set; }
        public string Description { get; set; }
        public int[] ParentGroups { get; set; }
    }
}