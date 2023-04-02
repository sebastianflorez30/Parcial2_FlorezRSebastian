using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Parcial2_FlorezRSebastian.DAL.Entities
{
    public class Entity
    {
        [Key] // establecer como primary key, se coloca sobre el campo
        [Required] //campo es requerido obligatorio (not null)
        public Guid Id { get; set; }
        
    }
}
