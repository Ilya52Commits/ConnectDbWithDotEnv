using System.ComponentModel.DataAnnotations;

namespace ConnectDbWithDotEnv.EntityFramework.Models;

public class Test
{
  [Key]
  public int Id { get; set; }
  public string Login { get; set; }          // Например: "seversta\iaiu.novoselov"
  public string FullName { get; set; }       // Полное имя пользователя
  public DateTime CreatedDate { get; set; } = DateTime.Now;  // Дата добавления
  public DateTime LastLogin { get; set; } = DateTime.Now;    // Дата последнего входа
}