using System.ComponentModel.DataAnnotations;


namespace Parcial2_FlorezRSebastian.DAL.Entities
{
    public class Ticket : Entity
    {
        [Display(Name = "Codigo boleta")]        
        //[Range(0, short.MaxValue, ErrorMessage = "Debe ingresar un valor numérico.")]//solo recibe valores numericos
        [RegularExpression("^.{0,100000000000}$", ErrorMessage = "El campo {0} debe tener una longitud máxima de {1} caracteres.")] //Establece el tamaño que debe tener
        [Required(ErrorMessage = "El campo {0} es oblilgatorio.")] //errormessage permite enviar un mensaje de error
        public int CodTicket { get; set; }

        [Display(Name = "Fecha uso boleta")]
        public DateTime? UsedDate { get; set; } //? quiere decir estos campos pueden ser nullos

        [Display(Name = "Boleta usada")]
        public Boolean IsUsed { get; set; } = false;

        [Display(Name = "Porteria entrada")]               
        public String? EntranceGate { get; set; }
    }
}
