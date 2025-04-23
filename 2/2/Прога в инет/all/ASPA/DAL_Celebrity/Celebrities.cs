using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Celebrity
{
    public Celebrity()
    {
        FullName = string.Empty;
        Nationality = string.Empty;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string FullName { get; set; }

    [Required]
    [StringLength(2)]
    public string Nationality { get; set; }

    [StringLength(500)]
    public string? RegPhotoPath { get; set; }

    // Убраны виртуальные методы, так как EF сам управляет изменениями
}

public class Lifewent
{
    public Lifewent()
    {
        Description = string.Empty;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Celebrity")]
    public int CelebrityId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(1000)]
    public string Description { get; set; }

    [StringLength(500)]
    public string? RegPhotoPath { get; set; }

    // Навигационное свойство для связи
    public virtual Celebrity Celebrity { get; set; }
}

// Конфигурация контекста (CelebrityContext)
protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lifewent>()
            .HasRequired(l => l.Celebrity)
            .WithMany(c => c.Lifewents) // Добавить коллекцию в Celebrity
            .HasForeignKey(l => l.CelebrityId);
    }

    // В классе Celebrity добавить:
    public virtual ICollection<Lifewent> Lifewents { get; set; } = new List<Lifewent>();