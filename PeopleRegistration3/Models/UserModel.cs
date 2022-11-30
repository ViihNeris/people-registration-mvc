using PeopleRegistration2.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PeopleRegistration2.Models
{
    public class UserModel
    {
        /*        public int Id { get; set; }
                public string Name { get; set; }
                public string Login { get; set; }
                public string Email { get; set; }
                public string Address { get; set; }
                public ProfileEnum Profile { get; set; }
                public string Senha { get; set; }
                public DateTime
                    RegistrationDate
                { get; set; }
                public DateTime? UpdateDate { get; set; }*/

        public int IdUser { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório!")]
        [StringLength(50, ErrorMessage = "O nome do usuário deve ter, no máximo, 50 caracteres.")]
        [Display(Name = "Nome*: ")]

        public String? UserName { get; set; }
        public String? Address { get; set; }
    }
}
