using Common.DB;

namespace Mgr.Entities
{


    [Table(TableName = "Editors")]
    public class Editor
    {
        [Field(IsId = true)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string USN { get; set; }
        public string PWD { get; set; }
    }

}