using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace SGVotaco.models
{
 class Usuario
 {
  [Key]
  private static int Id;
  private static string Nome;
  private string Email;
  private string Senha;
  private string Papel;
  private string Sessao;
  
  public  int id { get => Usuario.Id; set => Usuario.Id = value; }
  // [Required(ErrorMessage = "Este campo é obrigatorio!")]
  public string nome { get => Usuario.Nome; set => Usuario.Nome = value; }
  public string email { get => Email; set => Email = value; }
  
  // [Required(ErrorMessage = "Este campo é obrigatorio!")]
  public string senha { get => Senha; set => Senha = value; }
  public string papel { get => Papel; set => Papel = value; }
  public string sessao { get => Sessao; set => Sessao = value; }
 }
}
