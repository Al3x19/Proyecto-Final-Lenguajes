using TiendaSoftware.DataBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TiendaSoftware.DTOS.Publishers;

namespace TiendaSoftware.DTOS.Softwares
{
    public class SoftwareDto
    {

        public Guid Id {  get; set; }

        public Guid Direction { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string icon{ get; set; }

        public string Version { get; set; }

        public float Price { get; set; }

        public float score { get; set; }

        public string FileName { get; set; }

        public virtual PublisherDto Publisher { get; set; }

        public List<string> Tags { get; set; }

  


    }
}
